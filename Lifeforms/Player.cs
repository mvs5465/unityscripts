using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bunker
{
    public class Player : Lifeform
    {
        public PlayerSaveData playerSaveData;
        private float movingSpeed;
        private float jumpForce;

        private float moveInput;
        public bool facingRight = false;

        private bool isGrounded = false;
        private int jumpsLeft = 1;
        private int maxJumps = 1;
        private int killCount = 0;

        public Inventory inventory;

        private Camera mainCamera;
        public static Action<Vector3> OnPlayerHealthChange;

        // Weapons
        public WeaponData startingWeaponData;
        private GameObject weaponContainer;
        private List<WeaponData> weapons = new();
        private int currentWeaponIndex = 0;

        // Animations
        public AnimationData idleAnimation;
        public AnimationData fallingAnimation;
        public AnimationData flyingAnimation;
        public AnimationData runningAnimation;
        private PsuedoAnimationController animationController;

        override protected void StartCall()
        {
            mainCamera = FindObjectOfType<Camera>();
            curHealth = maxHealth = gameSettings.PlayerStartingHealth;
            movingSpeed = gameSettings.PlayerMoveSpeed;
            jumpForce = gameSettings.PlayerJumpForce;

            inventory = ScriptableObject.CreateInstance<Inventory>();
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.freezeRotation = true;

            GroundDetector.Create(gameObject, 0.1f, Vector3.down * -0.5f, new GroundNotificationTarget(this));

            animationController = PsuedoAnimationController.Build(gameObject, idleAnimation).GetComponent<PsuedoAnimationController>();

            // Weapons
            InitializeWeaponContainer();
            weapons.Add(startingWeaponData);
            Invoke("DamageCall", 0.1f);

            // LoadSaveState();
        }


        public void SavePlayerState()
        {
            playerSaveData.currentWeaponIndex = currentWeaponIndex;
            playerSaveData.weapons = weapons;
            playerSaveData.buffs = buffController.buffList;
        }

        private void LoadSaveState()
        {
            if (playerSaveData.weapons != null && playerSaveData.weapons.Count > 0)
            {
                currentWeaponIndex = playerSaveData.currentWeaponIndex;
                weapons = playerSaveData.weapons;
                weaponContainer.GetComponent<WeaponPlayerController>().ChangeWeapon(weapons[currentWeaponIndex]);
            }

            if (buffController.buffList != null)
            {
                foreach (Type buff in buffController.buffList)
                {
                    buffController.RemoveBuff(buff);
                }
            }

            if (playerSaveData.buffs != null)
            {
                foreach (Type buff in playerSaveData.buffs)
                {
                    Debug.Log("Adding buff from save data: " + buff);
                    buffController.AddBuff(buff);
                }
            }
        }

        public static Inventory GetInventory()
        {
            return FindObjectOfType<Player>()?.inventory;
        }

        public void AddJumps(int amount)
        {
            maxJumps += amount;
        }

        public void IncrementKillCount()
        {
            killCount++;
        }

        public int GetKillCount()
        {
            return killCount;
        }

        public void NotifyGround(bool detected)
        {
            isGrounded = detected;
            if (isGrounded) { jumpsLeft = maxJumps; }
        }
        private void Update()
        {
            AnimationData animationToPlay = idleAnimation;
            if (Input.GetButton("Horizontal"))
            {
                moveInput = Input.GetAxis("Horizontal");
                rb.velocity = new Vector3(moveInput * movingSpeed, rb.velocity.y, 0);
                animationToPlay = runningAnimation;
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            if (rb.velocity.y > 0)
            {
                animationToPlay = flyingAnimation;
            }
            else if (rb.velocity.y < 0)
            {
                animationToPlay = fallingAnimation;
            }
            if (animationController.GetAnimation() != animationToPlay)
            {
                animationController.SetAnimation(animationToPlay);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                movingSpeed = 2 * gameSettings.PlayerMoveSpeed;
            }
            else
            {
                movingSpeed = gameSettings.PlayerMoveSpeed;
            }

            if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsLeft > 0))
            {
                jumpsLeft -= 1;
                rb.velocity = new Vector3(rb.velocity.x, 0, 0);
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }

            if (Input.GetMouseButton(0))
            {
                FireWeapon();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ChangeWeapons();
            }

            Vector3 dir = Input.mousePosition - mainCamera.WorldToScreenPoint(transform.position);
            if (facingRight == false && dir.x > transform.position.x)
            {
                Flip();
            }
            else if (facingRight == true && dir.x < transform.position.x)
            {
                Flip();
            }
        }

        public void GrantWeapon(WeaponData newWeapon)
        {
            if (!weapons.Contains(newWeapon))
            {
                weapons.Add(newWeapon);
            }
            else
            {
                UINotifications.Notify.Invoke("Cannot equip more " + newWeapon.itemName + "!");
            }
        }

        void FireWeapon()
        {
            weaponContainer.GetComponent<WeaponPlayerController>().Fire();
        }

        override protected void DieCall()
        {
            // SceneManager.LoadScene(2);
            Time.timeScale = 0;
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            other.gameObject.GetComponent<ItemPrefabController>()?.Pickup(gameObject);
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
            FindObjectOfType<WeaponPlayerController>().transform.localScale = -Scaler;
        }

        //////////
        // Weapons
        private void InitializeWeaponContainer()
        {
            weaponContainer = GameUtils.AddChildObject(gameObject, "WeaponController");
            WeaponPlayerController weaponController = weaponContainer.AddComponent<WeaponPlayerController>();
            weaponController.Initialize(startingWeaponData);
        }

        private void ChangeWeapons()
        {
            if (weapons.Count == 0) return;

            int nextWeaponIndex = currentWeaponIndex + 1;
            if (nextWeaponIndex > weapons.Count - 1)
            {
                nextWeaponIndex = 0;
            }
            currentWeaponIndex = nextWeaponIndex;
            weaponContainer.GetComponent<WeaponPlayerController>().ChangeWeapon(weapons[currentWeaponIndex]);
        }

        public override void AddBuff(Type buffType)
        {
            base.AddBuff(buffType);
            UINotifications.Notify.Invoke("Player gained buff " + buffType);
        }

        public override void RemoveBuff(Type buffType)
        {
            base.RemoveBuff(buffType);
            UINotifications.Notify.Invoke("Player lost buff " + buffType);
        }

        protected override void DamageCall()
        {
            OnPlayerHealthChange.Invoke(new Vector3(curHealth, maxHealth, shield));
        }

        public void SetJumpForce(float newValue)
        {
            jumpForce = newValue;
        }

        public float GetJumpForce()
        {
            return jumpForce;
        }
    }
}
