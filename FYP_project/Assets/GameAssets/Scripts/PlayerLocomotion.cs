using System;
using System.Collections;
using System.Collections.Generic;
using ControlFreak2;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotion : MonoBehaviour
{

    public PlayerInput playerInput;
    
    [Header("General Player Settings")]
    public bool isMoving = false;
    public bool isCombat = false;
    public float _horizontal;
    public float _vertical;
    public float speed;
    private float _newSpeed;
    private Vector3 _move;

    [HideInInspector]
    public Vector2 inputMove;
    
    public GameObject playerObject;
    private Animator _animator;
    private CharacterController _controller;
    
    // [Header("Player Rotation Settings")]
    // public float turnSpeed;
    // private float _yRotations;
    
    [Header("Gravity Settings")]
    //Gravity
    public bool isGrounded;
    private Vector3 _velocity;
    public float gravity;
    public float checkRadius;
    public LayerMask groundMask;
    public Transform groundCheck;
    
    //animation stuff
    private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");
    private static readonly int IsCombat = Animator.StringToHash("isCombat");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");


    
    private void Start()
    {
        Application.targetFrameRate = GetTargetFrameRate();
        _animator = playerObject.GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        //CheckCrouch();
    }

    private int GetTargetFrameRate()
    {
        int target = Mathf.CeilToInt((float) Screen.currentResolution.refreshRateRatio.value);
        return target;
    }

    private void Update()
    {
        //CheckCrouch();
        MoveAndGravity();
        //PlayerDirectionalRotation();
        AnimateCharacter();
    }


    #region  Movement and Gravity

    public void CheckCrouch()
    {
        //if (CF2Input.GetButtonDown("Crouch"))
        //{
            isCombat = !isCombat;
            _animator.SetBool(IsCombat, isCombat);
       // }
    }
    
    private void MoveAndGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundMask);

        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        inputMove = playerInput.actions["Move"].ReadValue<Vector2>();
        
        _horizontal = inputMove.x;
        _vertical = inputMove.y;

        _horizontal = Mathf.Clamp(_horizontal, -1, 1);
        _vertical = Mathf.Clamp(_vertical, -1, 1);
        
        isMoving = (_horizontal != 0 || _vertical != 0);

        _move = isCombat ? transform.forward * _vertical + transform.right * _horizontal : 
            transform.forward;
        
        _controller.Move(_move * _newSpeed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

    }

    #endregion

    #region Character Animations
    
    private void AnimateCharacter()
    {
        
        Vector2 direction = new Vector2(_horizontal, _vertical);
        if (direction.sqrMagnitude > 1f)
        {
            direction.Normalize();
        }
        
        _newSpeed = speed * direction.magnitude;
        
        if (isCombat)
        {
            _animator.SetFloat(Horizontal, _horizontal);
            _animator.SetFloat(Vertical, _vertical);
        }
        else
        {
            _animator.SetFloat(MoveSpeed, direction.magnitude);
        }
        
    }
    
    #endregion

    #region Player Rotation (Discarded)

    // private void PlayerDirectionalRotation()
    // {
    //     //gem code
    //     //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
    //     
    //     if (_horizontal != 0) 
    //         _yRotations = Mathf.Atan2(_horizontal, _vertical) * Mathf.Rad2Deg;
    //     //lerp function
    //     transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, _yRotations, 0), turnSpeed * Time.deltaTime);
    // }

    #endregion

}
