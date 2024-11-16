using UnityEngine;

public class XnorGate : MonoBehaviour
{
    private WireNode input1;
    private WireNode input2;
    private WireNode output1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        WireNode[] nodes = GetComponentsInChildren<WireNode>();
        this.input1 = nodes[0];
        this.input2 = nodes[1];
        this.output1 = nodes[2];

    }

    // Update is called once per frame
    void Update()
    {
        this.output1.internalState = ! (this.input1.internalState ^ this.input2.internalState);
    }
}
