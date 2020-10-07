using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    public float Speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localPosition.y < -6.0f)
        {
            Destroy(gameObject);
        }
        else if (Input.GetKeyDown(key))
        {
            Debug.Log("Precision: " + (gameObject.transform.localPosition.y + 4.0f));
            Destroy(gameObject);
        }

        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x,
            gameObject.transform.localPosition.y - Speed, gameObject.transform.localPosition.z);
    }
}
