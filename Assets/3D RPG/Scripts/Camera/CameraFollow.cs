using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Transform scanObjectTransform;
    public Vector3 offset;
    public Vector3 offsetScanObject;

    private float moveDuration = 1f;
    private float moveTime = 0f;
    private void Start()
    {
        offset = transform.position - playerTransform.position;
    }
    void LateUpdate()
    {
        if (scanObjectTransform == null)
        {
            Quaternion rotation = Quaternion.Euler(0, playerTransform.eulerAngles.y, 0);
            Vector3 rotatedOffset = rotation * offset;

            transform.position = playerTransform.position + rotatedOffset;
            moveTime = 0;
        }
        else
        {
            if (moveTime > moveDuration)
            {
                moveTime = 1;
            }
            moveTime += Time.deltaTime;
            float fraction = moveTime / moveDuration;

            Vector3 target = scanObjectTransform.position + scanObjectTransform.forward * 1 + scanObjectTransform.up * 1.5f;
            transform.position = Vector3.Lerp(transform.position, target, fraction);
            transform.LookAt(scanObjectTransform.position);
            Vector3 currentEulerAngles = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(0, currentEulerAngles.y, currentEulerAngles.z);
        }
    }
}
