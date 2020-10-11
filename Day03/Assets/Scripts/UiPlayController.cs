using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPlayController : MonoBehaviour
{
	[SerializeField] private GameObject gameManager;
	[SerializeField] private GameObject textPlayButtonsField;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject sureMenu;
	private gameManager manager;
  private bool _isPaused;
  private bool _isPausedMenu;

   // Start is called before the first frame update
  void Start()
  {
    manager = gameManager.GetComponent<gameManager>();
    _isPaused = true;
		textPlayButtonsField.GetComponent<Text>().text = "click play to start";
    pauseMenu.SetActive(false);
    sureMenu.SetActive(false);
    _isPausedMenu = false;
  }

  public void PlaySpeedX1()
  {
		textPlayButtonsField.GetComponent<Text>().text = "speed: 1x";
    manager.changeSpeed(1);
    _isPaused = false;
  }

  public void PlaySpeedX2()
  {
		textPlayButtonsField.GetComponent<Text>().text = "speed: 2x";
    manager.changeSpeed(2);
    _isPaused = false;
  }

  public void PlaySpeedX4()
  {
		textPlayButtonsField.GetComponent<Text>().text = "speed: 4x";
    manager.changeSpeed(4);
    _isPaused = false;
  }

  public void Pause()
  {
    if (!_isPaused)
    {
      textPlayButtonsField.GetComponent<Text>().text = "paused";
      manager.pause(true);
      _isPaused = true;
    }
  }

  public void ShowMenu()
  {
    pauseMenu.SetActive(true);
    sureMenu.SetActive(false);
    _isPausedMenu = true;
    manager.pause(true);
  }

  public void HideMenu()
  {
    pauseMenu.SetActive(false);
    sureMenu.SetActive(false);
    _isPausedMenu = false;
    manager.pause(false);
  }

  public void ShowSureMenu()
  {
    sureMenu.SetActive(true);
  }

  public void GoOnMainMenu()
  {
    Application.LoadLevel("MainMenu");
  }

	void Update()
	{
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (!_isPausedMenu)
      {
        ShowMenu();
      }
      else
      {
        HideMenu();
      }
    }
	}
}
