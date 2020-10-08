using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExit : MonoBehaviour
{
    [SerializeField] private GameObject playerObj;
    private PlayerScript_ex00 _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = playerObj.GetComponent<PlayerScript_ex00>();
    }

    // Update is called once per frame
    void Update()
    {
        if (((Vector2)playerObj.transform.position - (Vector2)gameObject.transform.position).magnitude < 0.1f)
        {
            _player.OnFinish = true;
        }
        else
        {
            _player.OnFinish = false;
        }
    }
}
