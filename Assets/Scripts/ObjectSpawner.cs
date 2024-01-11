using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public void SpawnObject(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        Instantiate(gameObject, position, rotation);
    }
}
