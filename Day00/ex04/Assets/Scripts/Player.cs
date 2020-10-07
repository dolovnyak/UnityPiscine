using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode keyUp;
    [SerializeField] private KeyCode keyDown;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyUp) && transform.position.y <= 3.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
        }
        else if (Input.GetKey(keyDown) && transform.position.y >= -3.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
        }
    }
}
