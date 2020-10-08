using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject john;
    [SerializeField] private GameObject thomas;
    [SerializeField] private GameObject claire;
    private PlayerScript_ex00 _john;
    private PlayerScript_ex00 _thomas;
    private PlayerScript_ex00 _claire;

    // Start is called before the first frame update
    void Start()
    {
        _john = john.GetComponent<PlayerScript_ex00>();
        _thomas = thomas.GetComponent<PlayerScript_ex00>();
        _claire = claire.GetComponent<PlayerScript_ex00>();
        _john.IsActivePlayer = true;
        _thomas.IsActivePlayer = false;
        _claire.IsActivePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            _john.IsActivePlayer = true;
            _thomas.IsActivePlayer = false;
            _claire.IsActivePlayer = false;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            _john.IsActivePlayer = false;
            _thomas.IsActivePlayer = true;
            _claire.IsActivePlayer = false;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            _john.IsActivePlayer = false;
            _thomas.IsActivePlayer = false;
            _claire.IsActivePlayer = true;
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (_john.OnFinish && _claire.OnFinish && _thomas.OnFinish)
        {
            Debug.Log("Nice");
            if (Application.loadedLevelName == "ex00_ex01")
            {
                Application.LoadLevel("ex02");
            }
            if (Application.loadedLevelName == "ex02")
            {
                Application.LoadLevel("ex00_ex01");
            }
        }
    }
}
