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

public class ScoreKeeper : MonoBehaviour
 {	
	public static int score = 0;
	
	private Text scoreText;
	
	void Start()
	{
		scoreText = GetComponent<Text>();
		Reset();
	}
	
	public void SetScore(int points)
	{
		score += points;
		scoreText.text = "Score: " + score.ToString();
	}
	
	public static void Reset ()
	{
		score = 0;
	}
}
