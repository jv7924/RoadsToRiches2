using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class ManagerOfGame : MonoBehaviourPunCallbacks
{
    [SerializeField] private string level;

    #region Photon Callbacks

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom
        }
    }

    #endregion



    #region Public methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion



    #region Private Methods

    private void LoadLevel()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            return;
        }

        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", level);
        PhotonNetwork.LoadLevel(level);
    }

    #endregion
}
