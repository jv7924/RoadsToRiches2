using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

using Photon.Pun;
using Photon.Realtime;

public class DelayStartWaitingRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private string multiplayerScene;
    [SerializeField]
    private string menuScene;
    [SerializeField]
    private int minPlayersToStart;
    [SerializeField]
    private TMP_Text playerCountDisplay;
    [SerializeField]
    private TMP_Text timerToStartDisplay;
    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGameWaitTime;
    private PhotonView myPhotonView;
    private int playerCount;
    private int roomSize;
    private float timeToStartGame;
    private float notFullGameTimer;
    private float fullGameTimer;
    private bool readyToCountDown;
    private bool readyToStart;
    private bool startingGame;

    // Start is called before the first frame update
    void Start()
    {
        myPhotonView = GetComponent<PhotonView>();
        ResetTimer();

        PlayerCountUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        WaitingForPlayers();
    }

    private void PlayerCountUpdate()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        playerCountDisplay.text = playerCount + "/" + roomSize;

        if (playerCount == roomSize)
        {
            readyToStart = true;
        }
        else if (playerCount >= minPlayersToStart)
        {
            readyToCountDown = true;
        }
        else 
        {
            readyToCountDown = false;
            readyToStart = false;
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float timeIn)
    {
        timeToStartGame = timeIn;
        notFullGameTimer = timeIn;

        if (timeIn < fullGameTimer)
        {
            fullGameTimer = timeIn;
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        PlayerCountUpdate();

        if (PhotonNetwork.IsMasterClient)
        {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timeToStartGame);
        }
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    private void WaitingForPlayers()
    {
        // if (playerCount <= 1)
        // {
        //     ResetTimer();
        // }

        if (readyToStart)
        {
            fullGameTimer -= Time.deltaTime;
            timeToStartGame = fullGameTimer;
        }
        else if (readyToCountDown)
        {
            notFullGameTimer -= Time.deltaTime;
            timeToStartGame = notFullGameTimer;
        }

        string tempTimer = string.Format("{0:00}", timeToStartGame);
        timerToStartDisplay.text = tempTimer;

        if (timeToStartGame <= 0f)
        {
            if (startingGame)
                return;
            
            StartGame();
        }
    }

    private void ResetTimer()   
    {
        timeToStartGame = maxWaitTime;
        notFullGameTimer = maxWaitTime;
        fullGameTimer = maxFullGameWaitTime;
    }

    private void StartGame()
    {
        startingGame = true;

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(multiplayerScene);
    }

    public void DelayCancel()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuScene);
    }
}
