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

public class EnemySpawner : MonoBehaviour 
{
	public GameObject enemyPrefab1;
	public GameObject enemyPrefab2;
	public GameObject enemyPrefab3;
	public GameObject enemyPrefab4;
	public GameObject[] enemies;
	public float gizWidth;
	public float gizHeight;
	public float spawnDelay;
	public int movementSpeed;
	
	private bool movingRight = true;
	private float xMin;
	private float xMax;

	void Start ()
	{
		//Instantiate different enemies type to be spawned
		enemies = new GameObject[4];
		enemies.SetValue(enemyPrefab1, 0);
		enemies.SetValue(enemyPrefab2, 1);
		enemies.SetValue(enemyPrefab3, 2);
		enemies.SetValue(enemyPrefab4, 3);
	
		//Check screen resolution to fit everything
		float cameraDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance)); 
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance));
		xMin = leftBoundary.x;
		xMax = rightBoundary.x;
		
		//Spawn enemies until formation gets full
		SpawnUntilFull();
	}
	
	// Update is called once per frame
	void Update ()
	{
		MoveEnemies();
		
		if(AllMembersDead())
			SpawnUntilFull();
	}
	
	//Check to spawn another enemy formation
	bool AllMembersDead()
	{
		//Transform every child (Position) on the Game Object (EnemySpawner) which is calling EnemySpawner.cs
		foreach(Transform childPosition in transform)
		{
			if(childPosition.childCount > 0)
				return false;
		}
		
		return true;
	}
	
	Transform SpawnAtFreePosition()
	{
		foreach(Transform childPosObject in transform)
		{
			if(childPosObject.childCount == 0)
				return childPosObject;
		}
		
		return null;
	}

	void SpawnUntilFull()
	{
		Transform freePos = SpawnAtFreePosition();
		
		if(freePos)
		{
			GameObject enemy = Instantiate(enemies.GetValue(Random.Range(0,4)) as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
			
			//Attach spawned Enemy to its parent Game Object (Position)
			enemy.transform.SetParent(freePos, false);
		}
		
		if(SpawnAtFreePosition())
			Invoke("SpawnUntilFull", spawnDelay);
	}
	
	void MoveEnemies()
	{
		if(movingRight)
			transform.position += Vector3.right * movementSpeed * Time.deltaTime;
		else
			transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
		
		float leftBoundarySpawner = transform.position.x - (0.5f * gizWidth);
		float rightBoundarySpawner = transform.position.x + (0.5f * gizWidth);
		
		if(leftBoundarySpawner < xMin)
			movingRight = true;
		else if (rightBoundarySpawner > xMax)
			movingRight = false;

	}
	
	//Custom gizmo added to scene for better visualization
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gizWidth, gizHeight, 0));
	}
}
