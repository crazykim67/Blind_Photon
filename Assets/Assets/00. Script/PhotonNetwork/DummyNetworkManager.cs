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

    // ���� ���� �ݹ�
    public override void OnConnected()
    {
        Debug.Log("Connect to Successful");
    }

    #endregion
}
