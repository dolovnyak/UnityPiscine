using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject pipe1;
    [SerializeField] private GameObject pipe2;
    private Bird player;

    // Start is called before the first frame update
    void Start()
    {
        player = playerGameObject.GetComponent<Bird>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsGameOver)
            return;
        if (pipe1.transform.localPosition.x < -6.0f)
        {
            pipe1.transform.localPosition = new Vector3(9.0f, pipe1.transform.localPosition.y, pipe1.transform.localPosition.z);
            player.Score += 1;
        }
        if (pipe2.transform.localPosition.x < -6.0f)
        {
            pipe2.transform.localPosition = new Vector3(9.0f, pipe2.transform.localPosition.y, pipe2.transform.localPosition.z);
            player.Score += 1;
        }
        pipe1.transform.localPosition = new Vector3(pipe1.transform.localPosition.x - 0.01f, pipe1.transform.localPosition.y, pipe1.transform.localPosition.z);
        pipe2.transform.localPosition = new Vector3(pipe2.transform.localPosition.x - 0.01f, pipe2.transform.localPosition.y, pipe2.transform.localPosition.z);
    }
}
