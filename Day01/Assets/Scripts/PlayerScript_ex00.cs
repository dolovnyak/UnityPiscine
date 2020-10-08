using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript_ex00 : MonoBehaviour
{
    public bool OnFinish { get; set; }
    public bool IsActivePlayer { get; set; }
    [SerializeField] LayerMask jumpingSurfaceLayers;
    [SerializeField] float jumpSpeed;
    [SerializeField] float movementSpeed;
    [SerializeField] GameObject mainCamera;
    private KeyCode _keyLeft1;
    private KeyCode _keyRight1;
    private KeyCode _keyLeft2;
    private KeyCode _keyRight2;
    private Rigidbody2D _rb;
    private Transform _boxForCastTransform;
    private Collider2D _boxForCastCollider;
    private bool _isStatic;

    // Start is called before the first frame update
    void Start()
    {
        OnFinish = false;
        _keyLeft1 = KeyCode.A;
        _keyRight1 = KeyCode.D;
        _keyLeft2 = KeyCode.LeftArrow;
        _keyRight2 = KeyCode.RightArrow;
        _rb = this.GetComponent<Rigidbody2D>();
        _boxForCastTransform = gameObject.transform.GetChild(0);
        _boxForCastCollider = gameObject.transform.GetChild(0).gameObject.GetComponent<Collider2D>();
        _isStatic = true;
        _rb.bodyType = RigidbodyType2D.Static;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActivePlayer && !_isStatic)
        {
            if (IsStayOnJumpingSurface())
            {
                _isStatic = true;
                _rb.bodyType = RigidbodyType2D.Static;
            }

            return;
        }
        else if (!IsActivePlayer && _isStatic)
        {
            if (!IsStayOnJumpingSurface())
            {
                _isStatic = false;
                _rb.bodyType = RigidbodyType2D.Dynamic;
            }

            return;
        }

        if (_isStatic)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _isStatic = false;
        }

        var velocity = _rb.velocity;
        if (Input.GetKey(_keyLeft1) || Input.GetKey(_keyLeft2))
        {
            velocity.x = -movementSpeed;
        }
        else if (Input.GetKey(_keyRight1) || Input.GetKey(_keyRight2))
        {
            velocity.x = movementSpeed;
        }
        else if (Input.GetKeyUp(_keyLeft1) || Input.GetKeyUp(_keyLeft2))
        {
            velocity.x = 0.0f;
        }
        else if (Input.GetKeyUp(_keyRight1) || Input.GetKeyUp(_keyRight2))
        {
            velocity.x = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsStayOnJumpingSurface())
        {
            velocity.y = jumpSpeed;
        }

        _rb.velocity = velocity;
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, mainCamera.transform.position.z);
    }

    bool IsStayOnJumpingSurface()
    {
        var hit = Physics2D.BoxCast(_boxForCastTransform.position, _boxForCastCollider.bounds.size, 0, Vector2.down, 0.0f, jumpingSurfaceLayers);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
