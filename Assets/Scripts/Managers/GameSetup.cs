using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();

        if (!player.GetPhotonView().IsMine)
        {
            player.SetActive(false);
        }
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        player = PhotonNetwork.Instantiate(Path.Combine("Photon Prefabs", "Player"), Vector3.zero, Quaternion.identity);
    }
}
