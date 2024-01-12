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
            this.gameObject.transform.SetParent(hand.transform);
    
        InstantiateRoad(hit.transform.position);
        Destroy(hit.transform.gameObject);
        Destroy(gameObject);
    }

    public void InstantiateRoad(Vector3 position)
    {
        // spawner.SpawnObject(road.gameObject, position, Quaternion.identity);

        spawner.photonView.RPC("RPC_SpawnObject", RpcTarget.All, GetSerialzedRoad(), position, Quaternion.identity);
    }

    private string GetSerialzedRoad()
    {
        return JsonUtility.ToJson(road);
    }
}
