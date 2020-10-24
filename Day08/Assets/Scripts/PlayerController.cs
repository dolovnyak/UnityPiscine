using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float AttackDelay = 0.5f;
    public float Damage = 20;
    public float MaxHp = 100f;
    [HideInInspector] public bool IsDead = false;

    [SerializeField] private Camera camera = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private LayerMask enemyLayers = 0;
    [SerializeField] private float attackDistance = 0f;
    [SerializeField] private float interactionDistance = 0f;
    private Animator _animator;
    private GameObject _currentTarget;
    private float _nextAttackTime = 0f;
    private Vector3 _currentPath;
    private bool _inBattle = false;
    private bool _isWalk = false;
    private float _currentHp;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        agent.updatePosition = false;
        _currentHp = MaxHp;
    }

    void Update()
    {
        if (IsDead)
        {
            return;
        }

        EventProcessing();

        if (_currentTarget != null)
        {
            _isWalk = false;
            if (Vector3.Distance(transform.position, _currentTarget.transform.position) <= attackDistance)
            {
                agent.ResetPath();
                _inBattle = true;
                InAttackRangeProcessing();
            }
            else
            {
                _currentPath = _currentTarget.transform.position;
                _inBattle = false;
            }
        }
        if (!_inBattle && !_animator.GetCurrentAnimatorStateInfo(0).IsName("AttackMaya"))
        {
            agent.SetDestination(_currentPath);
        }
        bool isAnimateRunning = agent.remainingDistance > agent.stoppingDistance;
        _animator.SetBool("run", isAnimateRunning);

        if (Vector3.Distance(_currentPath, transform.position) <= agent.stoppingDistance + 0.001f)
        {
            _isWalk = false;
        }
        if (!_isWalk && _currentTarget == null)
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + transform.forward * 0.001f, interactionDistance, 
                transform.forward, interactionDistance, enemyLayers);
            if (hits != null && hits.Length != 0)
            {
                var minDistance = Vector3.Distance(hits[0].collider.gameObject.transform.position, transform.position);
                _currentTarget = hits[0].collider.gameObject;
                foreach (var hit in hits)
                {
                    if (Vector3.Distance(hit.collider.gameObject.transform.position, transform.position) < minDistance)
                    {
                        minDistance = Vector3.Distance(hit.collider.gameObject.transform.position, transform.position);
                        _currentTarget = hit.collider.gameObject;
                    }
                }
            }
        }

        transform.position = Vector3.Lerp(transform.position, agent.nextPosition, 0.2f + Time.deltaTime);
    }

    void InAttackRangeProcessing()
    {
        var playerEnemyDir = Vector3.Normalize(_currentTarget.transform.position - transform.position);

        if ((transform.forward - playerEnemyDir).magnitude > 0.1f)
        {
            transform.forward = Vector3.Lerp(transform.forward, playerEnemyDir, 0.03f + Time.deltaTime);
        }
        else if (Time.time >= _nextAttackTime)
        {
            _animator.SetTrigger("attack");
            _nextAttackTime = Time.time + 1f / AttackDelay;
            transform.forward = playerEnemyDir;
            _currentTarget.GetComponent<EnemyController>().TakeDamage(Damage);
            if (_currentTarget.GetComponent<EnemyController>().IsDead)
            {
                _currentTarget = null;
                _inBattle = false;
            }
        }
    }

    void EventProcessing()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (enemyLayers.Includes(hit.collider.gameObject.layer))
                {
                    _currentTarget = hit.collider.gameObject;
                    Debug.Log("pezda kop4enomy");
                }
                else
                {
                    _currentTarget = null;
                    _currentPath = hit.point;
                    _isWalk = true;
                    _inBattle = false;
                }
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
        _animator.SetTrigger("die");
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }
}
