using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    RaycastHit hit;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
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
        // Instantiate(road, hit.transform.position, Quaternion.identity);
        Destroy(hit.transform.gameObject);
        Destroy(gameObject);
        // Instantiate(road, eventData.position, Quaternion.identity);
    }
}
