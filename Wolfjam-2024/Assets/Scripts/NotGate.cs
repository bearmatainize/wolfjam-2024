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
        this.output1.internalState = !this.input1.internalState;
    }
}
