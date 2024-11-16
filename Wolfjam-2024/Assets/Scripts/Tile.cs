using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject HoverObject;

    private GateComponent attachedComponent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HoverObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeColor(bool isOffset)
    {
        sprite.color = isOffset ? offsetColor : baseColor;
        //nonHighlightColor = sprite.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //sprite.color = nonHighlightColor;
        HoverObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void AttachComponent(GateComponent component, bool locked)
    {
        attachedComponent = component;

        if (locked)
        {
            component.LockComponent();
        }
    }

    public void DetachComponent()
    {
        attachedComponent = null;
    }

    public bool HasAttachedComponent()
    {
        return attachedComponent != null;
    }
}
