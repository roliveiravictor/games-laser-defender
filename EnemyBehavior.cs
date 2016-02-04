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

public class EnemyBehavior : MonoBehaviour 
{
	public float health;
	public float shotsPerSecond;
	public int projectileSpeed;
	public int scorePoints;
	public AudioClip fireSound;
	public AudioClip explosionSound;
	public GameObject projectile;

	private ScoreKeeper scoreKeeper;
	
	void Start()
	{
		scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
	}

	void Update()
	{
		Fire();
	}
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile projectile = collider.gameObject.GetComponent<Projectile>();
		
		if(projectile)
		{
			projectile.Hit ();
			health -= projectile.GetDamage();
			
			if(health <= 0)
			{
				Destroy(gameObject);
				AudioSource.PlayClipAtPoint(explosionSound, transform.position, 0.25f);
				AddToScore();
			}
		}
	}
	
	void Fire()
	{
		float probability = Time.deltaTime * shotsPerSecond;
		
		if(Random.value < probability)
			SpawnBullets();
	}
	
	void SpawnBullets()
	{
		Vector3 bulletPos = new Vector3(0f, -0.5f, 0f);
		GameObject shot = Instantiate(projectile, transform.position + bulletPos, Quaternion.identity) as GameObject;		
		shot.rigidbody2D.velocity = Vector3.down * projectileSpeed;
		AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.25f);
	}
	
	//When enemy dies - increase score
	void AddToScore()
	{
		scoreKeeper.SetScore(scorePoints);
	}
}
