using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ClickLight : MonoBehaviour
{
    [SerializeField]
    private Light pointLight;

    [SerializeField]
    private float speed = 0.2f;

    private float intensity = 0f;

    public void OnLight()
    {
        StartCoroutine(ShowLight());
    }

    private IEnumerator ShowLight()
    {
        intensity = 0;

        pointLight.intensity = intensity;

        while(intensity < 1f)
        {
            intensity += speed * Time.deltaTime;
            pointLight.intensity = intensity;

            yield return null;
        }

        yield return new WaitForSeconds(2f);

        yield return HideStep();
    }

    private IEnumerator HideStep()
    {
        // ÃÊ±âÈ­
        intensity = 1f;
        pointLight.intensity = intensity;

        while (intensity > 0f)
        {
            intensity -= speed * Time.deltaTime;
            pointLight.intensity = intensity;

            yield return null;
        }

        DisableLight();

        MainMenuLightPoolManager.Instance.ReturnLight(this);
    }

    public void DisableLight()
    {
        intensity = 0f;
        pointLight.intensity = intensity;

        this.gameObject.SetActive(false);
    }
}
