using UnityEngine;
using UnityEngine.InputSystem;

public class Wire : MonoBehaviour
{
    public WireNode inputNode;
    public WireNode outputNode;
    public bool internalState;

    private Controls controls;
    private InputAction point;
    private Vector2 mousePos;

    [SerializeField] private LineRenderer lineRenderer;
    private GameManager gameManager;

    private MeshCollider meshCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        this.inputNode = null;
        this.outputNode = null;

        controls = new Controls();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        point = controls.UI.Point;
        point.Enable();

        controls.Player.Release.performed += ReleaseWire;
        controls.Player.Release.Enable();
    }

    private void OnDisable()
    {
        point.Disable();
        controls.Player.Release.Disable();
    }

    // Update is called once per frame
    void Update()
    {

        mousePos = gameManager.Cam.ScreenToWorldPoint(point.ReadValue<Vector2>());

        if (this.inputNode != null && this.outputNode != null)
        {
            this.outputNode.internalState = this.inputNode.internalState;
            lineRenderer.SetPosition(0, inputNode.transform.position + (Vector3)inputNode.GetComponent<BoxCollider2D>().offset);
            lineRenderer.SetPosition(1, outputNode.transform.position + (Vector3)outputNode.GetComponent<BoxCollider2D>().offset);

            if (meshCollider == null)
            {
                Mesh lineBakedMesh = new Mesh(); //Create a new Mesh (Empty at the moment)
                lineRenderer.BakeMesh(lineBakedMesh, true); //Bake the line mesh to our mesh variable
                lineRenderer.GetComponent<MeshCollider>().sharedMesh = lineBakedMesh; //Set the baked mesh to the MeshCollider
                lineRenderer.GetComponent<MeshCollider>().convex = true; //You need it convex if the mesh have any kind of holes
                //lineRenderer.GetComponent<MeshCollider>().includeLayers = LayerMask.GetMask("Line");

                //lineRenderer = null;
            }
        }

        if (this.inputNode != null ^ this.outputNode != null)
        {
            lineRenderer.positionCount = 2;
            if (inputNode != null)
            {
                lineRenderer.SetPosition(0, inputNode.transform.position + (Vector3)inputNode.GetComponent<BoxCollider2D>().offset);
                lineRenderer.SetPosition(1, mousePos);
            }
            else
            {
                lineRenderer.SetPosition(0, mousePos);
                lineRenderer.SetPosition(1, outputNode.transform.position + (Vector3)outputNode.GetComponent<BoxCollider2D>().offset);
            }

            //Mesh lineBakedMesh = new Mesh(); //Create a new Mesh (Empty at the moment)
            //lineRenderer.BakeMesh(lineBakedMesh, true); //Bake the line mesh to our mesh variable
            //lineRenderer.GetComponent<MeshCollider>().sharedMesh = lineBakedMesh; //Set the baked mesh to the MeshCollider
            //lineRenderer.GetComponent<MeshCollider>().convex = true; //You need it convex if the mesh have any kind of holes
            //lineRenderer.GetComponent<MeshCollider>().includeLayers = LayerMask.GetMask("Line");

            //lineRenderer = null;

        }

        if (this.inputNode != null && this.outputNode != null){
            this.inputNode.internalState = this.outputNode.internalState;
        }

    }

    public void setNode(WireNode node)
    {
        if (node.io == NodeType.In)
        {
            inputNode = node;
        }
        else
        {
            outputNode = node;
        }
    }

    public void RenderLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, inputNode.transform.position + (Vector3)inputNode.GetComponent<BoxCollider2D>().offset);
        lineRenderer.SetPosition(1, outputNode.transform.position + (Vector3)outputNode.GetComponent<BoxCollider2D>().offset);
    }

    public void ReleaseWire(InputAction.CallbackContext context)
    {
        if (this.inputNode != null ^ this.outputNode != null)
        {
            if (this.inputNode != null)
            {
                this.inputNode.RemoveWire();
                this.inputNode = null;
            }
            else
            {
                this.outputNode.RemoveWire();
                this.outputNode = null;
            }

            gameManager.hasWire = false;
            gameManager.currentWire = null;

            Destroy(this.gameObject);
        }

        else if (this.inputNode != null && this.outputNode != null)
        {
            if (Physics.Raycast(new Vector3(mousePos.x, mousePos.y, -30.0f), Vector3.forward, Mathf.Infinity))
            {
                this.inputNode.RemoveWire();
                this.inputNode = null;

                this.outputNode.RemoveWire();
                this.outputNode = null;

                gameManager.hasWire = false;
                gameManager.currentWire = null;

                Destroy(this.gameObject);
            }
        }
    }
}
