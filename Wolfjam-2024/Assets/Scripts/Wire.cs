using UnityEngine;
using UnityEngine.InputSystem;

public class Wire : MonoBehaviour
{
    public WireNode inputNode;
    public WireNode outputNode;
    public bool internalState;

    private Controls controls;
    private InputAction point;

    [SerializeField] private LineRenderer lineRenderer;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        this.inputNode = null;
        this.outputNode = null;

        controls = new Controls();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (this.inputNode != null && this.outputNode != null)
        {
            this.outputNode.internalState = this.inputNode.internalState;
        }

        if (this.inputNode != null ^ this.outputNode != null)
        {
            Vector2 mousePos = gameManager.Cam.ScreenToWorldPoint(point.ReadValue<Vector2>());
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
    }
}
