using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int width, height;

    [SerializeField] Tile tilePrefab;

    [SerializeField] private Transform cam;

    private List<Tile> allTiles;

    public List<Tile> AllTiles
    {
        get
        {
            return allTiles;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        allTiles = new List<Tile>();
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile Loc {x}, {y}";

                if ((x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0))
                {
                    spawnedTile.InitializeColor(true);
                }
                else
                {
                    spawnedTile.InitializeColor(false);
                }

                allTiles.Add(spawnedTile);

            }
        }

        cam.transform.position = new Vector3(((float)width / 1.5f) - 0.5f, ((float)height / 2.5f) - 0.5f, -10f);
    }
}
