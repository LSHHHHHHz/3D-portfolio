using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public interface IEvent
{
    void ExcuteEvent(IActor excute);
}

