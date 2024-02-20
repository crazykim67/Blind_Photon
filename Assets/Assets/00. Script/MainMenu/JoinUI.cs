using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinUI : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField codeInputField;

    public void SetActive(bool isAct)
    {
        this.gameObject.SetActive(isAct);
        JoinInit();
    }

    private void JoinInit()
    {
        codeInputField.text = "";
    }

    public void OnJoin()
    {
        if (codeInputField.text.Length != 5 || string.IsNullOrEmpty(codeInputField.text)) 
            return;

        PhotonManager.Instance.OnJoinRoom(codeInputField.text);
    }
}
