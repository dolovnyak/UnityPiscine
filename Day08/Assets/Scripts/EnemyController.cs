using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [HideInInspector] public bool IsDead = false;
    [HideInInspector] public float MaxHp = 0f;
    [HideInInspector] public float Damage = 0f;
    [HideInInspector] public float AttackDelay = 0f;
    [HideInInspector] public float AttackDistance = 0f;
    [HideInInspector] public float InteractionDistance = 0f;
    [HideInInspector] public float FollowPlayerTime = 0f;
    [HideInInspector] public GameObject PlayerObject = null;

    [SerializeField] private NavMeshAgent agent = null;

    private Animator _animator;
    private float _currentHp;
    private float _nextAttackTime = 0f;
    private Vector3 _spawnPosition;

    private bool _isFollowPlayer;
    private float _startFollowPlayerTime;

    private bool _isRotting;
    private float _startRotTime;
    public float _rotTime = 5f;
    private float _rotTimeDelay = 5f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHp = MaxHp;
        _spawnPosition = transform.position;
    }

    void Update()
    {
        if (IsDead)
        {
            if (_isRotting)
            {
                Rot();
            }
        }
        else
        {
            if (!_isFollowPlayer)
            {
                if (Vector3.Distance(PlayerObject.transform.position, transform.position) <= InteractionDistance)
                {
                    _isFollowPlayer = true;
                    _startFollowPlayerTime = Time.time;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, PlayerObject.transform.position) <= AttackDistance)
                {
                    _startFollowPlayerTime = Time.time;
                    agent.ResetPath();
                    InAttackRangeProcessing();
                }
                else
                {
                    if (Time.time - _startFollowPlayerTime > FollowPlayerTime)
                    {
                        agent.SetDestination(_spawnPosition);
                        _isFollowPlayer = false;
                    }
                    else
                    {
                        agent.SetDestination(PlayerObject.transform.position);
                    }
                }
            }

            bool shouldActiveRunAnimation = agent.remainingDistance > agent.stoppingDistance;
            _animator.SetBool("run", shouldActiveRunAnimation);
        // transform.position = Vector3.Lerp(transform.position, agent.nextPosition, 0.2f + Time.deltaTime);
        }
    }

    void InAttackRangeProcessing()
    {
        var enemyPlayerDir = Vector3.Normalize(PlayerObject.transform.position - transform.position);

        if ((transform.forward - enemyPlayerDir).magnitude > 0.1f)
        {
            transform.forward = Vector3.Lerp(transform.forward, enemyPlayerDir, 0.03f + Time.deltaTime);
        }
        else if (Time.time >= _nextAttackTime)
        {
            _animator.SetTrigger("attack");
            _nextAttackTime = Time.time + 1f / AttackDelay;
            transform.forward = enemyPlayerDir;
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
        if (Time.time - _startRotTime > _rotTime)
        {
            Destroy(gameObject);
        }
    }
}
