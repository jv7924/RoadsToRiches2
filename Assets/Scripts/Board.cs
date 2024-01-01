using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private const int BOARD_SIZE = 9;
    private const int TILE_OFFSET = 10;
    private GameObject[,] tiles = new GameObject[9, 9];
    public GameObject tileEven;
    public GameObject tileOdd;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                GameObject newTile = Instantiate(((i + j) % 2 == 0) ? tileEven : tileOdd, new Vector3(TILE_OFFSET * j, 0f, TILE_OFFSET * i), Quaternion.identity);
                AddToArray(newTile, i, j);
            }
        }
    }

    private void AddToArray(GameObject gameObject, int x, int z)
    {
        gameObject.name = "Tile " + z + " " + x;
        tiles[z, x] = gameObject;
    }
}
