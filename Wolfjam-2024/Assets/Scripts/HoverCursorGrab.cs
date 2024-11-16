using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCursorGrab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    public Texture2D grabCursor;
    public Texture2D grabbedCursor;
    public Vector2 hotspot = Vector2.zero; // Sets the cursor's "click point"
    private Texture2D defaultCursor;
    [SerializeField] private GateComponent gateComponent;

    private bool isClicking = false;    // Tracks if the user is currently clicking/holding
    
    void Start()
    {
        // Save the current default cursor (Unity uses the system cursor by default)
        defaultCursor = null; // Set to null to indicate no custom cursor
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(gateComponent.CurrentState != ComponentState.Locked && !isClicking)
        {
            // Change the cursor when the pointer enters the object
            Cursor.SetCursor(grabCursor, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicking)
        {
            // Revert to default cursor when mouse exits
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(gateComponent.CurrentState != ComponentState.Locked)
        {
            // Change to click/hold cursor when the object is clicked
            isClicking = true;
            Cursor.SetCursor(grabbedCursor, hotspot, CursorMode.Auto);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Revert to hover cursor when the mouse button is released
        isClicking = false;

        // Check if the mouse is still over the object
        if (eventData.pointerEnter == gameObject)
        {
            Cursor.SetCursor(grabCursor, hotspot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        }
    }
}
