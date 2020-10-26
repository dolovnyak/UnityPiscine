using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject canon = null;
    [SerializeField] private float speed = 0;
    [SerializeField] private LayerMask solidSurfaceLayers = 0;
    private bool _isOnSolidSurface;
    private float gravity = 200f;
    private Rigidbody _rb;
    private float _deltaYCanon;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
        _deltaYCanon = Mathf.Abs(transform.position.y - canon.transform.position.y);
    }

    void Update()
    {
        // Debug.Log("rb: " + _rb.centerOfMass);
        // Debug.Log(transform.rotation);

        // if (_isOnSolidSurface)
        // {
        //     Movement();
        // }
        if (IsOnSolidSurface());
        {
            Movement();
        }
        Rotation();

        canon.transform.position = transform.position + transform.up * _deltaYCanon;
    }

    void Rotation()
    {
        var rotateVelocity = new Vector3(0f, 100f, 0f);
        if (Input.GetKey(KeyCode.A))
        {
            Quaternion deltaRotation = Quaternion.Euler(-rotateVelocity * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Quaternion deltaRotation = Quaternion.Euler(rotateVelocity * Time.deltaTime);
            _rb.MoveRotation(_rb.rotation * deltaRotation);
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.AddForce(transform.forward * speed);
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
            _rb.AddForce(transform.forward * -speed);
        }
    }

    private bool IsOnSolidSurface()
    {
        // Gizmos.color = Color.red;
        var boxSize = new Vector3(2f, 1f, 2f);
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + transform.up * 3f, boxSize, -transform.up, transform.rotation, 2f, solidSurfaceLayers);
        if (hits != null && hits.Length != 0)
        {
            Debug.Log("AAAA");
            Debug.DrawLine(transform.position + transform.up * 3f, hits[0].point, Color.red);
            return true;
            // Gizmos.DrawRay(transform.position, -transform.up * hit.distance);
            // Gizmos.DrawWireCube(transform.position - transform.up * hit.distance, transform.localScale);
        }
        return false;
    }
    
    // void OnTriggerEnter(Collider other)
    // {
    //     Debug.Log("AA");
    //     _isOnSolidSurface = true;
    //     // if (IsInLayerMask(playerLayers, other.gameObject.layer))
    //     // {
    //     // }
    // }

    // void OnTriggerExit(Collider other)
    // {
    //     Debug.Log("BB");
    //     _isOnSolidSurface = false;
    //     // if (IsInLayerMask(playerLayers, other.gameObject.layer))
    //     // {
    //     // }
    // }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (IsInLayerMask(solidSurfaceLayers, other.gameObject.layer))
        {
            Debug.Log("OO");
            _isOnSolidSurface = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (IsInLayerMask(solidSurfaceLayers, other.gameObject.layer))
        {
            Debug.Log("YY");
            _isOnSolidSurface = false;
        }
    }

    bool IsInLayerMask(LayerMask mask, int layer)
    {
        return mask == (mask | 1 << layer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
            //Draw a Ray forward from GameObject toward the hit
        //  Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
        Gizmos.DrawWireCube(transform.position + transform.up, new Vector3(transform.localScale.x * 3f, transform.localScale.y, transform.localScale.z * 3f));
    }
}
