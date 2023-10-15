using System.Collections;
using UnityEngine;
using System;

namespace Bunker
{
    [CreateAssetMenu]
    public class GameSettings : ScriptableObject
    {
        // Game tuning
        public int PlayerStartingHealth = 8;
        public float PlayerJumpForce = 8;
        public float PlayerMoveSpeed = 2;
        public int PlayerInventoryColumns = 8;
        public int PlayerInventoryRows = 4;
        public int BulletDamage = 1;
        public int BulletLifetime = 2;
        public int FireHealAmount = 1;
        public int MartianHealth = 5;
        public int MartianDamage = 1;
        public int MedkitHealAmount = 3;
        public int JetpackJumpBoost = 2;
        public int InteractDistance = 2;

        public UIData uiData;

        // Animations (todo store this somewhere else?)
        public AnimationData JetpackActive;
        public AnimationData JetpackInactive;
        public Sprite shieldSprite;

        // Unity constants
        public string PlayerGameObjectName = "Player";
        public int ITEM_LAYER = 9;
        public int INTERACTABLE_LAYER = 10;
        public string ITEM_LAYER_NAME = "Items";
        public string ITEM_SORTING_LAYER_NAME = "Items";
        public string INTERACTABLE__SORTING_LAYER_NAME = "Interactables";
        public int FRIENDLY_PROJECTILE_LAYER = 8;
        public int ENEMY_PROJECTILE_LAYER = 11;
    }
}