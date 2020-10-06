using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject hole;
    public bool IsMoving { get; private set; }
    public bool IsDestroyed { get; private set; }
    private float movement;
    private int dir;
    private int score;
    float Abs(float a) => a >= 0 ? a : -a;

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        IsDestroyed = false;
        movement = 0.0f;
        dir = 1;
        score = -20;
    }

    public void GotHit(float punchPower, int dir)
    {
        IsMoving = true;
        movement = punchPower * 100;
        this.dir = dir;
        score += 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (movement > 0.0f)
        {
            if (gameObject.transform.localPosition.y >= 4.5f)
            {
                dir = -1;
            }
            else if (gameObject.transform.localPosition.y <= -4.5f)
            {
                dir = 1;
            }
            movement -= 0.1f;
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + movement * dir * Time.deltaTime, gameObject.transform.localPosition.z);
        }
        else
        {
            if (Abs(gameObject.transform.localPosition.y - hole.transform.localPosition.y) < 0.4)
            {
                Destroy(gameObject);
                IsDestroyed = true;
                Debug.Log("Score: " + score);
            }
            IsMoving = false;
        }
    }
}
