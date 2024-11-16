using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Camera cam;

    [SerializeField] private GridManager gridManager;

    [SerializeField] private Tray trayPrefab;

    public Camera Cam { get { return cam; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gridManager.GenerateGrid();

        var newTray = Instantiate(trayPrefab);
        newTray.transform.position = new Vector3(cam.transform.position.x - 2.0f, cam.transform.position.y - 4.5f, 0.0f);
        string[] gates = new string[] { "or", "or", "or" };
        newTray.AddGateComponents(gates);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
