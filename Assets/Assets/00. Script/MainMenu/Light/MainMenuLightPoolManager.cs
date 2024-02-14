using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainMenuLightPoolManager : MonoBehaviour
{
    #region Instance

    private static MainMenuLightPoolManager instance;

    public static MainMenuLightPoolManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new MainMenuLightPoolManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    [SerializeField]
    private GameObject lightObj;
    [SerializeField]
    private int initCount = 5;

    private Queue<ClickLight> lightQueue = new Queue<ClickLight>();

    private Vector3 clickPos;

    private void Awake()
    {
        instance = this;

        Initiallize(initCount);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                GetLight(hit.point);

        }
    }

    private void Initiallize(int _initCount)
    {
        for (int i = 0; i < initCount; i++)
            lightQueue.Enqueue(CreateNewObject());
    }

    private ClickLight CreateNewObject()
    {
        ClickLight light = null;

        light = Instantiate(lightObj).GetComponent<ClickLight>();

        light.DisableLight();
        light.transform.SetParent(this.transform);

        return light;
    }

    public ClickLight GetLight(Vector3 pos)
    {
        if(lightQueue.Count > 0)
        {
            var light = lightQueue.Dequeue();
            light.transform.SetParent(null);
            light.transform.position = pos;

            light.gameObject.SetActive(true);

            light.OnLight();

            return light;
        }
        else
            return null;
    }

    public void ReturnLight(ClickLight _light)
    {
        _light.transform.SetParent(this.transform);
        lightQueue.Enqueue(_light);
    }
}
