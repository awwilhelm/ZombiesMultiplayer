using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {

    public float lookSensitivity = 5.0f;
    private float lookSmoothDamp = 0.08f;

    private float xRotation;
    private float yRotation;
    private float currXRotation;
    private float currYRotation;
    private float xRotationV;
    private float yRotationV;

    void Start()
    {
        //Cursor.visible = false;
    }
    void Update()
    {
        xRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        yRotation += Input.GetAxis("Mouse X") * lookSensitivity;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        currXRotation = Mathf.SmoothDamp(currXRotation, xRotation, ref xRotationV, lookSmoothDamp);
        currYRotation = Mathf.SmoothDamp(currYRotation, yRotation, ref yRotationV, lookSmoothDamp);

        transform.rotation = Quaternion.Euler(currXRotation, currYRotation, 0);
        transform.parent.parent.rotation = Quaternion.Euler(0, currYRotation, 0);
    }
}
