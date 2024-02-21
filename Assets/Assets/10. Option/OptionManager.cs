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

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void OnShow()
    {
        InitUI();
        //OnValueChanged(resolutionDropdown.value);
    }

    public void OnHide()
    {
        optionGO.SetActive(false);
    }

    private void InitUI()
    {
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
        OnHide();
    }
}
