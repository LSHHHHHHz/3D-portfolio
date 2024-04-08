using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerData
{
    void SetData(object newData);
    object GetData();
    void RemoveData(int count);
    void ClearData();

}