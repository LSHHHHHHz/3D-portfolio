using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CharacterStatusBase : MonoBehaviour, IActor
{
    public GameObject textPrefab;
    List<GameObject> pools;
    public MonsterType monsterType;
    public Sprite characerImage;
    public float max_HP;
    public float current_HP;
    public int baseAttack;
    public int totalAttack;
    public float max_MP;
    public float current_MP;
    public string characterName;

    private void Awake()
    {
        totalAttack = baseAttack;
        pools = new List<GameObject>();
    }
    public void OnReceiveEvent(IEvent source)
    {
        source.ExcuteEvent(this);
    }
    public void Damaged(int damage)
    {
        current_HP -= damage;
        DisplayStatus(transform.position, damage, "Red", false);
    }
    public void RecoveryHP(int recovery)
    {
        current_HP += recovery;
        DisplayStatus(transform.position, recovery, "Red", true);
    }
    public void RecoveryMP(int recovery)
    {
        current_MP += recovery;
        DisplayStatus(transform.position, recovery, "Blue", true);
    }
    public void ConsumMP(int consume)
    {
        current_MP -= consume;
        DisplayStatus(transform.position, consume, "Blue", false);
    }
    public void UpdateTotalAttack(int additionalAttack)
    {
        totalAttack = baseAttack + additionalAttack;
    }
    public void DisplayStatus(Vector3 pos, int textStatus, string color, bool isPositive)
    {
        string sign = isPositive ? "+" : "-";
        GameObject select = null;

        foreach (GameObject item in pools)
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if (select == null)
        {
            select = Instantiate(textPrefab, transform);
            pools.Add(select);
        }
        if (select != null)
        {
            select.transform.position = pos;
            StatusText text = select.GetComponent<StatusText>();
            text.ShowStatusText(sign + textStatus.ToString(), color);
        }
    }

   
}


