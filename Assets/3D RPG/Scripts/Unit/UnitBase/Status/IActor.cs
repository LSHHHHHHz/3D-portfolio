using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public interface IActor
{
    void Damaged(int damage);
    Vector3 GetPos();
    void OnReceiveEvent(IEvent source);
    MonsterType GetType();
}


public class Monster : IActor
{
    public void Damaged(int damage)
    {
        throw new NotImplementedException();
    }

    public Vector3 GetPos()
    {
        throw new NotImplementedException();
    }

    public void OnReceiveEvent(IEvent source)
    {
        if (source is SendDamageEvent damangeEvent)
        {
            OnDamaged(damangeEvent.damage);
        }
    }

    private void OnDamaged(int damange)
    {
        // 여기서 데미지 입었을 때 로직
    }

    MonsterType IActor.GetType()
    {
        throw new NotImplementedException();
    }
}