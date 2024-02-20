using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class HostUI : MonoBehaviour
{
    [SerializeField]
    private Button hostBackBtn; 
    [SerializeField]
    private TextMeshProUGUI countText;
    [SerializeField]
    private int playerCount = 2;
    [SerializeField]
    private Button createBtn;


    public void SetActive(bool isAct)
    {
        this.gameObject.SetActive(isAct);
        Init();
    }
     
    public void Init()
    {
        PlayerCountInit();
    }

    private void PlayerCountInit()
    {
        playerCount = 2;
        countText.text = playerCount.ToString();
    }

    public void OnPlayerCountChanged(bool isPlus)
    {
        playerCount = Mathf.Clamp(playerCount + (isPlus ? 1 : -1), 1, 4);
        countText.text = playerCount.ToString();
    }

    public void OnCreate()
    {
        PhotonManager.Instance.OnCreateRoom(playerCount);
    }
}
