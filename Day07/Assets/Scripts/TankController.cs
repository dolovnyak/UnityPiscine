using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TankController : MonoBehaviour
{
    [SerializeField] private GameObject canon = null;
    [SerializeField] private float speed = 0;
    [SerializeField] private float speedUp = 0;
    [SerializeField] private LayerMask solidSurfaceLayers = 0;
    [SerializeField] private float mouseRotationSensevity = 200;
    private bool _isOnSolidSurface;
    private Rigidbody _rb;
    private float _deltaYCanon;
    private float _canonRotation;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = new Vector3(0f, 0f, 0f);
        _deltaYCanon = Mathf.Abs(transform.position.y - canon.transform.position.y);
    }

    void Update()
    {
        Debug.Log("tank: " + transform.forward);
        Debug.Log("world: " + Vector3.forward);
        Debug.Log("angle: " + Vector3.Angle(new Vector3(Vector3.forward.x, transform.forward.y, Vector3.forward.z), Vector3.forward));

        if (_isOnSolidSurface)
        {
            Movement();
        }
        Rotation();

        if (Vector3.Angle(new Vector3(Vector3.down.x, transform.up.y, Vector3.down.z), Vector3.down) <= 30)
        {
            transform.up = -transform.up;
        }

        UpdateCanon();
    }

    void UpdateCanon()
    {
        canon.transform.position = transform.position + transform.up * _deltaYCanon;
        _canonRotation += Input.GetAxis("Mouse X") * mouseRotationSensevity * Time.deltaTime;
        canon.transform.localRotation = Quaternion.Lerp(canon.transform.localRotation, Quaternion.AngleAxis(_canonRotation, Vector3.up), 10f * Time.deltaTime);
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
        float curSpeedUp = 1f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            curSpeedUp = speedUp;
        }
        if (Input.GetKey(KeyCode.W) && (Vector3.Angle(new Vector3(Vector3.forward.x, transform.forward.y, Vector3.forward.z), Vector3.forward) <= 30f || transform.forward.y <= 0))
        {
            _rb.AddForce(transform.forward * speed * curSpeedUp);
            // _rb.velocity = transform.forward * speed * curSpeedUp;
            // transform.Translate(transform.forward * speed * curSpeedUp * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) && (Vector3.Angle(new Vector3(Vector3.forward.x, transform.forward.y, Vector3.forward.z), Vector3.forward) <= 30f || transform.forward.y >= 0))
        {
            _rb.AddForce(transform.forward * -speed * curSpeedUp);
            // _rb.velocity = transform.forward * -speed * curSpeedUp;
            // transform.Translate(transform.forward * -speed * curSpeedUp * Time.deltaTime);
        }

        // RaycastHit hit;
        // if (Physics.Raycast(pizdecForward.position, -pizdecForward.up, out hit))
        // {
        //     agent.SetDestination(hit.point);
        //     Debug.Log("hit: " + hit.point);
        //     Debug.Log("obj: " + hit.collider.gameObject.name);
        // }
    }

    void OnTriggerStay(Collider other)
    {
        if (IsInLayerMask(solidSurfaceLayers, other.gameObject.layer))
        {
            _isOnSolidSurface = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsInLayerMask(solidSurfaceLayers, other.gameObject.layer))
        {
            _isOnSolidSurface = false;
        }
    }

    // void OnCollisionEnter(Collision other)
    // {
    //     Debug.Log(other.gameObject.name);
    //     if (IsInLayerMask(solidSurfaceLayers, other.gameObject.layer))
    //     {
    //         Debug.Log("OO");
    //         _isOnSolidSurface = true;
    //     }
    // }

    // void OnCollisionExit(Collision other)
    // {
    //     if (IsInLayerMask(solidSurfaceLayers, other.gameObject.layer))
    //     {
    //         Debug.Log("YY");
    //         _isOnSolidSurface = false;
    //     }
    // }

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
