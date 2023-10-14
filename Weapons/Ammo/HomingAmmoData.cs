using UnityEngine;

namespace Bunker
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Ammo/HomingAmmoEffectData")]
    public class HomingAmmoData : AmmoEffectData
    {
        public float homingRadius = 5;
        public float homingDelay = 0.5f;

        public override GameObject Build(GameObject parent, AmmoEffectData ammoEffectData, int layer)
        {
            GameObject homingObject = GameUtils.AddChildObject(parent);
            homingObject.layer = layer;
            HomingAmmoEffect homingAmmoEffect = homingObject.AddComponent<HomingAmmoEffect>();
            homingAmmoEffect.homingAmmoData = ammoEffectData as HomingAmmoData;
            return homingObject;
        }
    }
}