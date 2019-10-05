using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Character;
    public float Dis;
    public float RotationSpeed;

    public float MinRotationY;
    public float MaxRotationY;

    private float MouseX;
    private float MouseY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraControl();
    }

    private void CameraControl()
    {
        MouseX += Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
        MouseY -= Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;
        MouseY = Mathf.Clamp(MouseY, MinRotationY, MaxRotationY);

        transform.rotation = Quaternion.Euler(MouseY, MouseX, 0);

        transform.position = Character.transform.position + transform.rotation * Vector3.back * Dis;
    }

}
