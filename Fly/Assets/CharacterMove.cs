using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float Speed;
    public float RotateSpeed;
    public GameObject Camera;
    public GameObject Canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void LateUpdate()
    {
        Canvas.transform.LookAt(2*transform.position - Camera.transform.position, Vector3.up);
    }

    private void CheckInput()
    {
        Vector3 MoveVector = Vector3.zero;

        Quaternion CameraQua = Quaternion.Euler(0, Camera.transform.eulerAngles.y, 0);


        bool HaveInput = false;

        if (InputForward())
        {
            HaveInput = true;
            MoveVector += CameraQua * Vector3.forward;
        }

        if (InputBack())
        {
            HaveInput = true;
            MoveVector += CameraQua * Vector3.back;
        }

        if (InputRight())
        {
            HaveInput = true;
            MoveVector += CameraQua * Vector3.right;
        }

        if (InputLeft())
        {
            HaveInput = true;
            MoveVector += CameraQua * Vector3.left;
        }

        GetComponent<CharacterController>().Move(MoveVector.normalized*Speed*Time.deltaTime);

        if (HaveInput)
        {
            
            transform.rotation = Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, MoveVector, Vector3.up), 0);
        }

    }

    private bool InputForward()
    {
        return Input.GetKey(KeyCode.W);
    }

    private bool InputRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    private bool InputLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    private bool InputBack()
    {
        return Input.GetKey(KeyCode.S);
    }
    
}
