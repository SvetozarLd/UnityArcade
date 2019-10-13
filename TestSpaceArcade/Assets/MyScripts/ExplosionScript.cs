using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    ParticleSystem particle;
    ParticleSystem.MainModule main;
    PoolObject po;
    // Start is called before the first frame update
    private void Awake()
    {
        po = gameObject.transform.GetComponent<PoolObject>();
        particle = GetComponent<ParticleSystem>();
        main = particle.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnEnable()
    {
        particle.Play();
    }
    void OnParticleSystemStopped()
    {
        po.ReturnToPool();
    }
}
