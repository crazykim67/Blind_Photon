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
            if(instance == null)
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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

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
    }

    public void OnShow()
    {
        InitUI();
    }

    public void OnHide()
    {
        optionGO.SetActive(false);
    }

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
    }

    public void OnValueChanged(Int32 val)
    {
        resolutionNum = val;
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }

    public void OnConfirm()
    {
        Screen.SetResolution(resolutions[resolutionNum].width, resolutions[resolutionNum].height, screenMode);

        if (SoundManager.Instance == null)
            return;

        SoundManager.Instance.VolumeConfirm();

        OnHide();
    }

    public void OnCancel()
    {
        if (SoundManager.Instance == null)
            return;

        SoundManager.Instance.VolumeCancel();
        OnHide();
    }
}
