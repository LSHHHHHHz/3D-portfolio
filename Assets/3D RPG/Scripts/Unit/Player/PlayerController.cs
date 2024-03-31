using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform characterPoint;
    [SerializeField] Transform camera_Point;
    [SerializeField] Animator anim;
    [SerializeField] float speed;
    public CharacterController controller;
    public Vector2 moveInput;
    public float playerSpeed;
    public float rotationSpeed;
    public float LookRotation;
    public float currentSpeed;
    public float acceleration;
    public float deceleration;
    public Vector3 playerMoveDir;

    public Vector3 velocity;
    public Vector3 lastMoveDirection;
    public Vector3 playerDir;
    public float turnSpeed;
    public GameObject rotationObj;
    [SerializeField] Vector3 TartGetPos;

    //중력관련
    [SerializeField]
    float jumpForce = 3.0f;
    [SerializeField]
    float gravity = -9.81f;
    [SerializeField]
    float groundDistance = 0f;
    public Transform groundCheck;
    public LayerMask groundMask;
    public bool isGrounded;
    bool keyQ;
    bool isActivMouseCusor;
    bool isWalking;
    float elapsedTime;
    private float moveSpeedThreshold = 0.1f;
    private float lastCursorToggleTime = 0f;
    private float cursorToggleCooldown = 1f;
    private void Update()
    {
        if (Input.GetButton("ActivCusor") && Time.time >= lastCursorToggleTime + cursorToggleCooldown)
        {
            ActivCusor();
            lastCursorToggleTime = Time.time;
        }
        CheckGroundStatus();
        ProcessJumpInput();
        ApplyGravity();
        KeyBoardDir();
        WalkPlayer();
    }
    private void FixedUpdate()
    {
        KeyBoardMove();
    }
    void WalkPlayer()
    {
        float moveSpeed = anim.GetFloat("MoveSpeed");

        if (moveSpeed > moveSpeedThreshold)
        {
            if (!isWalking)
            {
                isWalking = true;
                elapsedTime = 0f;
            }
        }
        else
        {
            isWalking = false;
            elapsedTime = 0f;
        }

        if (isWalking)
        {
            elapsedTime += Time.deltaTime;
            if (moveSpeed < 3f)
            {
                if (elapsedTime >= 0.5f)
                {
                    elapsedTime = 0f;
                }
            }
            else if (moveSpeed < 10)
                if (elapsedTime >= 0.4f)
                {
                    elapsedTime = 0f;
                }
        }
    }
    void CheckGroundStatus()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -5f;
        }
    }
    void ProcessJumpInput()
    {
        // 점프 입력 처리
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
    void ApplyGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        if (velocity.y < 1.7f && isGrounded)
        {
            velocity.y = 0;
        }
    }
    void KeyBoardMove()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        Vector3 lookForward = new Vector3(camera_Point.forward.x, 0f, camera_Point.forward.z).normalized;
        Vector3 lookRight = new Vector3(camera_Point.right.x, 0f, camera_Point.right.z).normalized;
        playerMoveDir = lookForward * moveInput.y + lookRight * moveInput.x;
        if (isMove)
        {
            lastMoveDirection = playerMoveDir;
            currentSpeed = Mathf.Lerp(currentSpeed, playerSpeed, Time.deltaTime * acceleration);
        }
        else if (!isMove && lastMoveDirection != Vector3.zero)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * deceleration);
            playerMoveDir = lastMoveDirection;
        }

        controller.Move((currentSpeed * Time.deltaTime) * playerMoveDir + new Vector3(0, velocity.y, 0) * Time.deltaTime);
        if (currentSpeed < 0.1f)
        {
            lastMoveDirection = Vector3.zero;
        }

    }
    void KeyBoardDir()
    {
        playerDir = moveInput.normalized;
        if (playerDir != Vector3.zero)
        {
            Vector3 forward = camera_Point.forward;
            Vector3 right = camera_Point.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 desiredMoveDirection = (forward * moveInput.y + right * moveInput.x).normalized;

            // 플레이어 회전
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            rotationObj.transform.rotation = Quaternion.Slerp(rotationObj.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
    public void ActivCusor()
    {
        if (isActivMouseCusor)
        {
            HideCursor();
        }
        else
        {
            ShowCursor();
        }
    }
    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isActivMouseCusor = false;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isActivMouseCusor = true;
    }

}
