using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    private float spawnPeriod;
    private float previousTime;
    
    // Start is called before the first frame update
    void Start()
    {
        previousTime = 0.0f;
        spawnPeriod = Random.Range(1.5f, 3.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - previousTime > spawnPeriod)
        {
            Instantiate(cube, new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, -9.0f), Quaternion.identity);
            previousTime = Time.time;
        }
    }
}
