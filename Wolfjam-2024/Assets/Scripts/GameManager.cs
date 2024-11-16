using System.Collections.Generic;
using UnityEngine;

public enum GameLevel { Tutorial };

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private GridManager gridManager;

    [SerializeField] private Tray trayPrefab;

    [SerializeField] private StartZeroTile startZeroTilePrefab;
    [SerializeField] private StartOneTile startOneTilePrefab;

    public Camera Cam { get { return cam; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager.GenerateGrid();

        // Set Up first level
        var newTray = Instantiate(trayPrefab);
        newTray.transform.position = new Vector3(cam.transform.position.x - 2.0f, cam.transform.position.y - 4.5f, 0.0f);

        string[] gates = new string[] { "or", "or", "or" };
        newTray.AddGateComponents(gates);

        var newStartOneTile = Instantiate(startOneTilePrefab);
        var newStartOneTileTwo = Instantiate(startOneTilePrefab);

        List<Tile> allTiles = gridManager.AllTiles;
        allTiles[2].AttachComponent(newStartOneTile.GateComponent, true);
        allTiles[4].AttachComponent(newStartOneTileTwo.GateComponent, true);
        newStartOneTile.transform.position = new Vector3(allTiles[2].transform.position.x, allTiles[2].transform.position.y, -1.0f);
        newStartOneTileTwo.transform.position = new Vector3(allTiles[4].transform.position.x, allTiles[4].transform.position.y, -1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
