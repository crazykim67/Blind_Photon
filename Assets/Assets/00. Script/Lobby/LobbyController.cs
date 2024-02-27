using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public static LobbyController Instance;

    [SerializeField]
    private TextMeshProUGUI roomCodeText;

    public static List<PlayerPun> playerList = new List<PlayerPun>();

    public override void OnEnable()
    {
        base.OnEnable();

        Init();
        PhotonNetwork.Instantiate("Player", new Vector3(0, 1, 0), Quaternion.identity);
    }

    private void Init()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        if (!PhotonNetwork.InRoom)
            return;

        if (PhotonNetwork.IsMasterClient)
            roomCodeText.text = $"Room Code : {PhotonNetwork.CurrentRoom.Name}";
        else
            roomCodeText.gameObject.SetActive(false);
    }

    #region Player Get/Set

    public static void AddController(PlayerPun controller)
    {
        if (controller == null)
            return;

        playerList.Add(controller);
    }

    private void RemoveController(PlayerPun controller)
    {
        if (controller == null)
            return;

        playerList.Remove(controller);
    }

    public static PlayerPun GetPlayer()
    {
        PlayerPun _player = null;

        foreach (var player in playerList)
        {
            if (player.nickName.Equals(PhotonNetwork.NickName))
            {
                _player = player;
                break;
            }
        }

        return _player;
    }

    #endregion

    #region PunCallbacks

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        foreach(var player in playerList)
        {
            if (player.nickName.Equals(otherPlayer.NickName))
            {
                RemoveController(player);
                break;
            }
        }
    }

    #endregion

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        PhotonManager.Instance.OnLeaveRoom();
    }
}
