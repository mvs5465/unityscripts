using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bunker
{
    public class TerrainGenerator : MonoBehaviour
    {
        public Tile dirtTile;
        public Tile dirtBackTile;
        public Tile baseTile;
        public Tile baseBackTile;

        private Tilemap colliderMap;
        private Tilemap decorMap;

        private void Start()
        {
            SetupTilemaps();
            DrawRectangle(colliderMap, dirtTile, new Vector2Int(-15, -15), new Vector2Int(15, 0));

            RandomRoom();
            RandomRoom();
            RandomRoom();
            RandomRoom();
            RandomRoom();
            ClearSharedBackground();
        }

        private void RandomRoom()
        {

            Vector2Int randomSpot = new(Random.Range(colliderMap.cellBounds.xMin, colliderMap.cellBounds.xMax), Random.Range(colliderMap.cellBounds.yMin, colliderMap.cellBounds.yMax));
            Vector2Int randomSize = new(Random.Range(2, 8), Random.Range(2, 8));
            CreateRoom(randomSpot, randomSize.x, randomSize.y);
        }

        private void CreateRoom(Vector2Int bottomLeft, int width, int height)
        {
            Vector2Int topRight = new(bottomLeft.x + width, bottomLeft.y + height);
            DrawRectangle(colliderMap, baseTile, bottomLeft, topRight);
            DrawRectangle(colliderMap, null, bottomLeft + Vector2Int.one, topRight - Vector2Int.one);
            DrawRectangle(decorMap, baseBackTile, bottomLeft + Vector2Int.one, topRight - Vector2Int.one);
        }

        private void ClearSharedBackground()
        {
            for (int y = colliderMap.cellBounds.yMin; y <= colliderMap.cellBounds.yMax; y++)
            {
                for (int x = colliderMap.cellBounds.xMin; x <= colliderMap.cellBounds.xMax; x++)
                {
                    Vector3Int pos = new(x, y);
                    if (decorMap.GetTile(pos))
                    {
                        colliderMap.SetTile(pos, null);
                    }
                }
            }
        }

        private void DrawRectangle(Tilemap tilemap, Tile tile, Vector2Int startPoint, Vector2Int endPoint)
        {
            for (int y = startPoint.y; y <= endPoint.y; y++)
            {
                for (int x = startPoint.x; x <= endPoint.x; x++)
                {
                    Vector3Int pos = new(x, y);
                    tilemap.SetTile(pos, tile);
                }
            }
        }

        private void SetupTilemaps()
        {
            GameObject gridObject = new("TerrainGeneratorGrid");
            gridObject.AddComponent<Grid>();

            GameObject colliderMapObject = GameUtils.AddChildObject(gridObject, "ColliderMap");
            colliderMap = colliderMapObject.AddComponent<Tilemap>();
            colliderMapObject.AddComponent<TilemapCollider2D>();
            colliderMap.tag = "Ground";
            TilemapRenderer tr = colliderMapObject.AddComponent<TilemapRenderer>();
            tr.sortingLayerName = "Ground";
            tr.sortingOrder = 1;

            GameObject decorMapObject = GameUtils.AddChildObject(gridObject, "DecorationMap");
            decorMap = decorMapObject.AddComponent<Tilemap>();
            TilemapRenderer trb = decorMapObject.AddComponent<TilemapRenderer>();
            trb.sortingLayerName = "Ground";
            trb.sortingOrder = 0;
        }
    }
}