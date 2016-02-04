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

public class ScoreDisplay : MonoBehaviour 
{	
	//Display score points on game over
	void Start()
	{
		Text scoreDisplay = this.GetComponent<Text>();
		scoreDisplay.text = "Score: " + ScoreKeeper.score.ToString();
		
		//Reset to play again if needed
		ScoreKeeper.Reset();
	}
}
