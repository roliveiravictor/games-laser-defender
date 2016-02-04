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

public class Position : MonoBehaviour 
{
	void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position,"Enemy 1 Gizmo.PNG", true);
	}
}
