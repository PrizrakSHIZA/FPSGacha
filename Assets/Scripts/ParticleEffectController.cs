using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Target))]
public class ParticleEffectController : MonoBehaviour
{
    ParticlesSO ParticlesSO;
    Target target;
    List<ElementalDamage.ElementalType> lastList = new List<ElementalDamage.ElementalType>();

    private void Awake()
    {
        ParticlesSO = Resources.Load<ParticlesSO>("Effectparticles");
        target = GetComponent<Target>();
    }

    private void Update()
    {

    }
}
