using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Controls the Weapon that is attached to the player.
//--------------------------------------------------------------------------------------------------------------------------

public class Attack : MonoBehaviour
{
	// Use only if spawn does not line up properly.
	public float offset;
	public Transform shotPoint;
	public Projectile[] projectiles;
	// This will be set from the RadialMenu script depending on what element was selected from the menu.
	public int selectedElement;

    private void Update()
	{
		// If the Left Mouse Button is clicked fire a projectile in the direction the mouse is pointing.
		if(Input.GetMouseButtonDown(0))
		{
			// Rotates the GameObject in the direction of the mouse.
			Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
			Instantiate(projectiles[selectedElement], shotPoint.position, transform.rotation);
		}
	}

	private void OnDrawGizmos()
	{
		if (shotPoint != null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(shotPoint.position, 0.5f);
		}
	}
}