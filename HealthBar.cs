/**********************************************************************/
/*                                                                    */
/*                        Laser Defender  							  */
/*																	  */
/*		This is the code from a game created to practice my           */
/*      Unity and Game Development skills. Most of this was           */
/*      made watching Ben Tristem's tutorial at Udemy.                */
/*                                                                    */
/*      For more information please visit:                            */
/*      https://www.udemy.com/unitycourse/                            */
/*                                                                    */
/*                                                                    */
/**********************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour 
{
	public Image visualHealth;
	public RectTransform healthTransform;
	
	private float yPos;
	private float xMin;
	private float xMax;
	private float currentHealth;
	private float maxHealth;
	private GameObject playerController;
	
	void Start()
	{
		//Get health from player controller
		playerController = GameObject.Find("Player");
		maxHealth = playerController.GetComponent<PlayerController>().health;
		currentHealth = maxHealth;
		
		//Acquire positions to calculate HealthMap
		yPos = healthTransform.position.y;
		xMax = healthTransform.position.x;
		xMin = healthTransform.position.x  - healthTransform.rect.width;
	}
	
	void DiscreaseHealth()
	{
		float currentX = HealthMap(currentHealth, 0, maxHealth, xMin, xMax);
		healthTransform.position = new Vector3(currentX, yPos, 0f); 
		HealthColor();
	}
	
	//Change Health Bar color based on RGB bytes from green to red
	void HealthColor()
	{
		//Case greater than half health - map color from green to yellow; otherwise from yellow to red
		if(currentHealth > maxHealth/2)
			visualHealth.color = new Color32((byte) HealthMap(currentHealth, maxHealth / 2, maxHealth, 255, 0), 255, 0, 255);
		else
			visualHealth.color = new Color32(255, (byte) HealthMap(currentHealth, 0, maxHealth / 2, 0, 255), 0, 255);
	}
	
	//Map position to slide visualHealth behind its parent mask - https://www.dropbox.com/s/yq0c26x5tifqill/HealthMap.png?dl=0
	float HealthMap(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}
	
	public float CurrentHealth
	{
		get {return currentHealth;}
		set 
		{
			currentHealth = value;
			DiscreaseHealth();	
		}
	}
}
