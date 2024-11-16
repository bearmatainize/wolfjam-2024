using UnityEngine;
using UnityEngine.EventSystems;

public enum WireNodeState { Disconnected, Off, On };
public enum NodeType { In, Out };

public class WireNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [SerializeField] public WireNodeState currentState;
    [SerializeField] public GameManager gameManager;
    [SerializeField] public Wire wirePrefab;

    public bool internalState;
    public NodeType io;
    public GateComponent parentComponent;
    public LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localPosition = new Vector3(0f, 0f, -5.0f);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == WireNodeState.On)
        {
            internalState = true;
        }
        else
        {
            internalState = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameManager.hasWire)
        {
            WireNode attached = (gameManager.currentWire.inputNode == null) ? gameManager.currentWire.outputNode : gameManager.currentWire.inputNode;

            if (attached.io != this.io)
            {
                if (attached.io == NodeType.In)
                {
                    gameManager.currentWire.outputNode = this;
                }
                else
                {
                    gameManager.currentWire.inputNode = this;
                }
            }
            gameManager.currentWire.RenderLine();
            gameManager.hasWire = false;
            gameManager.currentWire = null;
        }
        else
        {
            gameManager.currentWire = Instantiate(wirePrefab);
            if (this.io == NodeType.In)
            {
                gameManager.currentWire.setNode(this);
            }
            else
            {
                gameManager.currentWire.setNode(this);
            }
            gameManager.hasWire = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
