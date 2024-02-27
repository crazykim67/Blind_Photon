using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputKeyManager : MonoBehaviour
{
    public List<KeyBind> keyBindList = new List<KeyBind>();

    public KeyBind FORWARD, BACK, LEFT, RIGHT, SPRINT, PTT, INTER;

    public bool isChange = false;

    public void OnConfirm()
    {
        foreach(var bind in keyBindList)
            bind.OnConfirm();

        isChange = false;
    }

    public void OnCancel()
    {
        foreach (var bind in keyBindList)
            bind.OnCancel();

        isChange = false;
    }

    public bool OnSameKey(KeyBind bind, KeyCode code)
    {
        foreach (var _bind in keyBindList)
        {
            if (_bind == bind)
                continue;

            if (_bind.currentKey == code)
                return false;
        }

        return true;
    }
}
