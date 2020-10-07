using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject pipe1;
    [SerializeField] private GameObject pipe2;
    private Bird player;
    private bool isPlayerGetScore;
    private float speed;
    float Abs(float a) => a >= 0 ? a : -a;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.01f;
        player = playerGameObject.GetComponent<Bird>();
        isPlayerGetScore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsGameOver)
            return;
        if (pipe1.transform.localPosition.x < -6.0f)
        {
            pipe1.transform.localPosition = new Vector3(9.0f, pipe1.transform.localPosition.y, pipe1.transform.localPosition.z);
            isPlayerGetScore = false;
        }
        if (pipe2.transform.localPosition.x < -6.0f)
        {
            pipe2.transform.localPosition = new Vector3(9.0f, pipe2.transform.localPosition.y, pipe2.transform.localPosition.z);
            isPlayerGetScore = false;
        }
        if (Abs(pipe1.transform.localPosition.x) >= 0.0f && Abs(pipe1.transform.localPosition.x) <= 0.01f && !isPlayerGetScore)
        {
            player.Score += 1;
            isPlayerGetScore = true;
            speed += 0.005f;
        }
        if (Abs(pipe2.transform.localPosition.x) >= 0.0f && Abs(pipe2.transform.localPosition.x) <= 0.01f && !isPlayerGetScore)
        {
            player.Score += 1;
            isPlayerGetScore = true;
            speed += 0.005f;
        }
        pipe1.transform.localPosition = new Vector3(pipe1.transform.localPosition.x - speed, pipe1.transform.localPosition.y, pipe1.transform.localPosition.z);
        pipe2.transform.localPosition = new Vector3(pipe2.transform.localPosition.x - speed, pipe2.transform.localPosition.y, pipe2.transform.localPosition.z);
    }
}
