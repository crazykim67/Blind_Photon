using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DummyNetworkManager : MonoBehaviourPunCallbacks
{
    #region Instance

    private static DummyNetworkManager instance;

    public static DummyNetworkManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new DummyNetworkManager();
                return instance;
            }
            return instance;
        }
    }

    #endregion

    private const string ROOM_CODE = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private int playerCount = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        OnConnect();
    }

    #region Connect & Disconnect

    public void OnConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnDisconnect()
    {
        PhotonNetwork.Disconnect();
    }

    // 서버 연결 콜백
    public override void OnConnected()
    {
        Debug.Log("Connect to Successful");
    }

    #endregion

    #region RoomCreate

    public void OnCreate(int _maxPlayer)
    {
        OnCreateRoom(CreateRoomCode(), _maxPlayer);
    }

    public void OnCreateRoom(string _roomName, int _maxPlayer)
    {
        RoomOptions options = new RoomOptions { MaxPlayers = _maxPlayer };
        PhotonNetwork.CreateRoom(_roomName, options);
        playerCount = _maxPlayer;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"RoomName : {PhotonNetwork.CurrentRoom.Name}\n MaxPlayer : {PhotonNetwork.CurrentRoom.MaxPlayers}\n Room Create Successful");
        playerCount = 0;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if(returnCode == 32766)
        {
            Debug.Log("방제목 중복 재 생성합니다.");

            OnCreateRoom(CreateRoomCode(), playerCount);
        }
    }

    public string CreateRoomCode()
    {
        var sb = new System.Text.StringBuilder(5);
        var r = new System.Random();

        for (int i = 0; i < 5; i++)
        {
            int pos = r.Next(ROOM_CODE.Length);
            char c = ROOM_CODE[pos];
            sb.Append(c);
        }

        return sb.ToString();
    }

    #endregion
}
