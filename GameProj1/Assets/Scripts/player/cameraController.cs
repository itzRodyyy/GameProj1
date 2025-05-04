using UnityEngine;

public class cameraController : MonoBehaviour
{
    [Header("*** Camera Settings ***")]
    [SerializeField] int cameraSensitivity;
    [SerializeField] int cameraVertMin;
    [SerializeField] int cameraVertMax;
    [SerializeField] bool invertY;

    float rotX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Input
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        if (invertY)
        {
            rotX += mouseY;
        }
        else
        {
            rotX -= mouseY;
        }
        // Clamp Camera X
        rotX = Mathf.Clamp(rotX, cameraVertMin, cameraVertMax);
        // rot Camera X for up down
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);

        // rot Player for right left
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
