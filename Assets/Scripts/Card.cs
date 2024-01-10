using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Sprite cardFace;
    [SerializeField] private Road road;
    // private GameObject hand;
    private Canvas canvas;
    private RectTransform rectTransform;
    private RaycastHit hit;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        // hand = transform.parent.gameObject;
        canvas = GetComponentInParent<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag start");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Does the ray intersect any objects
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(Input.mousePosition, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(Input.mousePosition, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag end");
    }

    public void OnDrop(PointerEventData eventData)
    {
        InstantiateRoad(hit.transform.position);
        Destroy(hit.transform.gameObject);
        Destroy(gameObject);
    }

    public void InstantiateRoad(Vector3 position)
    {
        Instantiate(road, position, Quaternion.identity);
    }
}
