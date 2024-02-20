using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPun : PlayerController
{
    public string nickName;

    private void Start()
    {
        if (pv.IsMine)
        {
            pv.RPC("SetNickName", RpcTarget.AllBuffered, PhotonNetwork.NickName);
            pv.RPC("SetController", RpcTarget.AllBufferedViaServer);

            //StepPoolManager.Instance.SetTransform(this.transform);
        }
    }

    #region RPC

    [PunRPC]
    private void SetNickName(string _nickName)
    {
        nickName = _nickName;
    }

    [PunRPC]
    private void SetController()
    {
        LobbyController.AddController(this);
    }

    #endregion
}
