using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Card : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Sprite cardFace;
    [SerializeField] private Road road;
    private ObjectSpawner spawner;
    public GameObject player;
    public GameObject hand;
    private Vector3 startPos;
    private Canvas canvas;
    private RectTransform rectTransform;
    private RaycastHit hit;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {

        rectTransform = GetComponent<RectTransform>();
        canvas = player.GetComponent<Canvas>();
        spawner = GetComponent<ObjectSpawner>();

        hand = transform.parent.gameObject;
        player = transform.parent.parent.gameObject;
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
        this.gameObject.transform.SetParent(player.transform, true);
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
        Debug.Log("Drop");
        if (hit.transform == null)
            this.gameObject.transform.SetParent(hand.transform);
    
        InstantiateRoad(hit.transform.position);
        Destroy(hit.transform.gameObject);
        Destroy(gameObject);
    }

    public void InstantiateRoad(Vector3 position)
    {
        spawner.SpawnObject(road.gameObject, position, Quaternion.identity);
    }
}
