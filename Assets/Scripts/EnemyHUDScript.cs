using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static ElementalDamage;

public class EnemyHUDScript : MonoBehaviour
{
    [SerializeField] GameObject fireImg;
    [SerializeField] GameObject iceImg;
    [SerializeField] GameObject voltImg;
    [SerializeField] GameObject waterImg;
    [SerializeField] GameObject windImg;
    [SerializeField] GameObject burnImg;

    public Image healthBar;

    Target target;
    List<ElementalType> lastList = new List<ElementalType>();

    public void Init(Target targ)
    {
        target = targ;
    }

    private void Update()
    {
        if (Enumerable.SequenceEqual(lastList ,target.ActiveElements))
        {
            lastList = target.ActiveElements;
            UpdateStatusIcons();
        }
    }

    void UpdateStatusIcons()
    {
        DisableAllStatusIcons();
        foreach (ElementalType type in lastList)
        {
            switch (type)
            {
                case ElementalType.Fire:
                    fireImg.SetActive(true);
                    break;
                case ElementalType.Ice:
                    iceImg.SetActive(true);
                    break;
                case ElementalType.Volt:
                    voltImg.SetActive(true);
                    break;
                case ElementalType.Wind:
                    waterImg.SetActive(true);
                    break;
                case ElementalType.Water:
                    waterImg.SetActive(true);
                    break;
                case ElementalType.Burn:
                    burnImg.SetActive(true);
                    break;
                case ElementalType.Frozen:
                    break;
                case ElementalType.Electrolyze:
                    break;
                case ElementalType.Shocked:
                    break;
                case ElementalType.Plasm:
                    break;
                default:
                    break;
            }
        }
    }

    void DisableAllStatusIcons()
    {
        fireImg.SetActive(false);
        iceImg.SetActive(false);
        voltImg.SetActive(false);
        waterImg.SetActive(false);
        windImg.SetActive(false);
        burnImg.SetActive(false);
    }

    public void UpdageHealth()
    {
        healthBar.fillAmount = target.maxHealth / 10000f * target.health;
    }
}
