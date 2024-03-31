using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset; 

    bool keyQ;
    float currentAngle = 0f;
    public float rotationSpeed = 5.0f; 
    public float currentPitch = 0f; 
    public float pitchRange = 30f;
    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        keyQ = Input.GetButton("CameraRotation");

        if (keyQ)
        {
            currentAngle += Input.GetAxis("Mouse X") * rotationSpeed;
            currentPitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentPitch = Mathf.Clamp(currentPitch, -pitchRange, pitchRange);
        }
        Quaternion horizontalRotation = Quaternion.Euler(0, currentAngle, 0);
        Quaternion verticalRotation = Quaternion.Euler(currentPitch, 0, 0);
        Quaternion combinedRotation = horizontalRotation * verticalRotation;
        Vector3 rotatedOffset = combinedRotation * offset;
        transform.position = playerTransform.position + rotatedOffset;
        transform.LookAt(playerTransform);
    }
}

