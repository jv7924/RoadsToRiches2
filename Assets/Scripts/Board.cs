using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject tileEven;
    [SerializeField] private GameObject tileOdd;
    private const int BOARD_SIZE = 9;
    private const int TILE_OFFSET = 10;
    private GameObject[,] tiles = new GameObject[9, 9];

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
        for (int i = 1; i <= BOARD_SIZE; i++)
        {
            for (int j = 1; j <= BOARD_SIZE; j++)
            {
                GameObject newTile = Instantiate(((i + j) % 2 == 0) ? tileEven : tileOdd, new Vector3(TILE_OFFSET * j, 0f, TILE_OFFSET * i), Quaternion.identity);
                newTile.transform.SetParent(gameObject.transform);
                AddToArray(newTile, i, j);
            }
        }
    }

    public void AddToArray(GameObject gameObject, int x, int z)
    {
        gameObject.name = "Tile " + z + " " + x;
        tiles[z-1, x-1] = gameObject;
    }
}
