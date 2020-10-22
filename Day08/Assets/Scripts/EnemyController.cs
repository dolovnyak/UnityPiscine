using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [HideInInspector]
    public bool IsDead = false;

    [SerializeField] private float maxHp = 0f;
    private Animator _animator;
    private float _currentHp;
    private bool _isRotting;
    private float _startRotTime;
    public float _rotTime = 5f;
    private float _rotTimeDelay = 5f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHp = maxHp;
    }

    void Update()
    {
        if (_isRotting)
        {
            Rot();
            if (Time.time - _startRotTime > _rotTime)
            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHp -= damage;
        Debug.Log("hp: " + _currentHp);
        if (_currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        Debug.Log("cMepTb");
        _animator.SetTrigger("die");
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        Invoke("StartRotting", _rotTimeDelay);
    }

    private void StartRotting()
    {
        _isRotting = true;
        _startRotTime = Time.time;
    }

    private void Rot()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, Time.deltaTime);
    }
}
