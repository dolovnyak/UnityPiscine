using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public bool IsGameOver { get; set; }
    public int Score { get; set; }
    [SerializeField] private float gravitation;
    [SerializeField] private float jumpEnergy;
    private float movement;

    // Start is called before the first frame update
    void Start()
    {
        IsGameOver = false;
        movement = 0.0f;
        Score = 0;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Score: " + Score);
        Debug.Log("Time: " + Mathf.RoundToInt(Time.time) + "s");
        IsGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            return;
        }
        if (Input.GetKeyDown("space"))
        {
            movement = jumpEnergy;
        }
        movement -= gravitation;
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + movement * Time.deltaTime, gameObject.transform.localPosition.z);
    }
}
