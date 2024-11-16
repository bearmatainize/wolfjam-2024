using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WanderingBug : MonoBehaviour, IPointerDownHandler
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    public float speed; // Current speed
    public float directionChangeInterval = 1f; // Time in seconds to change direction
    public float turnSpeed = 2f;      // How quickly the bug turns

    private Vector2 targetDirection; // Current target direction
    private Vector2 currentDirection; // Smoothed current direction
    private float directionChangeTimer;
    private Vector3 lastPosition;              // To calculate actual movement direction

    void Start()
    {
        // Initialize directions
        currentDirection = Random.insideUnitCircle.normalized;
        targetDirection = currentDirection;
        directionChangeTimer = directionChangeInterval;
    }

    void Update()
    {
        speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.PingPong(Time.time, 1)); // Smoothly oscillate speed
        
        // Update direction periodically
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0)
        {
            directionChangeTimer = directionChangeInterval;
            targetDirection = Random.insideUnitCircle.normalized; // Choose a new random direction
        }

        // Smoothly adjust the direction
        currentDirection = Vector2.Lerp(currentDirection, targetDirection, turnSpeed * Time.deltaTime);

        // Move the bug
        Vector3 newPosition = transform.position + (Vector3)(currentDirection * speed * Time.deltaTime);
        transform.position = newPosition;

        // Calculate the actual movement direction
        Vector2 movementDirection = (newPosition - lastPosition).normalized;

        // Rotate bug to face movement direction
        float angle = (-.5f * Mathf.PI + Mathf.Atan2(movementDirection.y, movementDirection.x)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        lastPosition = transform.position;

        // Ensure the bug stays within screen bounds
        StayInBounds();
        
    }

     public void OnPointerDown(PointerEventData eventData)
    {
        // Destroy the bug when clicked
        Debug.Log("Bug clicked!"); // Add this to confirm the click is detected
        Destroy(gameObject);
    }

    void StayInBounds()
    {
        Vector3 position = transform.position;

        // Calculate the screen bounds in world coordinates
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        // Clamp the position within the screen bounds
        position.x = Mathf.Clamp(position.x, screenBottomLeft.x, screenTopRight.x);
        position.y = Mathf.Clamp(position.y, screenBottomLeft.y, screenTopRight.y);

        // Update the object's position
        transform.position = position;

        // Reverse direction if hitting a boundary
        if (position.x == screenBottomLeft.x || position.x == screenTopRight.x)
            currentDirection.x = -currentDirection.x;
        if (position.y == screenBottomLeft.y || position.y == screenTopRight.y)
            currentDirection.y = -currentDirection.y;
    }
}
