using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField] private Vector2 destination;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask playerLayers;
    private Vector2 _startPosition;
    private Vector2 _direction;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = (Vector2)gameObject.transform.position;
        _direction = (destination - _startPosition).normalized;
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (((Vector2)transform.position - destination).magnitude < 0.001)
        {
            _direction = -_direction;
        }
        else if (((Vector2)transform.position - _startPosition).magnitude < 0.001)
        {
            _direction = -_direction;
        }

        var tmp = _direction * speed * Time.deltaTime;
        // _rb.velocity = tmp;

        transform.position = new Vector3(transform.position.x + tmp.x, transform.position.y + tmp.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(playerLayers, other.gameObject.layer))
        {
            other.transform.parent.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsInLayerMask(playerLayers, other.gameObject.layer))
        {
            other.transform.parent.transform.SetParent(null);
        }
    }

    bool IsInLayerMask(LayerMask mask, int layer)
    {
        return mask == (mask | 1 << layer);
    }
}
