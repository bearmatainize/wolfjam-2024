using UnityEngine;

public enum WireNodeState { Disconnected, Off, On };
public enum NodeType {In, Out};

public class WireNode : MonoBehaviour
{

    public WireNodeState currentState;
    public bool internalState;
    public NodeType io;
    public GateComponent parentComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
