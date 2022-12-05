using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ElementalDamage;

public class Target : MonoBehaviour
{
    [SerializeField] bool showHUD = true;
    [SerializeField] Transform HUDPosition;

    public float maxHealth = 50f;
    public float health = 50f;
    public List<ElementalType> ActiveElements = new List<ElementalType>();

    [Header("Prefabs")]
    [SerializeField] GameObject floatingTextPrefab;
    [SerializeField] GameObject HudPrefab;

    EnemyHUDScript Hud;

    public Dictionary<ElementalType, float> StatusList = new Dictionary<ElementalType, float>()
    {
        { ElementalType.Fire, 0f},
        { ElementalType.Ice, 0f},
        { ElementalType.Volt, 0f},
        { ElementalType.Wind, 0f},
        { ElementalType.Water, 0f},
        { ElementalType.Burn, 0f},
        { ElementalType.Frozen, 0f},
        { ElementalType.Electrolyze, 0f},
        { ElementalType.Shocked, 0f},
    };

    private void Start()
    {
        if (showHUD)
        { 
            Hud = Instantiate(HudPrefab, HUDPosition).GetComponent<EnemyHUDScript>();
            Hud.Init(this);
        }
    }

    public void TakeDamage(float damage, float elementAmount, ElementalType type)
    {        
        float multiplier = 1f;
        Color textColor = Color.white;

        KeyValuePair<ElementalType, float> curStatus = new KeyValuePair<ElementalType, float>();

        //check elemental reactions
        foreach (KeyValuePair<ElementalType, float> status in StatusList)
        {
            if (status.Value > 0)
            {
                curStatus = status;
                break;
            }
        }

        ApplyElement(type, elementAmount);

        ElementalDamage.ApplyReaction(type, curStatus.Key, ref StatusList, out multiplier, out textColor);

        UpdateActiveElements();

        float totalDamage = damage * multiplier;
        ApplyDamage(totalDamage);
        ShowBattleText(totalDamage, textColor);
        ChangeHealthBar();
    }

    void ChangeHealthBar()
    {
        Hud.UpdageHealth();
    }

    void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Death();
    }

    void ApplyElement(ElementalType type, float amount)
    {
        StatusList[type] += amount;
    }

    void UpdateActiveElements()
    {
        ActiveElements.Clear();
        foreach (KeyValuePair<ElementalType, float> status in StatusList)
        {
            if (status.Value > 0)
                ActiveElements.Add(status.Key);
        }
    }

    void ShowBattleText(float damage, Color color)
    {
        GameObject temp = Instantiate(floatingTextPrefab, transform.root);
        temp.GetComponent<FloatingText>().Setup(damage.ToString(), color);
    }


    void Death()
    {
        Destroy(gameObject);
    }
}
