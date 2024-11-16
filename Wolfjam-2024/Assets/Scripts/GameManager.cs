using System.Collections.Generic;
using UnityEngine;

public enum GameLevel { Tutorial };

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private GridManager gridManager;

    [SerializeField] private Tray trayPrefab;

    [SerializeField] private StartTile startTilePrefab;
    [SerializeField] private EndTile endTilePrefab;

    public bool hasWire = false;

    public Wire currentWire = null;

    public Camera Cam
    { get { return cam; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager.GenerateGrid();

        // Set Up first level
        var newTray = Instantiate(trayPrefab);
        newTray.transform.position = new Vector3(cam.transform.position.x - 2.0f, cam.transform.position.y - 4.5f, 0.0f);

        string[] gates = new string[] { "or", "not", "xor", "and", "nor", "nand", "xnor" };
        newTray.AddGateComponents(gates);

        List<Tile> allTiles = gridManager.AllTiles;

        var newStartOneTile = Instantiate(startTilePrefab);
        var newStartOneTileTwo = Instantiate(startTilePrefab);

        allTiles[2].AttachComponent(newStartOneTile.GateComponent, true);
        allTiles[4].AttachComponent(newStartOneTileTwo.GateComponent, true);
        newStartOneTile.transform.position = new Vector3(allTiles[2].transform.position.x, allTiles[2].transform.position.y, -2.0f);
        newStartOneTileTwo.transform.position = new Vector3(allTiles[4].transform.position.x, allTiles[4].transform.position.y, -2.0f);

        var newEndTile = Instantiate(endTilePrefab);

        allTiles[allTiles.Count - 4].AttachComponent(newEndTile.GateComponent, true);
        newEndTile.transform.position = new Vector3(allTiles[allTiles.Count - 4].transform.position.x, allTiles[allTiles.Count - 4].transform.position.y, -2.0f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwapHasWire()
    {
        if (hasWire)
        {
            hasWire = false;
        }
        else
        {
            hasWire = true;
        }
    }
}
