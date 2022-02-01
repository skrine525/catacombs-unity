using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSupportingProgressBars : MonoBehaviour {
	[SerializeField] private PlayerLifeSupporting playerLifeSupporting;
	[SerializeField] private Image healthProgressBarForeground;
	[SerializeField] private Image thirstProgressBarForeground;
	[SerializeField] private Image hungerProgressBarForeground;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		healthProgressBarForeground.fillAmount = playerLifeSupporting.health / 100f;
		thirstProgressBarForeground.fillAmount = playerLifeSupporting.thirst / 100f;
		hungerProgressBarForeground.fillAmount = playerLifeSupporting.hunger / 100f;
	}
}
