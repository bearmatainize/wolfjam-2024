using UnityEngine;
using UnityEngine.EventSystems;

public class StartTile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private WireNode output1;

    [SerializeField] public Sprite OneImage;
    [SerializeField] public Sprite ZeroImage;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GateComponent gateComponent;

    public GateComponent GateComponent { get { return gateComponent; } }


    private void Awake()
    {
        output1 = GetComponentsInChildren<WireNode>()[0];
        output1.io = NodeType.Out;
        output1.currentState = WireNodeState.Off;
        sprite.sprite = ZeroImage;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gateComponent.transform.localPosition = new Vector3(0f, 0f, 1.0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Swap();
    }

    void Swap()
    {
        if (output1.currentState == WireNodeState.Off)
        {
            output1.currentState = WireNodeState.On;
            sprite.sprite = OneImage;
        }
        else
        {
            output1.currentState = WireNodeState.Off;
            sprite.sprite = ZeroImage;
        }
    }
}
