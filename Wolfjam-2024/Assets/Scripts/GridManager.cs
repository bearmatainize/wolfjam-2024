using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int width, height;

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    [SerializeField] Tile tilePrefab;

    [SerializeField] Tray trayPrefab;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateGrid()
    {
        allTiles = new List<Tile>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 5.0f), Quaternion.identity);
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
