using UnityEngine;

namespace Bunker
{
    public static class GameUtils
    {
        public static GameObject AddChildObject(GameObject parent)
        {
            return AddChildObject(parent, "");
        }
        public static GameObject AddChildObject(GameObject parent, string name)
        {
            GameObject childObject = new(name);
            childObject.transform.SetParent(parent.transform);
            childObject.transform.position = parent.transform.position;
            return childObject;
        }
    }
}