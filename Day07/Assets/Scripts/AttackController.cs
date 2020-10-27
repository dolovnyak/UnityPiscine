using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] private ParticleSystem gunParticalSystem;
    [SerializeField] private AudioSource gunShootAudio;
    [SerializeField] private float gunShootDelay;
    [SerializeField] private ParticleSystem missileParticalSystem;
    [SerializeField] private AudioSource missileShootAudio;
    [SerializeField] private float missileShootDelay;
    private float _gunShootStart = 0f;
    private float _missleShootStart = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - _gunShootStart > gunShootDelay)
        {
            gunParticalSystem.Play();
            gunShootAudio.Play();
            _gunShootStart = Time.time;
        }
        else if (Input.GetMouseButtonDown(1) && Time.time - _missleShootStart > missileShootDelay) 
        {
            missileParticalSystem.Play();
            missileShootAudio.Play();
            _missleShootStart = Time.time;
        }
    }
}
