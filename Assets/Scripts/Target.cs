using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ElementalDamage;

public class Target : MonoBehaviour
{
    public float health = 50f;

    [SerializeField] GameObject floatingTextPrefab;

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

    public void TakeDamage(float damage, float elementAmount, ElementalType type)
    {
        ApplyElement(type, elementAmount);
        
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

        ElementalDamage.ApplyReaction(type, curStatus.Key, ref StatusList, out multiplier, out textColor);

        float totalDamage = damage * multiplier;
        ApplyDamage(totalDamage);
        ShowBattleText(totalDamage, textColor);
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
        Debug.Log(StatusList[type]);
    }

    void ShowBattleText(float damage, Color color)
    {
        GameObject temp = Instantiate(floatingTextPrefab, transform.root);
        temp.GetComponent<FloatingText>().Setup(damage.ToString(), color);
    }

    float ApplyReaction(ElementalType appliedElement, ElementalType elementOnTarget)
    {
        float multiplier = 1;
        string key = appliedElement.ToString() + elementOnTarget.ToString();
        switch (key)
        {
            case "FireFire": // solo Fire
                multiplier = 1.3f;
                if (StatusList[appliedElement] > 100)
                { 
                    StatusList[ElementalType.Burn] = 100;
                    StatusList[appliedElement] = 0;
                    multiplier = 1.5f;
                }
                break;
            case "IceIce": // solo Ice
                multiplier = 1.3f;
                if (StatusList[appliedElement] > 100)
                {
                    StatusList[ElementalType.Frozen] = 100;
                    StatusList[appliedElement] = 0;
                    multiplier = 1.3f;
                }
                break;
            case "VoltVolt":
                multiplier = 1.3f;
                if (StatusList[appliedElement] > 100)
                {
                    StatusList[ElementalType.Shocked] = 100;
                    StatusList[appliedElement] = 0;
                    multiplier = 1.5f;
                }
                break;
            case "FireIce":
            case "IceFire": // temperature difference
                float water = (StatusList[appliedElement] + StatusList[elementOnTarget]) / 2;
                StatusList[appliedElement] = 0;
                StatusList[elementOnTarget] = 0;
                StatusList[ElementalType.Water] = water;
                multiplier = 2f;
                break;
            case "FireVolt":
            case "VoltFire": // plasma
                StatusList[appliedElement] = 0;
                StatusList[elementOnTarget] = 0;
                StatusList[ElementalType.Plasm] = 100;
                multiplier = 1.2f;
                break;
            default:
                break;
        }
        return multiplier;
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
