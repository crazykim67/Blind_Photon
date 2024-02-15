using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DummyNetworkManager : MonoBehaviourPunCallbacks
{
    #region Connect & Disconnect

    public void OnConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void OnDisconnect()
    {
        PhotonNetwork.Disconnect();
    }

    // ���� ���� �ݹ�
    public override void OnConnected()
    {
        Debug.Log("Connect to Successful");
    }

    #endregion
}
