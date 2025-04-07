using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject endWallTile;
    public float zSpawn = 0;
    public float tileLength = 60;
    public int numberOfTiles = 40;

    private void Start()
    {
        SpawnTile(0);
        SpawnTile(0);

        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
        
        Instantiate(endWallTile, transform.forward * (zSpawn - 30) + transform.up * 100, transform.rotation);
    }

    private void Update()
    {
        
    }

    public void SpawnTile(int tileIndex)
    {
        Instantiate(tilePrefabs[tileIndex],transform.forward * zSpawn, transform.rotation);
        zSpawn += tileLength;
    }
}
