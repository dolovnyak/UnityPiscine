using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript_ex00 : MonoBehaviour
{
    public bool IsActivePlayer { get; set; }
    [SerializeField] LayerMask layers;
    [SerializeField] float jumpSpeed;
    [SerializeField] float movementSpeed;
    private KeyCode _keyLeft1;
    private KeyCode _keyRight1;
    private KeyCode _keyLeft2;
    private KeyCode _keyRight2;
    private Rigidbody2D _rb;
    private Transform _circleTransform;
    private float _circleRadius;

    // Start is called before the first frame update
    void Start()
    {
        _keyLeft1 = KeyCode.A;
        _keyRight1 = KeyCode.D;
        _keyLeft2 = KeyCode.LeftArrow;
        _keyRight2 = KeyCode.RightArrow;
        _rb = this.GetComponent<Rigidbody2D>();
        _circleTransform = gameObject.transform.GetChild(0);
        _circleRadius = this.GetComponent<Collider2D>().bounds.size.x / 2.0f - 0.01f;
        Debug.Log(_circleRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActivePlayer)
        {
            return;
        }

        var velocity = _rb.velocity;
        if (Input.GetKey(_keyLeft1) || Input.GetKey(_keyLeft2))
        {
            velocity.x = -2.5f;
        }
        else if (Input.GetKey(_keyRight1) || Input.GetKey(_keyRight2))
        {
            velocity.x = 2.5f;
        }
        else if (Input.GetKeyUp(_keyLeft1) || Input.GetKeyUp(_keyLeft2))
        {
            velocity.x = 0.0f;
        }
        else if (Input.GetKeyUp(_keyRight1) || Input.GetKeyUp(_keyRight2))
        {
            velocity.x = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsCanJump())
        {
            velocity.y = jumpSpeed;
        }

        _rb.velocity = velocity;
    }

    bool IsCanJump()
    {
        var hit = Physics2D.CircleCast((Vector2)_circleTransform.position, _circleRadius, Vector2.down, 0.0f, layers);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
