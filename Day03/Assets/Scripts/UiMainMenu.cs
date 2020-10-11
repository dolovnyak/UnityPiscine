using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMainMenu : MonoBehaviour
{
    public void LoadFirstLevel()
    {
        Application.LoadLevel("Level00");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
