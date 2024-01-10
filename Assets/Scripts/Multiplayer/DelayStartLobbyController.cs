using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class DelayStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject cancelButton;

    [SerializeField]
    private byte roomSize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
    }

    public void DelayStart()
    {
        startButton.SetActive(false);
        cancelButton.SetActive(true);

        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Delay start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        CreateRoom();
    }

    private void CreateRoom()
    {
        Debug.Log("Creating room");

        int randomRoomNum = Random.Range(0, 10000); // Random room name
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = roomSize };
        PhotonNetwork.CreateRoom("Room" + randomRoomNum, roomOptions);

        Debug.Log(randomRoomNum);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom();
    }

    public void DelayCancel()
    {
        cancelButton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
