using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 spawnPosition;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private int _scorePlayerLeft, _scorePlayerRight;
    // Start is called before the first frame update
    void Start()
    {
        _scorePlayerLeft = 0;
        _scorePlayerRight = 0;
        _rb = this.GetComponent<Rigidbody2D>();
        ReloadBall();
    }

    void ReloadBall()
    {
        transform.position = spawnPosition;
        _direction = new Vector2(Mathf.Round(Random.Range(0.0f, 1.0f)) == 0 ? -1.0f : 1.0f, Mathf.Round(Random.Range(0.0f, 1.0f)) == 0 ? -1 : 1);
        _rb.velocity = _direction * speed;
        Debug.Log("Player 1: " + _scorePlayerLeft + " | " + "Player 2: " + _scorePlayerRight);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Border")
        {
            _direction = new Vector2(_direction.x, -_direction.y);
            _rb.velocity = _direction * speed;
        }
        else if (other.tag == "Player")
        {
            _direction = new Vector2(-_direction.x, _direction.y);
            _rb.velocity = _direction * speed;
        }
        else if (other.name == "RespawnLeft")
        {
            _scorePlayerRight++;
            ReloadBall();
        }
        else if (other.name == "RespawnRight")
        {
            _scorePlayerLeft++;
            ReloadBall();
        }
    }
}
