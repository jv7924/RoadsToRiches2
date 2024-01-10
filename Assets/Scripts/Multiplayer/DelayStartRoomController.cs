using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;

public class DelayStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string waitingRoomScene;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);  // Register to photon callback functions
    }

    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene(waitingRoomScene);
    }
}
