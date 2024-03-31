using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWarePoint : MonoBehaviour
{
    public Transform[] warePointPos;
    private int currentIndex = 0; 
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    private Vector3 targetPosition; 

    private void Start()
    {
        targetPosition = warePointPos[currentIndex].position;
        targetPosition.y = transform.position.y; 
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        if (transform.position == targetPosition)
        {
            currentIndex = (currentIndex + 1) % warePointPos.Length; 
            targetPosition = warePointPos[currentIndex].position;
            targetPosition.y = transform.position.y; 
        }
    }
}
