using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject canon = null;
    [SerializeField] private float speed = 0;
    [SerializeField] private Rigidbody rbLeft = null;
    [SerializeField] private Rigidbody rbRight = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Transform pizdecForward = null;
    private float gravity = 200f;
    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = new Vector3(0f, 0f, 0f);
        // _rb.AddForce(Vector3.down * 1000);
        // rbLeft.AddForce(Vector3.down * gravity);
        // rbRight.AddForce(Vector3.down * gravity);
        // rbRight.centerOfMass = new Vector3(1f, 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // rbLeft.centerOfMass = new Vector3(-1f, 0f, 0.5f);
        // rbRight.centerOfMass = new Vector3(1f, 0f, 0.5f);
        // Debug.Log("rb: " + _rb.centerOfMass);
        // Debug.Log("rbl: " + rbLeft.centerOfMass);
        // Debug.Log("rbr: " + rbRight.centerOfMass);
        // Debug.Log(_rb.)
        if (Input.GetKey(KeyCode.W))
        {
            // rbLeft.velocity = transform.forward * speed;
            // rbRight.velocity = transform.forward * speed;

            // _rb.AddForce(transform.forward * speed);
            // rbLeft.AddForce(transform.forward * speed);
            // rbRight.AddForce(transform.forward * speed);

            _rb.AddForce(transform.forward * speed);
            // rbLeft.AddForce(transform.forward * speed);
            // rbRight.AddForce(transform.forward * speed);

            // rbLeft.velocity = Vector3.Normalize(transform.forward) * speed;
            // rbRight.velocity = Vector3.Normalize(transform.forward) * speed;
            // _rb.velocity = Vector3.Normalize(transform.forward) * speed;
            // _rb.velocity = transform.forward * speed;
            // RaycastHit hit;
            // if (Physics.Raycast(pizdecForward.position, -pizdecForward.up, out hit))
            // {
            //     agent.SetDestination(hit.point);
            //     Debug.Log("hit: " + hit.point);
            //     Debug.Log("obj: " + hit.collider.gameObject.name);
            // }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // _rb.velocity = Vector3.Normalize(transform.forward * -speed);
            // rbLeft.velocity = transform.forward * -speed;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            // rbLeft.velocity = Vector3.zero;
        }
        // rbLeft.velocity = Vector3.Normalize(rbLeft.velocity + Vector3.down * gravity);
        // rbRight.velocity = Vector3.Normalize(rbRight.velocity + Vector3.down * gravity);

        if (Input.GetKey(KeyCode.A))
        {
            transform.forward = Quaternion.AngleAxis(-1, transform.up) * transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.forward = Quaternion.AngleAxis(1, transform.up) * transform.forward;
        }
        canon.transform.position = new Vector3(canon.transform.position.x, transform.position.y, transform.position.z);
        // _rb.AddForce(Physics.gravity * _rb.mass);
    }
}
