using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    #region Instance

    private static OptionManager instance;

    public static OptionManager Instance
    {
        get
        {
            if(Instance == null)
            {
                instance = new OptionManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

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
        
    }

    public void OnHide()
    {

    }
}
