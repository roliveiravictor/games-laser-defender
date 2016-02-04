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

public class PlayerController : MonoBehaviour
 {
	public int projectileSpeed;
	public int speed;
	public int turnAngle;
	public int turnSpeed;
	public float firingRate;
	public float health;
	public float padding;
	public GameObject projectile;
	public AudioClip fireSound;

	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;
	private LevelManager levelManager;
	private HealthBar healthBar;
	
	void Start () 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		healthBar = GameObject.FindObjectOfType<HealthBar>();
		
		//Check screen resolution to fit everything
		float cameraDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 mostLeftCam = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance)); 
		Vector3 mostRightCam = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance));
		xMin = mostLeftCam.x + padding;
		xMax = mostRightCam.x - padding;
		
		Vector3 mostLowerCam = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance)); 
		Vector3 mostUpperCam = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, cameraDistance));
		yMin = mostLowerCam.y + padding;
		yMax = mostUpperCam.y - padding;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		AircraftMovement();
		Fire();
	}
	
	void AircraftMovement()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.RightArrow))
		{
			AircraftSwing(turnAngle);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			AircraftSwing(-turnAngle);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
				
		float xMovRange = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(xMovRange, transform.position.y, transform.position.z);
		
		float yMovRange = Mathf.Clamp(transform.position.y, yMin, yMax);
		transform.position = new Vector3(transform.position.x, yMovRange, transform.position.z);
	}
	
	//Rotate sprite for turning effect
	void AircraftSwing(int angle)
	{
		float step = turnSpeed * Time.deltaTime;
		Quaternion turn = Quaternion.Euler(0f, angle, 0f);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, turn, step);
	}
	
	void Fire()
	{
		if(Input.GetKeyDown(KeyCode.Space))
			InvokeRepeating("SpawnBullets", 0.000001f, firingRate);
		if (Input.GetKeyUp(KeyCode.Space))
			CancelInvoke("SpawnBullets");
	}
	
	void SpawnBullets()
	{
		Vector3 bulletPos = new Vector3(0f, 0.85f, 0f);
		GameObject shot = Instantiate(projectile, transform.position + bulletPos, Quaternion.identity) as GameObject;		
		shot.rigidbody2D.velocity = Vector3.up * projectileSpeed;
		AudioSource.PlayClipAtPoint(fireSound, transform.position, 0.25f);
	}
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile projectile = collider.gameObject.GetComponent<Projectile>();
		EnemyBehavior enemy = collider.gameObject.GetComponent<EnemyBehavior>();
		
		//if collides with projectile - get damage
		if(projectile)
		{
			projectile.Hit ();
			health -= projectile.GetDamage();
			CheckDamage();			
		}		
		
		//if collides with enemy - kill player
		if(enemy)
		{
			health = 0;
			CheckDamage();	
		}
	}
	
	void CheckDamage()
	{
		//Update Health Bar UI
		healthBar.CurrentHealth = health;
		
		//Check for game over and scene change
		if(health <= 0)
		{
			Destroy(gameObject);
			levelManager.LoadNextLevel();
		}
	}
}
