using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class PlayerTargettingEnemy : MonoBehaviour
{
    public LayerMask layerMask;
    public int distanceObj;
    public GameObject targetObj;
    private float elapsedTime;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log(targetObj);
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= distanceObj)
                {
                    if (targetObj != null)
                    {
                       // PopupManager.Instance.CloseIndicateMonsterPopup(targetObj);
                    }
                    targetObj = enemy.gameObject;
                  //  PopupManager.Instance.IndicateMonsterPopup(targetObj);
                    elapsedTime = 0;
                }
            }
        }
        if (targetObj != null)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > 2)
            {
              //  PopupManager.Instance.CloseIndicateMonsterPopup(targetObj);
                targetObj = null;
                elapsedTime = 0;
            }
        }
    }
}
