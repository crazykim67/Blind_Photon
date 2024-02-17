using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{   
    public enum State
    {
        Main,
        Host,
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
    private GameObject hostGroup;

    [SerializeField]
    private Button hostBtn;
    [SerializeField]
    private Button joinBtn;
    [SerializeField]
    private Button backBtn;

    [Header("Exit Group")]
    [SerializeField]
    private GameObject exitGroup;
    [SerializeField]
    private Button exitYesBtn;
    [SerializeField]
    private Button exitNoBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(() => { OnChange(State.Host); });
        backBtn.onClick.AddListener(() => { OnChange(State.Main); });
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
                    hostGroup.SetActive(false);
                    break;
                }
            case State.Host:
                {
                    mainGroup.SetActive(false);
                    hostGroup.SetActive(true);
                    break;
                }
        } 
    }
}
