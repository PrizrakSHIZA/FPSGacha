using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalDamage
{
    public static void ApplyReaction(ElementalType appliedElement, ElementalType elementOnTarget, ref Dictionary<ElementalType, float> StatusList, out float multiplier, out Color color)
    {
        color = Color.white;
        multiplier = 1f;

        string key = appliedElement.ToString() + elementOnTarget.ToString();
        switch (key)
        {
            case "FireFire": // solo Fire
                multiplier = 1.3f;
                color = new Color(255, 185, 0, 255);
                if (StatusList[appliedElement] > 100)
                {
                    StatusList[ElementalType.Burn] = 100;
                    StatusList[appliedElement] = 0;
                    multiplier = 1.5f;
                    color = Color.red;
                }
                break;
            case "IceIce": // solo Ice
                multiplier = 1.3f;
                color = Color.cyan;
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
    }

    public enum ElementalType
    { 
        Fire, //smold
        Ice, //frostbite
        Volt,
        Wind,
        Water,

        //combo statuses
        Burn,
        Frozen,
        Electrolyze,
        Shocked,
        Plasm,
    }
}
