using UnityEngine;
using UnityEngine.EventSystems;

public enum WireNodeState { Disconnected, Off, On };
public enum NodeType { In, Out };

public class WireNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public WireNodeState currentState;
    public bool internalState;
    public NodeType io;
    public GateComponent parentComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localPosition = new Vector3(0f, 0f, -1.0f);
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
        Debug.Log("CLICK!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ON NODE");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    
    }
}
