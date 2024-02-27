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
                    if (!OptionManager.Instance.keyManager.OnSameKey(this, code))
                        return;

                    buttonLabel.text = code.ToString();

                    currentKey = code;
                    OptionManager.Instance.keyManager.isChange = false;
                }
            }
    }

    public void ChangeKey()
    {
        if (OptionManager.Instance.keyManager.isChange)
            return;

        OptionManager.Instance.keyManager.isChange = true;

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
