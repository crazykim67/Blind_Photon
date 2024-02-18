using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{   
    public enum State
    {
        Main,
        HostandJoin,
        Host,
        Join,
    }

    [Header("Main UI Group")]

    [SerializeField]
    private GameObject mainGroup;

    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private Button optionBtn;
    [SerializeField]
    private Button exitBtn;

    [Header("Host/Join UI Group")]

    [SerializeField]
    private GameObject hostJoinGroup;

    [SerializeField]
    private Button hostBtn;
    [SerializeField]
    private Button joinBtn;
    [SerializeField]
    private Button backBtn;

    [Header("Host UI Group")]
    [SerializeField]
    private HostUI hostUI;
    [SerializeField]
    private Button hostBackBtn;

    [Header("Join UI Group")]
    [SerializeField]
    private GameObject joinGroup;
    [SerializeField]
    private TMP_InputField codeInputField;
    [SerializeField]
    private Button roomJoinBtn;
    [SerializeField]
    private Button joinBackBtn;

    [Header("Exit Group")]
    [SerializeField]
    private GameObject exitGroup; 
    [SerializeField]
    private Button exitYesBtn;
    [SerializeField]
    private Button exitNoBtn;
    

    private void Awake()
    {
        startBtn.onClick.AddListener(() => { OnChange(State.HostandJoin); });
        backBtn.onClick.AddListener(() => { OnChange(State.Main); });

        hostBtn.onClick.AddListener(() => { OnChange(State.Host); });
        hostBackBtn.onClick.AddListener(() => { OnChange(State.HostandJoin); });

        joinBtn.onClick.AddListener(() => { OnChange(State.Join); });
        joinBackBtn.onClick.AddListener(() => { OnChange(State.HostandJoin); });

        exitBtn.onClick.AddListener(() => { exitGroup.SetActive(true); });
        exitYesBtn.onClick.AddListener(() => 
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
        
        });
        exitNoBtn.onClick.AddListener(() => { exitGroup.SetActive(false); });
    }

    private void OnChange(State _state)
    {
        switch (_state) 
        {
            case State.Main:
                {
                    mainGroup.SetActive(true);
                    hostJoinGroup.SetActive(false);
                    break;
                }
            case State.HostandJoin:
                {
                    mainGroup.SetActive(false);
                    hostJoinGroup.SetActive(true);
                    hostUI.SetActive(false);
                    joinGroup.SetActive(false);
                    break;
                }
            case State.Host:
                {
                    hostJoinGroup.SetActive(false);
                    hostUI.SetActive(true);

                    break;
                }
            case State.Join:
                {
                    hostJoinGroup.SetActive(false);
                    joinGroup.SetActive(true);

                    JoinInit();
                    break;
                }
        } 
    }

    #region Join

    private void JoinInit()
    {
        codeInputField.text = "";
    }

    #endregion
}
