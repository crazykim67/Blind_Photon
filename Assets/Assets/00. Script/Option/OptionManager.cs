using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;

public class OptionManager : MonoBehaviour
{
    #region Instance

    private static OptionManager instance;

    public static OptionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new OptionManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    public GameObject optionGO;

    [Header("Rsolution")]
    private FullScreenMode screenMode;
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private Toggle screenToggle;
    private int resolutionNum;

    private List<Resolution> resolutions = new List<Resolution>();

    [Header("Buttons")]
    [SerializeField]
    private Button confirmBtn;
    [SerializeField]
    private Button cancelBtn;

    [Header("Output")]
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider bgSlider;
    [SerializeField]
    private Slider sfxSlider;

    private float masterVal = 1;
    private float bgVal = 1;
    private float sfxVal = 1;

    [Header("Mouse")]
    [SerializeField]
    private Slider sensSlider;
    [SerializeField]
    private TextMeshProUGUI sensitivity;
    [SerializeField]
    private float mouseSen;
    [SerializeField]
    private float fixedSen;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        #region Sound

        if (PlayerPrefs.HasKey("Master"))
            masterVal = PlayerPrefs.GetFloat("Master");
        else
            PlayerPrefs.SetFloat("Master", masterVal);

        if (PlayerPrefs.HasKey("BG"))
            bgVal = PlayerPrefs.GetFloat("BG");
        else
            PlayerPrefs.SetFloat("BG", bgVal);

        if (PlayerPrefs.HasKey("SFX"))
            sfxVal = PlayerPrefs.GetFloat("SFX");
        else
            PlayerPrefs.SetFloat("SFX", sfxVal);

        if (SoundManager.Instance == null)
            return;

        SoundManager.Instance.SetVolume(PlayerPrefs.GetFloat("Master"), PlayerPrefs.GetFloat("BG"), PlayerPrefs.GetFloat("SFX"));
        
        #endregion
    }

    private void Start()
    {
        InitUI();
        OnHide();
    }

    #region Show & Hide

    public void OnShow()
    {
        InitUI();
    }

    public void OnHide()
    {
        optionGO.SetActive(false);
    }

    #endregion

    private void InitUI()
    {
        #region Resolution

        int optionNum = 0;

        resolutions.Clear();
        resolutionDropdown.options.Clear();

        for(int i = 0; i < Screen.resolutions.Length; i++)
            if(Screen.currentResolution.refreshRateRatio.value == Screen.resolutions[i].refreshRateRatio.value)
                resolutions.Add(Screen.resolutions[i]);

        foreach(var item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = $"{item.width} x {item.height} {Mathf.Round((float)item.refreshRateRatio.value)} Hz";
            resolutionDropdown.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdown.value = optionNum;

            optionNum++;
        }

        screenToggle.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
        FullScreenBtn(screenToggle.isOn);
        optionGO.SetActive(true);

        resolutionDropdown.RefreshShownValue();

        #endregion

        #region Output

        if (SoundManager.Instance == null)
            return;

        masterSlider.value = PlayerPrefs.GetFloat("Master");
        bgSlider.value = PlayerPrefs.GetFloat("BG");
        sfxSlider.value = PlayerPrefs.GetFloat("SFX");

        #endregion

        #region Key Setting

        InputKeyManager.Instance.OnConfirm();

        #endregion

        #region Mouse

        if (PlayerPrefs.HasKey("MouseSensitivity"))
            mouseSen = PlayerPrefs.GetFloat("MouseSensitivity");
        else
            mouseSen = fixedSen = 0.5f;

        sensSlider.value = mouseSen;

        #endregion
    }

    public void OnValueChanged(Int32 val)
    {
        resolutionNum = val;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    #region Mouse

    public void OnSensitivityChanged(float val)
    {
        sensitivity.text = Mathf.RoundToInt(val * 100).ToString();
        fixedSen = val;
    }

    #endregion

    public void OnConfirm()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);

        if (SoundManager.Instance == null)
            return;

        SoundManager.Instance.VolumeConfirm();

        InputKeyManager.Instance.OnConfirm();

        PlayerPrefs.SetFloat("MouseSensitivity", fixedSen);
        mouseSen = fixedSen;

        OnHide();
    }

    public void OnCancel()
    {
        if (SoundManager.Instance == null)
            return;

        SoundManager.Instance.VolumeCancel();

        InputKeyManager.Instance.OnCancel();

        if (PlayerPrefs.HasKey("MouseSensitivity"))
            mouseSen = fixedSen = PlayerPrefs.GetFloat("MouseSensitivity");
        else
            mouseSen = fixedSen = 0.5f;

        sensSlider.value = mouseSen;
        OnHide();
    }
}
