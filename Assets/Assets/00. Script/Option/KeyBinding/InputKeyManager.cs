using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputKeyManager : MonoBehaviour
{
    #region Instance

    private static InputKeyManager instance;

    public static InputKeyManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new InputKeyManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    public List<KeyBind> keyBindList = new List<KeyBind>();

    public KeyBind FORWARD, BACK, LEFT, RIGHT, SPRINT, PTT, INTER;

    private void Awake()
    {
        instance = this;
    }

    public void OnConfirm()
    {
        foreach(var bind in keyBindList)
            bind.OnConfirm();
    }

    public void OnCancel()
    {
        foreach (var bind in keyBindList)
            bind.OnCancel();
    }

    public void OnSameKey(KeyBind bind, KeyCode code)
    {
        foreach (var _bind in keyBindList)
        {
            if (_bind == bind)
                continue;

            if(_bind.currentKey == code)
                _bind.ChangeKey();
        }

    }
}
