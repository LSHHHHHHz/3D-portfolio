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
    private StatusPopup targetObjPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectTarget();
        }

        if (targetObj != null)
        {
            UpdateTargetUI();
            CheckTargetTimeout();
        }
    }
    void SelectTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layerMask))
        {
            CharacterStatusBase enemy = hit.collider.GetComponent<CharacterStatusBase>();
            if (enemy != null && Vector3.Distance(transform.position, enemy.transform.position) <= distanceObj)
            {
                targetObj = enemy.gameObject;
                UpdatePopup(enemy);
                elapsedTime = 0;
            }
        }
    }
    void UpdatePopup(CharacterStatusBase enemy)
    {
        if (targetObjPrefab == null)
        {
            targetObjPrefab = Instantiate(PopupFactory.instance.monsterStatusPopupPrefab, PopupFactory.instance.monsterStatusPopupTransform).GetComponent<StatusPopup>();
        }
        targetObjPrefab.OpenPopupUI();
        targetObjPrefab.SetData(enemy.characterName, enemy.max_HP, enemy.current_HP, enemy.max_MP, enemy.current_MP);
    }

    void UpdateTargetUI()
    {
        CharacterStatusBase enemy = targetObj.GetComponent<CharacterStatusBase>();
        if (enemy != null)
        {
            targetObjPrefab.SetData(enemy.characterName, enemy.max_HP, enemy.current_HP, enemy.max_MP, enemy.current_MP);
        }
    }
    void CheckTargetTimeout()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 5)
        {
            targetObjPrefab.ClosePopupUI();
            targetObj = null;
            elapsedTime = 0;
        }
    }
}
