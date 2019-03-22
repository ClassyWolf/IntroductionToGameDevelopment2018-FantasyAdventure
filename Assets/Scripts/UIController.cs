using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] private Text HealthText;
    [SerializeField] private Text HookText;

	void Start ()
    {
        PlayerManager.instance.uIHandler = GetComponent<UIController>();


       //HealthText = GetComponent<Text>();
       // HookText = GetComponent<Text>();

        HealthText.text = "Health: " + PlayerManager.instance.healthLeft.ToString();
        HookText.text = "Hooks left: " + PlayerManager.instance.hooksLeft.ToString();

        //HealtText.text = "Healt: " + characterStatus.healthPoints;
        //HookText.text = "Hooks left: " + playerStatus.hookCounter;
	}
	

    public void UpdateResources(int health, int hooks)
    {
        HealthText.text = "Health:" + health.ToString();
        HookText.text = "Hooks left: " + hooks.ToString();
    }
}
