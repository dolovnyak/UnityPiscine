using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float AttackFrame = 0.5f;
    public float Damage = 20;
    [SerializeField] private Camera camera = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private LayerMask enemyLayers = 0;
    [SerializeField] private float attackDistance = 0f;
    private Animator _animator;
    private GameObject _currentTarget;
    private float _nextAttackTime = 0f;
    private Vector3 _currentPath;
    private bool _inBattle = false;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        agent.updatePosition = false;
    }

    void Update()
    {
        EventProcessing();

        if (_currentTarget != null)
        {
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

        bool shouldActiveRunAnimation = agent.remainingDistance > agent.stoppingDistance;
        _animator.SetBool("run", shouldActiveRunAnimation);
        
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
            _nextAttackTime = Time.time + 1f / AttackFrame;
            transform.forward = Vector3.Normalize(_currentTarget.transform.position - transform.position);
            _currentTarget.GetComponent<EnemyController>().TakeDamage(Damage);
            if (_currentTarget.GetComponent<EnemyController>().IsDead)
            {
                _currentTarget = null;
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
                    _inBattle = false;
                }
            }
        }
    }
}
