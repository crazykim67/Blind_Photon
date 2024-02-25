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

    private bool isChange = false;

    public bool IsChange
    {
        get { return isChange; }
        set { isChange = value; }
    }

    private void Awake()
    {
        instance = this;
    }

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
