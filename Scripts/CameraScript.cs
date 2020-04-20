using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float _AxisX;
    float _AxisY;

    public float _distance = 50;
    public float _speed = 5f;
    void Start()
    {
        SetCameraPosition();
    }


    void Update()
    {
        if (Input.GetMouseButton(1)) 
            SetCameraPosition(); //odwołanie do private void
    }



    private void SetCameraPosition() //ruch kamery
    {
            float deltaX = Input.GetAxis("Mouse X");
            float deltaY = Input.GetAxis("Mouse Y");

            _AxisX += deltaY * _speed;
            _AxisY += deltaX * _speed;

            _AxisX = Mathf.Clamp(_AxisX, -85f, 0f); //maksymalny kąt ruchu kamery

            var rotation = Quaternion.Euler(_AxisX, _AxisY, 0);
            transform.position = rotation * Vector3.forward * _distance;

            transform.LookAt(Vector3.up * 4f);
    }
    
}
