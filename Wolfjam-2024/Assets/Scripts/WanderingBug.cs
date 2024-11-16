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
    public float centerBiasStrength = 0.5f; // Strength of bias towards the center (0 = no bias, 1 = full bias)
    private float approachTimer;
    public float approachDuration = 2f; // Time in seconds to move toward the center

    
    public Sprite squashedBugSprite;           // Reference to squashed bug sprite
    public float fadeDuration = 1.5f;          // Time it takes to fade out

    private Vector2 targetDirection; // Current target direction
    private Vector2 currentDirection; // Smoothed current direction
    private float directionChangeTimer;
    private Vector3 lastPosition;              // To calculate actual movement direction
    private bool isSquashed = false;           // To prevent movement after being squashed
    public GameObject squashedBugPrefab;     // Reference to the squashed bug prefab
    private Vector3 screenCenter;

        private enum State
    {
        ApproachingCenter,
        Wandering
    }
    private State currentState;

    void Start()
    {
        // Initialize directions
        currentDirection = Random.insideUnitCircle.normalized;
        targetDirection = currentDirection;
        directionChangeTimer = directionChangeInterval;

        // Calculate the center of the screen in world coordinates
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

        // Start in the ApproachingCenter state
        currentState = State.ApproachingCenter;

        // Initialize approach timer
        approachTimer = approachDuration;
    }

    void Update()
    {
        speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.PingPong(Time.time, 1)); // Smoothly oscillate speed

        if (currentState == State.ApproachingCenter)
        {
            ApproachCenter();
        }
        else if (currentState == State.Wandering)
        {
            WanderRandomly();
        }

        // Ensure the bug stays within screen bounds
        StayInBounds();
    }

    void ApproachCenter()
    {
        // Calculate the direction to the center
        Vector2 toCenter = (screenCenter - transform.position).normalized;
        currentDirection = Vector2.Lerp(currentDirection, toCenter, turnSpeed * Time.deltaTime);

        // Rotate bug to face movement direction
        float angle = (-.5f * Mathf.PI + Mathf.Atan2(currentDirection.y, currentDirection.x)) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), turnSpeed * Time.deltaTime);

        speed = 3f; // Constant speed when approaching the center

        // Move the bug
        transform.Translate(currentDirection * speed * Time.deltaTime, Space.World);

        // Count down the approach timer
        approachTimer -= Time.deltaTime;


        // End the approach phase when the timer runs out
        if (approachTimer <= 0)
        {
            currentState = State.Wandering;
        }
    }
    void WanderRandomly()
    {
        if (isSquashed) return; // Stop movement if the bug is squashed

        speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.PingPong(Time.time, 1)); // Smoothly oscillate speed
        
        // Update direction periodically
        directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer <= 0)
        {
            directionChangeTimer = directionChangeInterval;
            
            // Calculate bias towards the center
            Vector2 toCenter = (screenCenter - transform.position).normalized * centerBiasStrength;
            Vector2 randomDirection = Random.insideUnitCircle.normalized * (1 - centerBiasStrength);

            // Combine center bias and random direction
            targetDirection = (toCenter + randomDirection).normalized;
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
        if (isSquashed) return; // Prevent multiple clicks from triggering

        // Stop movement
        isSquashed = true;

        // Instantiate the squashed bug prefab
        GameObject squashedBug = Instantiate(squashedBugPrefab, transform.position, Quaternion.identity);
        squashedBug.transform.SetParent(transform.parent); // Set the same parent as the original bug

        // Destroy the original bug
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
