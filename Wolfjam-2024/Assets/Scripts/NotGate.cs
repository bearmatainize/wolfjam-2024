using UnityEngine;

public class NotGate : MonoBehaviour
{
    private WireNode input1;
    private WireNode output1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        WireNode[] nodes = GetComponentsInChildren<WireNode>();
        this.input1 = nodes[0];
        this.output1 = nodes[1];

    }

    // Update is called once per frame
    void Update()
    {
        if (this.output1.currentState != WireNodeState.Disconnected){
            if (!this.input1.internalState){
                this.output1.currentState = WireNodeState.On;
            } else {
                this.output1.currentState = WireNodeState.Off;
            }
        } 
    }
}
