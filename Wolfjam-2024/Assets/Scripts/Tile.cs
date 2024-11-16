using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] private Color baseColor, offsetColor, highlightColor;
    private Color nonHighlightColor;
    [SerializeField] private SpriteRenderer renderer;

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
        renderer.color = isOffset ? offsetColor : baseColor;
        nonHighlightColor = renderer.color;
    }

    void OnMouseEnter()
    {
        renderer.color = Color.red;
    }

    void OnMouseExit()
    {
        renderer.color = nonHighlightColor;
    }
}
