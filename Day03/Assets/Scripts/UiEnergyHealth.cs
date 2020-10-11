using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiEnergyHealth : MonoBehaviour
{
	[SerializeField] private GameObject gameManager;
	[SerializeField] private GameObject healthText;
	[SerializeField] private GameObject energyText;
	private gameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = gameManager.GetComponent<gameManager>(); 
		healthText.GetComponent<Text>().text = manager.playerHp.ToString();
		energyText.GetComponent<Text>().text = manager.playerEnergy.ToString();
    }

    void Update()
    {
		healthText.GetComponent<Text>().text = manager.playerHp.ToString();
		energyText.GetComponent<Text>().text = manager.playerEnergy.ToString();
    }
}