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
using System.Collections;

public class MusicManager : MonoBehaviour
{
	static MusicManager musicManager = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Start()
	{
		if(musicManager != null && musicManager != this)
		{
			Destroy(gameObject);
		}
		else
		{
			musicManager = this;
			GameObject.DontDestroyOnLoad(musicManager);
			MusicParameters();		
		}
	}
	
	//Game start music parameters
	void MusicParameters()
	{
		music = GetComponent<AudioSource>();
		music.clip = startClip;
		music.loop = true;
		music.Play();
	}
	
	//Check for loaded levels and set new music to play
	void OnLevelWasLoaded(int level)
	{
		music.Stop();
	
		if(level == 0)
			music.clip = startClip;
		if(level == 1)
			music.clip = gameClip;
		if(level == 2)
			music.clip = endClip;
		
		music.loop = true;
		music.Play();
	}
}
