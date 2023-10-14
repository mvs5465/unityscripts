using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bunker
{
    [Serializable]
    public class TilemapGenerator : MonoBehaviour
    {
        public Tile dirtTile;
        public Tile dirtTileBack;

        public Tile baseTile;
        public Tile baseTileBack;

        public List<ItemData> spawnItems = new();
        public GameObject itemSpawnerPrefab;

        public int width = 20;
        public int height = 20;
        // public int numTunnels = 10;

        private Tilemap tilemap;
        private Tilemap tilemapBack;

        private class TilePoint
        {
            public int x;
            public int y;

            public TilePoint(int x0, int y0)
            {
                x = x0;
                y = y0;
            }

            public static int Distance(TilePoint p1, TilePoint p2)
            {
                return (int)Math.Sqrt(Math.Pow((p2.x - p1.x), 2) + Math.Pow((p2.y - p1.y), 2));
            }
        }

        private void Start()
        {
            SetupTilemaps();
            GenerateRectangle(width, height, dirtTile);
            DrawBase();
            // GenerateTunnels(numTunnels);
            // SpawnItems();

            tilemap.RefreshAllTiles();
            tilemapBack.RefreshAllTiles();
            tilemap.AddComponent<TilemapCollider2D>();
        }

        private void DrawBase()
        {
            // Vector3Int centerpoint = new((tilemap.cellBounds.xMin + tilemap.cellBounds.xMax) / 2, 0);
            // tilemap.SetTile(centerpoint, null);
            GenerateRectangle(5, 5, null);
        }


        private void SpawnItems()
        {
            // Iterate through the spawn items list
            foreach (ItemData spawnItem in spawnItems)
            {
                // Each item has a spawn chance per virutal chunk
                // Each virtual chunk is 10x10 blocks
                // So spawns per map = numChunks * spawnChance;
                int mapHeight = tilemap.cellBounds.xMax - tilemap.cellBounds.xMin;
                int mapWidth = tilemap.cellBounds.yMax - tilemap.cellBounds.yMin;
                int spawnsPerMap = (int)(mapHeight * mapWidth / 100 * spawnItem.spawnDensity);

                Debug.Log("Spawning " + spawnsPerMap + " " + spawnItem.itemName);

                for (int i = 0; i < spawnsPerMap; i++)
                {
                    // Look for an open square or give up
                    bool spawned = false;
                    int spawnAttempts = 100;
                    while (!spawned && spawnAttempts > 0)
                    {
                        int spawnX = UnityEngine.Random.Range(tilemap.cellBounds.xMin, tilemap.cellBounds.xMax);
                        int spawnY = UnityEngine.Random.Range(tilemap.cellBounds.yMin, tilemap.cellBounds.yMax);

                        Vector3Int spawnPosition = new Vector3Int(spawnX, spawnY);

                        if (!tilemap.HasTile(spawnPosition))
                        {
                            Vector3 spawnPositionWorld = tilemap.GetCellCenterWorld(spawnPosition);
                            Instantiate(itemSpawnerPrefab, spawnPositionWorld, transform.rotation);
                            spawned = true;
                            // Debug.Log("Spawned item [" + spawnItem.itemName + "] at location (" + spawnPositionWorld.x + "," + spawnPosition.y + ")");
                        }
                        --spawnAttempts;
                    }
                }
            }
        }

        private void GenerateTunnels(int numTunnels)
        {
            for (int i = 0; i < numTunnels; i++)
            {
                int startingX = UnityEngine.Random.Range(tilemap.cellBounds.xMin * 3 / 4, tilemap.cellBounds.xMax * 3 / 4);
                int startingY = UnityEngine.Random.Range(tilemap.cellBounds.yMin * 3 / 4, tilemap.cellBounds.yMax * 3 / 4);

                // generate max tunnel length from half to full map length
                int maxTunnelLength = TilePoint.Distance(new TilePoint(tilemap.cellBounds.xMin, tilemap.cellBounds.yMin), new TilePoint(tilemap.cellBounds.xMax, tilemap.cellBounds.yMax));
                int tunnelLength = UnityEngine.Random.Range(maxTunnelLength / 2, maxTunnelLength);

                // Generate the first tunnel at the surface
                if (i == 0)
                {
                    startingY = 0;
                    Debug.Log("Digging tunnel starting at (x/y) with length: (" + startingX + "," + startingY + " : " + tunnelLength);
                }

                RandomWalkTunnel(startingX, startingY, tunnelLength);
            }
        }

        private void SetupTilemaps()
        {
            GameObject gridObject = new GameObject("GenGrid");
            gridObject.transform.SetParent(gameObject.transform);
            gridObject.transform.localPosition = Vector3.zero;
            Grid grid = gridObject.AddComponent<Grid>();

            GameObject tilemapObject = new GameObject("GenTilemap");
            tilemapObject.transform.SetParent(gridObject.transform);
            tilemapObject.transform.localPosition = Vector3.zero;
            tilemap = tilemapObject.AddComponent<Tilemap>();
            tilemap.tag = "Ground";
            TilemapRenderer tr = tilemap.AddComponent<TilemapRenderer>();
            tr.sortingLayerName = "Ground";
            tr.sortingOrder = 1;

            GameObject tilemapObjectBack = new GameObject("GenTilemapBack");
            tilemapObjectBack.transform.SetParent(gridObject.transform);
            tilemapObjectBack.transform.localPosition = Vector3.zero;
            tilemapBack = tilemapObjectBack.AddComponent<Tilemap>();
            TilemapRenderer trb = tilemapBack.AddComponent<TilemapRenderer>();
            trb.sortingLayerName = "Ground";
            trb.sortingOrder = 0;
        }

        private void GenerateRectangle(int diameter, int height, Tile tile)
        {
            for (int y = -height; y <= 0; y++)
            {
                for (int x = -diameter / 2; x <= diameter / 2; x++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }

        }

        private void RandomWalkTunnel(int startingX, int startingY, int tunnelLength)
        {
            // Debug.Log("Digging tunnel starting at (x/y) with length: (" + startingX + "," + startingY + " : " + tunnelLength);
            int horizontalBiasAmount = 2;
            int horizontalBias = UnityEngine.Random.Range(-horizontalBiasAmount, horizontalBiasAmount);
            int verticalBiasAmount = -2;

            // Generate path
            List<TilePoint> path = new();
            int curX = startingX;
            int curY = startingY;
            path.Add(new TilePoint(curX, curY));

            for (int step = 0; step < tunnelLength; step++)
            {
                curX += UnityEngine.Random.Range(-1 + horizontalBias, 1 + horizontalBias);
                curY += UnityEngine.Random.Range(0, verticalBiasAmount);
                path.Add(new TilePoint(curX, curY));

                if (curX < tilemap.cellBounds.xMin || curX > tilemap.cellBounds.xMax || curY < tilemap.cellBounds.yMin || curY > tilemap.cellBounds.yMax)
                {
                    // Stop if we went out of the bounds of the map
                    break;
                }
            }

            // Carve path and surrounding 3 tiles
            foreach (TilePoint point in path)
            {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        tilemap.SetTile(new Vector3Int(point.x + x, point.y + y, 0), null);
                        tilemapBack.SetTile(new Vector3Int(point.x + x, point.y + y, 0), dirtTileBack);
                    }
                }
            }

        }

        private void CarveTunnel()
        {
            // Pick a random starting point on the surface
            int startingX = UnityEngine.Random.Range(tilemap.cellBounds.xMin, tilemap.cellBounds.xMax);
            Debug.Log("Picked tile #" + startingX);
            Debug.Log("tilemap.cellBounds.yMin: " + tilemap.cellBounds.yMin);

            // Delete that cell and half of the height downwards
            // Also delete the two cells beside it
            for (int y = 0; y > tilemap.cellBounds.yMin / 2; y--)
            {
                int jitter = UnityEngine.Random.Range(0, 2);

                int xPos = startingX + jitter;
                tilemap.SetTile(new Vector3Int(xPos, y, 0), null);
                tilemap.SetTile(new Vector3Int(xPos - 1, y, 0), null);
                tilemap.SetTile(new Vector3Int(xPos + 1, y, 0), null);
            }

        }
    }
}