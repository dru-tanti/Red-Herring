using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float offset;
	public Transform shotPoint;
	public Projectile[] projectiles;
	private int element;
    private void Update()
	{
		// Rotates the GameObject in the direction of the mouse.
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

		// If the Left Mouse Button is clicked fire a projectile in the direction the mouse is pointing.
		if(Input.GetMouseButtonDown(0))
		{
			Instantiate(projectiles[element], shotPoint.position, transform.rotation);
		}
	}

}