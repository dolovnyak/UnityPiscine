using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private int breatheCoolDown = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localScale.x < 0.001f || gameObject.transform.localScale.x > 0.3f)
        {
            Debug.Log("Balloon life time: " + Mathf.RoundToInt(Time.time) + "s");
            GameObject.Destroy(gameObject);
        }
        if (Input.GetKeyDown("space") && breatheCoolDown < 200)
        {
            breatheCoolDown += 70;
            gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x + 0.02f, gameObject.transform.localScale.y + 0.02f, gameObject.transform.localScale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x - 0.0003f, gameObject.transform.localScale.y - 0.0003f, gameObject.transform.localScale.z);
        }

        if (breatheCoolDown != 0)
            breatheCoolDown--;
        
        Debug.Log("breathe: " + breatheCoolDown);
    }
}
