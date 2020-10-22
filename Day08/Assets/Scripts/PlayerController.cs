using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private LayerMask enemyLayers = 0;
    [SerializeField] private float attackDistance = 0f;
    private Animator _animator;
    private GameObject _currentTarget;
    
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
            EnemyProcessing();
        }

        bool shouldActiveRunAnimation = agent.remainingDistance > agent.stoppingDistance;
        _animator.SetBool("run", shouldActiveRunAnimation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("attack");
        }

        transform.position = Vector3.Lerp(transform.position, agent.nextPosition, 0.2f + Time.deltaTime);
    }

    void EnemyProcessing()
    {
        if (Vector3.Distance(transform.position, _currentTarget.transform.position) <= attackDistance)
        {
            Debug.Log("Nu vce Tepepb To4Ho pezda");
            agent.ResetPath();
        }
        else
        {
            agent.SetDestination(_currentTarget.transform.position);
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
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}
