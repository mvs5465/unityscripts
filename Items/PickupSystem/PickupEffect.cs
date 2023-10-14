using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bunker
{
    public abstract class PickupEffect : MonoBehaviour
    {
        protected ItemData itemData;
        public void SetItemData(ItemData itemData) => this.itemData = itemData;
        public abstract void Apply();
    }
}