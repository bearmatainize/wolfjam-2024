using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [SerializeField] private Color baseColor, offsetColor, highlightColor;
    private Color nonHighlightColor;
    [SerializeField] private SpriteRenderer sprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitializeColor(bool isOffset)
    {
        sprite.color = isOffset ? offsetColor : baseColor;
        nonHighlightColor = sprite.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sprite.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sprite.color = nonHighlightColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}
