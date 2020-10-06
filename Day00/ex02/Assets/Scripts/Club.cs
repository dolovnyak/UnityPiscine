using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    [SerializeField] private GameObject ballGameObject;
    [SerializeField] private GameObject hole;
    private Ball ball;
    private bool isSwing;
    private float punchPower;
    private bool previousIsBallMoving;
    private float previousClubPosY;

    // Start is called before the first frame update
    void Start()
    {
        ball = ballGameObject.GetComponent<Ball>();
        isSwing = false;
        punchPower = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ball.IsDestroyed)
        {
            if (!ball.IsMoving)
            {
                if (Input.GetKeyDown("space"))
                {
                    isSwing = true;
                }
                else if (Input.GetKeyUp("space"))
                {
                    isSwing = false;
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, ball.transform.localPosition.y, gameObject.transform.localPosition.z);
                    if (ball.transform.localPosition.y > hole.transform.localPosition.y)
                    {
                        ball.GotHit(punchPower, -1);
                    }
                    else
                    {
                        ball.GotHit(punchPower, 1);
                    }
                    punchPower = 0.0f;
                }
                if (previousIsBallMoving)
                {
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, ball.transform.localPosition.y, gameObject.transform.localPosition.z);
                    if (gameObject.transform.localPosition.y < hole.transform.localPosition.y)
                    {
                        gameObject.transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                    }
                    else
                    {
                        gameObject.transform.localRotation = new Quaternion(1.0f, 0.0f, 0.0f, 0.0f);
                    }
                }
            }
            if (isSwing)
            {
                if (punchPower < 1.0f)
                    punchPower += 0.002f;
                if (gameObject.transform.localPosition.y < hole.transform.localPosition.y)
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y - 0.01f, gameObject.transform.localPosition.z);
                if (gameObject.transform.localPosition.y > hole.transform.localPosition.y)
                    gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y + 0.01f, gameObject.transform.localPosition.z);
            }

            previousIsBallMoving = ball.IsMoving;
        }
    }
}
