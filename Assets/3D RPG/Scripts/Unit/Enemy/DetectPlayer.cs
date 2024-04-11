using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectPlayer : DetectBase
{
    public bool detectedPlayer = false;
    private new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Player"))
        {
            detectedPlayer = true;
        }
    }

    private new void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.CompareTag("Player"))
        {
            detectedPlayer = false;
        }
    }
}
