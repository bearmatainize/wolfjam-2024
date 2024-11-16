using UnityEngine;

public class Wire : MonoBehaviour
{
    public WireNode inputNode;
    public WireNode outputNode;
    public bool internalState;

    [SerializeField] private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        this.inputNode = null;
        this.outputNode = null;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.inputNode != null && this.outputNode != null)
        {
            this.outputNode.internalState = this.inputNode.internalState;
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
}
