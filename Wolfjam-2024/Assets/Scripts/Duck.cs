using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Duck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public Sprite closed;
    [SerializeField] public Sprite open;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private AudioSource audioData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        swap();
    }

    public void swap(){
        sprite.sprite = open;
        new WaitForSecondsRealtime(1);
        sprite.sprite = closed;
    }
}
