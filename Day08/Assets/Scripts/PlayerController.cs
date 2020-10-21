using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera camera = null;
    [SerializeField] private NavMeshAgent agent = null;
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        agent.updatePosition = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        bool shouldActiveRunAnimation = agent.remainingDistance > agent.radius;
        _animator.SetBool("run", shouldActiveRunAnimation);

        transform.position = Vector3.Lerp(transform.position, agent.nextPosition, 0.2f + Time.deltaTime);
    }
}
