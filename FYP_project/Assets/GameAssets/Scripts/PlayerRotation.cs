using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using ControlFreak2;

public class PlayerRotation : MonoBehaviour
{

    [Range(0, 10)]
    public float xSensitivity;
    
    [Range(0, 10)]
    public float ySensitivity;
    
    public float turnSpeed;
    [SerializeField]
    private Transform mainCam;
    private PlayerLocomotion _locomotion;
    public Transform cameraFollow;
    private float _xRotation;
    private float _yRotation;


    private void Start()
    {
        _locomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        Vector3 inputDir = new Vector3(_locomotion.inputMove.x, 0, _locomotion.inputMove.y);
        
        float targetRotation = _locomotion.isCombat ? mainCam.eulerAngles.y :
            Quaternion.LookRotation(inputDir).eulerAngles.y + mainCam.eulerAngles.y;
        
        if (_locomotion.inputMove != Vector2.zero)
        {
            Quaternion playerRotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        CameraRotation();
       
    }

    private void CameraRotation()
    {
        _yRotation += CF2Input.GetAxis("Mouse X") * ySensitivity;
        _xRotation -= CF2Input.GetAxis("Mouse Y") * xSensitivity;

        _xRotation = Mathf.Clamp(_xRotation, -60, 70);
        
        Quaternion rotation = Quaternion.Euler(_xRotation, _yRotation, 0);

        if (_locomotion.isCombat)
        {
            cameraFollow.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, _yRotation, 0), turnSpeed * Time.deltaTime);
        }
        else
        {
            cameraFollow.rotation = rotation;
        }
    }
    
}
