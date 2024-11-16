using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum ComponentState { Stashed, Grabbed, Placed, Locked };

public class GateComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Color highlightColor;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GridManager gridManager;

    private Vector3 originalPosition;

    private Controls controls;

    private InputAction point;

    [SerializeField] private ComponentState currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
        //Debug.Log(transform.position);
    }

    void Awake()
    {
        controls = new Controls();
        currentState = ComponentState.Stashed;
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
        Debug.Log(currentState.ToString());
        if (currentState == ComponentState.Grabbed)
        {
            transform.position = gameManager.Cam.ScreenToWorldPoint(point.ReadValue<Vector2>());
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hovering");
        //sprite.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sprite.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Pointer Down");
        if (currentState != ComponentState.Locked)
        {
            currentState = ComponentState.Grabbed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentState != ComponentState.Locked)
        {
            Debug.Log("Pointer Up");
            CheckIfOnTile();
        }
    }

    private void CheckIfOnTile()
    {
        List<Tile> tiles = gridManager.AllTiles;
        float shortestDist = Mathf.Infinity;
        Tile closestTile = null;
        foreach (Tile tile in tiles)
        {
            if (!tile.HasAttachedComponent())
            {
                float dist = Vector3.SqrMagnitude(transform.position - tile.transform.position);
                if (dist < shortestDist)
                {
                    closestTile = tile;
                    shortestDist = dist;
                }
            }
        }

        Debug.Log("Shortest Dist: " + shortestDist);

        if (shortestDist > 1.5f)
        {
            currentState = ComponentState.Stashed;
            transform.position = originalPosition;
            Debug.Log(transform.position);

        }
        else
        {
            currentState = ComponentState.Placed;
            transform.position = closestTile.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
            closestTile.AttachComponent(this, false);
        }
    }

    public void LockComponent()
    {
        currentState = ComponentState.Locked;
    }

    public void SetOriginalPosition()
    {
        originalPosition = transform.position;
    }
}
