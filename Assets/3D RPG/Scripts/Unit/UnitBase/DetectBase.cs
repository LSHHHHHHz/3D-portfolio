using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class DetectBase : MonoBehaviour
{
    public bool detectedTarget;
    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection") || (other.CompareTag("ForEnemyDetection")))
        {
            detectedTarget = true;
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ForPlayerDetection") || (other.CompareTag("ForEnemyDetection")))
        {
            detectedTarget = false;
        }
    }
}
