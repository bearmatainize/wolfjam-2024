using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Duck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public Sprite closed;
    [SerializeField] public Sprite open;
    [SerializeField] public Sprite blink;
    [SerializeField] private SpriteRenderer sprite;

    private AudioSource audioSource;

    float timer;
    bool openTimer;
    void Start()
    {
        timer = 0.4f;
        openTimer = false;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (openTimer) {
            timer -= Time.deltaTime;
            if (timer < 0.0f) {
                sprite.sprite = closed;
                openTimer = false;
                timer = 0.4f;
            }
        }
        
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sprite.sprite = blink;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!openTimer) {
            sprite.sprite = closed;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        swap();
    }

    public void swap(){
        
        sprite.sprite = open;

        audioSource.Play(0);
        
        openTimer = true;
        
    }
}
