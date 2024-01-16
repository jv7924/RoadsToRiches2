using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Card : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    [SerializeField] private Sprite cardFace;
    [SerializeField] private Road road;
    [SerializeField] private RoadData roadData;
    private ObjectSpawner spawner;
    private GameObject player;
    private GameObject hand;
    private Canvas canvas;
    private RectTransform rectTransform;
    private RaycastHit hit;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        hand = transform.parent.gameObject;
        player = hand.transform.parent.gameObject;

        rectTransform = GetComponent<RectTransform>();
        canvas = player.GetComponent<Canvas>();
        spawner = player.GetComponent<ObjectSpawner>();
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
        if (hit.transform == null || hit.transform.CompareTag("Road"))  
            gameObject.transform.SetParent(hand.transform);
    
        // AddToBoardArray(hit.transform);
        InstantiateRoad(hit.transform.position);
        Destroy(hit.transform.gameObject);
        Destroy(gameObject);
    }

    private void InstantiateRoad(Vector3 position)
    {
        spawner.SpawnObject(road.gameObject, position, Quaternion.identity);

        // spawner.photonView.RPC("RPC_SpawnObject", RpcTarget.All, GetSerialzedRoadData(), position, Quaternion.identity);
    }

    // private void AddToBoardArray(Transform transform)
    // {
    //     Board board = transform.GetComponentInParent<Board>();

    //     int x;
    //     int z;

    //     string[] coords = transform.gameObject.name.Split(' ');
    //     x = int.Parse(coords[1]);
    //     z = int.Parse(coords[2]);

    //     board.AddToArray(road.gameObject, z, x);
    // }

    private string GetSerialzedRoadData()
    {
        return JsonUtility.ToJson(roadData);
    }
}
