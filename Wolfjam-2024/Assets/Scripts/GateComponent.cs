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

    public Tile currentTile;

    private Vector3 originalPosition;

    private Controls controls;

    private InputAction point;

    [SerializeField] private ComponentState currentState;

    public ComponentState CurrentState { get { return currentState; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gridManager = GameObject.Find("GridManager").GetComponent<GridManager>();
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
        if (currentState == ComponentState.Grabbed)
        {
            transform.position = gameManager.Cam.ScreenToWorldPoint(point.ReadValue<Vector2>());
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("In Gate Component");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sprite.color = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState != ComponentState.Locked)
        {
            currentState = ComponentState.Grabbed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (currentState != ComponentState.Locked)
        {
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
                float dist = Vector2.SqrMagnitude(transform.position - tile.transform.position);
                if (dist < shortestDist)
                {
                    closestTile = tile;
                    shortestDist = dist;
                }
            }
        }

        if (shortestDist > 1.5f)
        {
            currentState = ComponentState.Stashed;
            transform.parent.gameObject.transform.position = new Vector3(originalPosition.x, originalPosition.y, -2.0f);
            transform.localPosition = new Vector3(0, 0, -1.0f);
            if (currentTile != null)
            {
                currentTile.DetachComponent();
            }

        }
        else
        {
            currentState = ComponentState.Placed;

            transform.parent.gameObject.transform.position = closestTile.transform.position;
            transform.parent.gameObject.transform.position = new Vector3(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y, -2.0f);
            transform.position = new Vector3(transform.parent.gameObject.transform.position.x, transform.parent.gameObject.transform.position.y, -1.0f);
            closestTile.AttachComponent(this, false);
            if (currentTile != null)
            {
                currentTile.DetachComponent();
            }
            currentTile = closestTile;
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
