using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ElementalDamage;

[RequireComponent(typeof(Target))]
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

    private void Start()
    {
        target = GetComponent<Target>();
    }

    private void Update()
    {

    }

    public void UpdageHealth()
    {
        healthBar.fillAmount = target.maxHealth / 10000f * target.health;
    }
}
