using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectSpawner : MonoBehaviour
{
    // [HideInInspector]
    public PhotonView photonView;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        photonView.GetComponent<PhotonView>();
    }

    public void SpawnObject(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        Instantiate(gameObject, position, rotation);
    }

    [PunRPC]
    public void RPC_SpawnObject(string roadSerialized, Vector3 position, Quaternion rotation)
    {
        Road road = JsonUtility.FromJson<Road>(roadSerialized);

        Instantiate(road.gameObject, position, rotation);
    }

}
