using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class SubCameraFollow : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;
    public Vector3 orginOffect;
    float currentAngle = 0f;
    public float cameraSpeed = 10;
    public float rotationSpeed = 5.0f; 
    public float currentPitch = 0f; 
    public float pitchRange = 30f;
    private float elapsedTime = 0f;
    private void Start()
    {
        offset = transform.position - targetTransform.position;
        orginOffect = offset;
        EventManager.instance.cameraTarget += ChageCameraPos;
    }
    private void OnEnable()
    {
        elapsedTime = 0;
    }
    void LateUpdate()
    {
        elapsedTime += Time.deltaTime;
        Quaternion horizontalRotation = Quaternion.Euler(0, currentAngle, 0);
        Quaternion verticalRotation = Quaternion.Euler(currentPitch, 0, 0);
        Quaternion combinedRotation = horizontalRotation * verticalRotation;
        Vector3 rotatedOffset = combinedRotation * offset;
        if (elapsedTime <= 0.5f)
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position + rotatedOffset, cameraSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = targetTransform.position + rotatedOffset;
        }
        transform.LookAt(targetTransform);
    }
    void ChageCameraPos(int y, int z)
    {
        StartCoroutine(ChangeCameraOffset(new Vector3(0, y, z), 1.5f));
    }
    IEnumerator ChangeCameraOffset(Vector3 changeOffset, float changeDuration)
    {
        float timeElapsed = 0;
        Vector3 startOffset = offset;

        while (timeElapsed < changeDuration)
        {
            offset = Vector3.Lerp(startOffset, startOffset -changeOffset, timeElapsed / changeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        offset = startOffset- changeOffset; 
    }
}

