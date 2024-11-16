using UnityEngine;

public class Wire : MonoBehaviour
{
    public WireNode inputNode;
    public WireNode outputNode;
    public bool internalState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.inputNode = null;
        this.outputNode = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.inputNode != null && this.outputNode != null){
            this.outputNode.internalState = this.inputNode.internalState;
        }
        
    }

    public void setNode(WireNode node){
        if (node.io == NodeType.In){
            inputNode = node;
        } else {
            outputNode = node;
        }
    }
}
