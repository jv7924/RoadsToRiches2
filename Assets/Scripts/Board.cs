using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private const int BOARD_SIZE = 9;
    private const int TILE_OFFSET = 10;
    private Tile[,] tiles = new Tile[9, 9];

    public GameObject tileEven;
    public GameObject tileOdd;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
        // Instantiate(tile, new Vector3(0f, 0f, 0f), Quaternion.identity);
        // Instantiate(tile, new Vector3(10f, 0f, 0f), Quaternion.identity);
        // Instantiate(tile, new Vector3(20f, 0f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                Instantiate(((i + j) % 2 == 0) ? tileEven : tileOdd, new Vector3(TILE_OFFSET * i, 0f, TILE_OFFSET * j), Quaternion.identity);
                // Store instantiated tile in array
                // Place tile in correct spot
            }
        }
    }
}
