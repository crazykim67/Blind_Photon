using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Text;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region Instance

    private static PhotonManager instance;

    public static PhotonManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PhotonManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    private const string ROOM_CODE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private int playerCount = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        OnConnect();
        PhotonNetwork.NickName = NickName();
    }

    public void OnConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnected()
    {
        Debug.Log("Connect to Successful...!");
    }

    public void OnCreateRoom(int maxPlayer)
    {
        PhotonNetwork.CreateRoom(RoomCode(), new RoomOptions { MaxPlayers = maxPlayer });
        playerCount = maxPlayer;
    }

    private string RoomCode()
    {
        var sb = new StringBuilder(5);
        var r = new System.Random();

        for(int i = 0; i < 5; i++)
        {
            int pos = r.Next(ROOM_CODE.Length);
            char c = ROOM_CODE[pos];
            sb.Append(c);
        }

        return sb.ToString();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room Successful...!");
        OnLoadScene("Lobby");
        playerCount = 0;
    }

    public void OnLoadScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (returnCode == 32766)
            OnCreateRoom(playerCount);
    }

    public void OnJoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnLeaveRoom()
    {
        if (!PhotonNetwork.InRoom)
            return;

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        OnLoadScene("MainMenu");
    }

    #region Dummy

    private string NickName()
    {
        string nick = "0123456789";

        var sb = new StringBuilder(4);
        var r = new System.Random();

        for (int i = 0; i < 4; i++)
        {
            int pos = r.Next(nick.Length);
            char c = nick[pos];
            sb.Append(c);
        }

        return "Player" + sb.ToString();
    }

    #endregion
}
