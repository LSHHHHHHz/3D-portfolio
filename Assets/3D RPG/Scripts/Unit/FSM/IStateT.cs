using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public interface IState<T>
{
    void Enter(T sender);
    void Update(T sender);
    void Exit(T sender);
}
