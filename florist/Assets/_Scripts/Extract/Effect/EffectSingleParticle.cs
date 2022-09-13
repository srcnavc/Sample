using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSingleParticle : MonoBehaviour,IEffect
{
    [SerializeField] Vector3 StartPositionOffset;
    [SerializeField] GameObject ParticlePrefab;
    [SerializeField] bool RecreateParticle;
    [SerializeField] bool ReleaseParticle;
    ParticleSystem Particle;

    [SerializeField] string EffectTag = "default";
    public string _EffectTag => EffectTag;
    private void Start()
    {

        if(RecreateParticle)
        ParticlePrefab = Instantiate(ParticlePrefab);
        Particle = ParticlePrefab.GetComponent<ParticleSystem>();
    }
    public void doEffect(GameObject target)
    {
        ParticlePrefab.transform.position = transform.position + StartPositionOffset;
        if (ReleaseParticle)
            ParticlePrefab.transform.SetParent(null);
        ParticlePrefab.SetActive(true);

        Particle?.Play();
    }
}
