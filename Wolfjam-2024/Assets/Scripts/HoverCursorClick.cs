using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCursorClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Texture2D clickCursor;  // Drag your custom cursor texture here
    public Vector2 hotspot = new Vector2(11.0f, 1.0f); // Sets the cursor's "click point"
    private Texture2D defaultCursor;
    
    void Start()
    {
        // Save the current default cursor (Unity uses the system cursor by default)
        defaultCursor = null; // Set to null to indicate no custom cursor
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the cursor when the pointer enters the object
        Cursor.SetCursor(clickCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert to the default cursor when the pointer exits
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
