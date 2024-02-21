using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using static UnityEditor.Progress;
using System;

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
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private TMP_Dropdown rateDropdown;

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
        Init();
        OnValueChanged(resolutionDropdown.value);
    }

    public void OnHide()
    {
        optionGO.SetActive(false);
    }

    private void Init()
    {
        resolutions.Clear();
        resolutionDropdown.ClearOptions();

        optionGO.SetActive(true);

        resolutions.AddRange(Screen.resolutions);

        int i = 0;

        foreach (Resolution item in resolutions)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();

            option.text = $"{item.width} x {item.height}";

            if (i != 0 && resolutionDropdown.options[i-1] != null)
                if (resolutionDropdown.options[i-1].text.Equals(option.text))
                    continue;

            resolutionDropdown.options.Add(option);
            i++;
        }

        resolutionDropdown.RefreshShownValue();
    }

    public void OnValueChanged(Int32 val)
    {
        rateDropdown.ClearOptions();

        TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
        string currentResoultion = resolutionDropdown.options[resolutionDropdown.value].text;

        foreach (Resolution item in resolutions)
        {
            string str = $"{item.width} x {item.height}";

            if (currentResoultion.Equals(str))
            {
                option.text = $"{Mathf.Round((float)item.refreshRateRatio.value)} hz";
                rateDropdown.options.Add(option);
            }
        }

        rateDropdown.RefreshShownValue();
    }

}
