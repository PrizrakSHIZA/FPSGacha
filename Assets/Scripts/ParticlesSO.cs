using UnityEngine;

[CreateAssetMenu(fileName = "Particles", menuName = "ScriptableObjects/ParticlesSO")]
public class ParticlesSO : ScriptableObject
{
    public ParticleSystem Fire;
    public ParticleSystem Ice;
    public ParticleSystem Volt;
    public ParticleSystem Wind;
    public ParticleSystem Water;

    // Combo effects
    public ParticleSystem Burn;
    public ParticleSystem Frozen;
    public ParticleSystem Electrolyze;
    public ParticleSystem Shocked;
}
