using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Bunker
{
    public abstract class InteractableController : MonoBehaviour
    {
        private bool inRange = false;

        private void Start()
        {
            CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
            collider.radius = FindObjectOfType<GameController>().gameSettings.InteractDistance;
            collider.isTrigger = true;
            StartCall();
        }

        private void Update()
        {
            if (inRange && Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Player>())
            {
                inRange = true;
                Activate();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<Player>())
            {
                inRange = false;
                Deactivate();
            }
        }

        public virtual bool InRange()
        {
            return inRange;
        }

        protected virtual void StartCall() { }
        protected abstract void Activate();
        protected abstract void Deactivate();
        protected abstract void Interact();
    }
}