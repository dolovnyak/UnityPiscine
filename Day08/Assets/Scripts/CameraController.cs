using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private float cameraMinDistance = 0f;
    [SerializeField] private float cameraMaxDistance = 0f;
    [SerializeField] private float cameraSensevity = 0f;
    private Camera _camera;
    private float _cameraDist;
    private Vector3 _playerCameraDir;


    void Start()
    {
        _camera = GetComponent<Camera>();
        _playerCameraDir = Vector3.Normalize(_camera.transform.position - player.transform.position);
        _cameraDist = (_camera.transform.position - player.transform.position).magnitude;
    }

    void Update()
    {
        LookAtPlayer();
        FollowPlayerAndChangeHeight();
        RotateAroundPlayer();
        if (_camera.transform.position.y < 1)
            _camera.transform.position = new Vector3(_camera.transform.position.x, 1, _camera.transform.position.z);
    }

    void FollowPlayerAndChangeHeight()
    {
        if (Input.mouseScrollDelta.y > 0 && _cameraDist < cameraMaxDistance)
        {
            _cameraDist += 0.5f;
        }
        else if (Input.mouseScrollDelta.y < 0 && _cameraDist > cameraMinDistance)
        {
            _cameraDist -= 0.5f;
        }
        var newCameraPosition = player.transform.position + _playerCameraDir * _cameraDist;
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, newCameraPosition, 0.9f + Time.deltaTime);
    }

    void LookAtPlayer()
    {
        _camera.transform.forward = Vector3.Normalize(player.transform.position - _camera.transform.position);
    }

    void RotateAroundPlayer()
    {
        if (Input.GetMouseButton(0))
        {
            var rotationX = Input.GetAxis("Mouse X") * cameraSensevity * Time.deltaTime;
            _camera.transform.RotateAround(player.transform.position, Vector3.up, rotationX);
            var rotationY = Input.GetAxis("Mouse Y") * cameraSensevity * Time.deltaTime;
            _camera.transform.RotateAround(player.transform.position, Vector3.right, rotationY);
            _playerCameraDir = Vector3.Normalize(_camera.transform.position - player.transform.position);
        }
    }
}
