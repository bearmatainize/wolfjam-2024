using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum ComponentState { Stashed, Grabbed, Placed };

public class Component : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [SerializeField] private Color highlightColor;
    [SerializeField] private SpriteRenderer sprite;

    private Controls controls;

    private InputAction point;

    private ComponentState currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = ComponentState.Stashed;
    }

    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        point = controls.UI.Point;
        point.Enable();
    }

    void OnDisable()
    {
        point.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == ComponentState.Grabbed)
        {
            transform.position = point.ReadValue<Vector2>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering");
        //sprite.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sprite.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
