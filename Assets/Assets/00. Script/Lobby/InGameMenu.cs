using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    #region

    private static InGameMenu instane;

    public static InGameMenu Instance
    {
        get
        {
            if(instane == null)
            {
                instane = new InGameMenu();
                return instane;
            }
            return instane;
        }
    }

    #endregion

    [SerializeField]
    private GameObject menuObj;

    private bool isMenu = false;

    public bool IsMenu { get { return isMenu; } }

    [Header("Exit Group")]
    [SerializeField]
    private GameObject exitObj;
    [SerializeField]
    private Button yesBtn;
    [SerializeField]
    private Button noBtn;

    private void Awake()
    {
        instane = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenu)
                OnShow();
            else
                OnHide();
        }
    }

    public void OnShow()
    {
        menuObj.SetActive(true);
        isMenu = true;
    }

    public void OnHide()
    {
        menuObj.SetActive(false);
        isMenu = false;

        if (OptionManager.Instance.IsOption)
            OptionManager.Instance.OnHide();

        exitObj.SetActive(false);
    }

    public void OnOption()
    {
        if (OptionManager.Instance == null)
            return;

        OptionManager.Instance.OnShow();
    }

    public void OnExit()
    {
        PhotonManager.Instance.OnLeaveRoom();
    }
}
