using UnityEngine;

namespace Bunker
{
    public abstract class Buff : MonoBehaviour
    {
        public BuffData buffData;
        protected Lifeform target;
        protected GameSettings gameSettings;

        public void Apply()
        {
            gameSettings = FindObjectOfType<GameController>().gameSettings;
            ApplyCall();
            if (buffData?.duration > 0)
            {
                Invoke("Remove", buffData.duration);
            }
        }

        public void Remove()
        {
            target.GetComponent<BuffController>().RemoveBuff(GetType());
            Destroy(gameObject);
        }

        public void SetTarget(Lifeform target)
        {
            this.target = target;
        }

        public abstract bool IsUnique();
        protected abstract void ApplyCall();
        protected abstract void RemoveCall();
    }
}