using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bunker
{
    public class ShadowMapGenerator : MonoBehaviour
    {
        public Grid parentGrid;
        public Tilemap sourceMap;

        public ShadowMapSettings settings;

        private Tilemap shadowMap;

        private Dictionary<string, Tile> tileAssignments;

        public void Start()
        {
            SetupShadowMap();
            SetupTileAssignments();

            for (int x = sourceMap.cellBounds.xMin; x < sourceMap.cellBounds.xMax; x++)
            {
                for (int y = sourceMap.cellBounds.yMin; y < sourceMap.cellBounds.yMax; y++)
                {
                    string surroundings = GetTileSurroundings(x, y);

                    Tile tileToPlace = null;
                    if (tileAssignments.ContainsKey(surroundings))
                    {
                        tileToPlace = tileAssignments[GetTileSurroundings(x, y)];
                    }
                    shadowMap.SetTile(new Vector3Int(x, y, 0), tileToPlace);
                }
            }

            // int randX;
            // int randY;
            // string surroundings;

            // foreach (KeyValuePair<string, Tile> keyValuePair in tileAssignments)
            // {
            //     Debug.Log("Map entry: " + keyValuePair.Key);
            // }

            // randX = Random.Range(sourceMap.cellBounds.xMin, sourceMap.cellBounds.xMax);
            // randY = Random.Range(sourceMap.cellBounds.yMin, sourceMap.cellBounds.yMax);
            // surroundings = GetTileSurroundings(randX, randY);
            // Debug.Log("Tile surroundings: " + surroundings);
            // Debug.Log("Shouldplace? " + tileAssignments.ContainsKey(surroundings));
            // shadowMap.SetTile(new Vector3Int(randX, randY, 0), settings.fade);

            // randX = Random.Range(sourceMap.cellBounds.xMin, sourceMap.cellBounds.xMax);
            // randY = Random.Range(sourceMap.cellBounds.yMin, sourceMap.cellBounds.yMax);
            // surroundings = GetTileSurroundings(randX, randY);
            // Debug.Log("Tile surroundings: " + surroundings);
            // Debug.Log("Shouldplace? " + tileAssignments.ContainsKey(surroundings));
            // shadowMap.SetTile(new Vector3Int(randX, randY, 0), settings.fade);

            // randX = Random.Range(sourceMap.cellBounds.xMin, sourceMap.cellBounds.xMax);
            // randY = Random.Range(sourceMap.cellBounds.yMin, sourceMap.cellBounds.yMax);
            // surroundings = GetTileSurroundings(randX, randY);
            // Debug.Log("Tile surroundings: " + surroundings);
            // Debug.Log("Shouldplace? " + tileAssignments.ContainsKey(surroundings));
            // shadowMap.SetTile(new Vector3Int(randX, randY, 0), settings.fade);

            // randX = Random.Range(sourceMap.cellBounds.xMin, sourceMap.cellBounds.xMax);
            // randY = Random.Range(sourceMap.cellBounds.yMin, sourceMap.cellBounds.yMax);
            // surroundings = GetTileSurroundings(randX, randY);
            // Debug.Log("Tile surroundings: " + surroundings);
            // Debug.Log("Shouldplace? " + tileAssignments.ContainsKey(surroundings));
            // shadowMap.SetTile(new Vector3Int(randX, randY, 0), settings.fade);
        }

        private void SetupTileAssignments()
        {
            tileAssignments = new Dictionary<string, Tile> {
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
                {"11111 11111 11111 11111 11111", settings.fade},
            };
        }

        private string GetTileSurroundings(int tileX, int tileY)
        {
            int radius = 2;
            string responseCode = "";
            for (int y = radius; y >= -radius; y--)
            {

                if (y != radius) { responseCode += " "; }
                for (int x = -radius; x <= radius; x++)
                {
                    if (sourceMap.HasTile(new Vector3Int(tileX + x, tileY + y)))
                    {
                        responseCode += "1";
                    }
                    else
                    {
                        responseCode += "0";
                    }
                }
            }
            return responseCode;
        }

        private void SetupShadowMap()
        {
            GameObject tilemapObject = new GameObject("ShadowMapContainer");
            tilemapObject.transform.SetParent(parentGrid.transform);
            tilemapObject.transform.localPosition = Vector3.zero;
            shadowMap = tilemapObject.AddComponent<Tilemap>();
            TilemapRenderer trb = tilemapObject.AddComponent<TilemapRenderer>();
            trb.sortingLayerName = "Ground";
            trb.sortingOrder = 2;
        }
    }
}