using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLevel { Tutorial };

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private GridManager gridManager;
    private TruthTableManager truthTableManager;

    [SerializeField] private Tray trayPrefab;

    [SerializeField] private StartTile startTilePrefab;
    [SerializeField] private EndTile endTilePrefab;

    private List<StartTile> startTiles = new List<StartTile>();
    private EndTile endTile;

    public bool hasWire = false;

    public Wire currentWire = null;

    public List<int> resultsList = new List<int>();

    [SerializeField] int gatesIndex;

    private string[] orLevelGates = new string[] { "or" };                              //0
    private string[] andLevelGates = new string[] { "and" };                            //1
    private string[] notLevelGates = new string[] { "not" };                            //2
    private string[] nandLevelGates = new string[] { "not", "and" };                    //3
    private string[] pureNandLevelGates = new string[] { "nand" };                      //4
    private string[] norLevelGates = new string[] { "nor" };                            //5
    private string[] norFromNandGates = new string[] { "nand", "nand", "nand", "nand" };//6

    private List<string[]> gatesLists = new List<string[]>();

    public Camera Cam
    { get { return cam; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        gatesLists.Add(orLevelGates);
        gatesLists.Add(andLevelGates);
        gatesLists.Add(notLevelGates);
        gatesLists.Add(nandLevelGates);
        gatesLists.Add(pureNandLevelGates);
        gatesLists.Add(norLevelGates);
        gatesLists.Add(norFromNandGates);

        truthTableManager = GameObject.Find("TruthTable").GetComponent<TruthTableManager>();

        gridManager.GenerateGrid();

        // Set Up first level
        var newTray = Instantiate(trayPrefab);
        newTray.transform.position = new Vector3(cam.transform.position.x - 2.0f, cam.transform.position.y - 4.5f, 0.0f);

        string[] gates = gatesLists[gatesIndex];
        newTray.AddGateComponents(gates);

        List<Tile> allTiles = gridManager.AllTiles;

        var newStartOneTile = Instantiate(startTilePrefab);
        var newStartOneTileTwo = Instantiate(startTilePrefab);

        startTiles.Add(newStartOneTile);
        startTiles.Add(newStartOneTileTwo);

        allTiles[2].AttachComponent(newStartOneTile.GateComponent, true);
        allTiles[4].AttachComponent(newStartOneTileTwo.GateComponent, true);
        newStartOneTile.transform.position = new Vector3(allTiles[2].transform.position.x, allTiles[2].transform.position.y, -2.0f);
        newStartOneTileTwo.transform.position = new Vector3(allTiles[4].transform.position.x, allTiles[4].transform.position.y, -2.0f);

        var newEndTile = Instantiate(endTilePrefab);
        endTile = newEndTile;

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

    public void TestAndPause()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        int resultsIndex = 0;
        resultsList.Clear();
        for (int i = 0; i < startTiles.Count; i++)
        {
            for (int j = 0; j < startTiles.Count; j++)
            {
                startTiles[0].Set(i == 0 ? false : true);
                startTiles[1].Set(j == 0 ? false : true);

                yield return new WaitForSeconds(1f);

                if (endTile.input1.currentState == WireNodeState.On)
                {
                    resultsList.Add(1);
                }
                else
                {
                    resultsList.Add(0);
                }

                resultsIndex++;

                yield return new WaitForSeconds(2f);

            }
        }

        truthTableManager.ChangeYours(resultsList);
    }
}
