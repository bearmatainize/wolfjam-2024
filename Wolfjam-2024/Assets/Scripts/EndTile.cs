using UnityEngine;

public class EndTile : MonoBehaviour
{
    private WireNode input1;

    [SerializeField] public Sprite OneImage;
    [SerializeField] public Sprite ZeroImage;
    [SerializeField] public Sprite DCImage;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GateComponent gateComponent;

    public GateComponent GateComponent { get { return gateComponent; } }


    private void Awake()
    {
        input1 = GetComponentsInChildren<WireNode>()[0];
        input1.io = NodeType.In;
        input1.currentState = WireNodeState.Disconnected;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChangeEndValue();
    }

    void ChangeEndValue()
    {
        if (input1.currentState == WireNodeState.Disconnected)
        {
            sprite.sprite = DCImage;
        }
        else if (input1.currentState == WireNodeState.Off)
        {
            sprite.sprite = ZeroImage;
        }
        else
        {
            sprite.sprite = OneImage;
        }
    }
}
