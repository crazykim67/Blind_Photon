using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeyBind : MonoBehaviour
{
    public TextMeshProUGUI buttonLabel;

    public KeyCode currentKey;
    public KeyCode initKey;


    private void Awake()
    {
        if (PlayerPrefs.HasKey(this.gameObject.name))
        {
            currentKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(this.gameObject.name));
            buttonLabel.text = currentKey.ToString();
        }

        initKey = currentKey;
    }

    private void Update()
    {
        if (buttonLabel.text == "[Awaiting Input]")
            foreach(KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(code))
                {
                    if (!InputKeyManager.Instance.OnSameKey(this, code))
                        return;

                    buttonLabel.text = code.ToString();

                    currentKey = code;
                    InputKeyManager.Instance.IsChange = false;
                }
            }
    }

    public void ChangeKey()
    {
        if (InputKeyManager.Instance.IsChange)
            return;

        InputKeyManager.Instance.IsChange = true;

        buttonLabel.text = "[Awaiting Input]";
    }

    public void OnConfirm()
    {
        initKey = currentKey;

        buttonLabel.text = initKey.ToString();

        PlayerPrefs.SetString(this.gameObject.name, initKey.ToString());
        PlayerPrefs.Save();
    }

    public void OnCancel()
    {
        currentKey = initKey;

        buttonLabel.text = initKey.ToString();
    }
}
