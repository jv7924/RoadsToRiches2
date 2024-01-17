using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class ObjectSpawner : MonoBehaviour
{
       public PhotonView photonView;

    public void SpawnObject(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        Instantiate(gameObject, position, rotation);
    }

    [PunRPC]
    public void RPC_SpawnObject(string prefabName, Vector3 position, Quaternion rotation)
    {
        // RoadData roadData = JsonUtility.FromJson<RoadData>(roadSerialized);
        // Road road;
        // road

        PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Roads", prefabName), position, rotation);
    }
}
