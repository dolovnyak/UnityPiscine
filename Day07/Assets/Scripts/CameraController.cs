using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject canon = null;
    [SerializeField] private float cameraSensevity = 200;
    private float _rotationX;

    void Start()
    {
        transform.forward = canon.transform.forward;
        transform.position = canon.transform.position - canon.transform.forward * 20f + canon.transform.up * 2f;
    }

    void Update()
    {
        _rotationX += Input.GetAxis("Mouse X") * cameraSensevity * Time.deltaTime;
        transform.localRotation = Quaternion.AngleAxis(_rotationX, Vector3.up);
        canon.transform.localRotation = Quaternion.AngleAxis(_rotationX, Vector3.up);
        var newCamPos = canon.transform.position - canon.transform.forward * 20f + canon.transform.up * 2f;
        transform.position = Vector3.Lerp(transform.position, newCamPos, 20f * Time.deltaTime);
    }
}
