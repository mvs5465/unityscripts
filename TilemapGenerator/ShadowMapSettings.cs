using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bunker
{
    [CreateAssetMenu]
    public class ShadowMapSettings : ScriptableObject
    {
        public Tile fade;
        public Tile faderight;
        public Tile fadeleft;
        public Tile fadeup;
        public Tile fadeupright;
        public Tile fadeuprighthalf;
        public Tile fadeuprightquarter;
        public Tile fadeupleft;
        public Tile fadeuplefthalf;
        public Tile fadeupleftquarter;
        public Tile fadedown;
        public Tile fadedownright;
        public Tile fadedownrighthalf;
        public Tile fadedownrightquarter;
        public Tile fadedownleft;
        public Tile fadedownlefthalf;
        public Tile fadedownleftquarter;
    }
}