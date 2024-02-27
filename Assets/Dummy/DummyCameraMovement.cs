using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyCameraMovement : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform body;
    [SerializeField]
    private float xRotation = 0f;

    [SerializeField]
    private float minClamp = -90f;
    [SerializeField]
    private float maxClamp = 90f;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MouseSensitivity"))
            sensitivity = Mathf.RoundToInt(PlayerPrefs.GetFloat("MouseSensitivity") * 100);
        else
            sensitivity = 50f;
    }

    private void LateUpdate()
    {
        LookAt();
    }

    private void LookAt()
    {
        if (body == null)
            return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minClamp, maxClamp);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
