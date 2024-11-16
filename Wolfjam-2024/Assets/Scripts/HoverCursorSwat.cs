using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCursorSwat : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public Texture2D swatCursor;  // Drag your custom cursor texture here
    public Vector2 hotspot = Vector2.zero; // Sets the cursor's "click point"
    private Texture2D defaultCursor;

    public AudioClip[] clips; // Array of audio clips to play when the object is clicked
    private AudioSource audioSource;
    
    void Start()
    {
        // Save the current default cursor (Unity uses the system cursor by default)
        defaultCursor = null; // Set to null to indicate no custom cursor

        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure AudioSource is enabled
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.enabled = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change the cursor when the pointer enters the object
        Cursor.SetCursor(swatCursor, hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert to the default cursor when the pointer exits
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Check if there are clips in the array
        if (clips.Length > 0)
        {
            Debug.Log("Playing audio clip");
            // Choose a random clip from the array
            int randomIndex = Random.Range(0, clips.Length);
            audioSource.clip = clips[randomIndex];
            
            // Play the clip at the current position of the GameObject
            AudioSource.PlayClipAtPoint(clips[randomIndex], transform.position);
        }
    }
}
