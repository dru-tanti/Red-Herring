using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Version of the attack script that does not rely on the radial menu.
// Instantiates the projectile prefabs depending on what element is selected
//--------------------------------------------------------------------------------------------------------------------------

public class PlayerAttack : MonoBehaviour
{
	public Transform shotPoint;
	public Projectile[] projectiles;
	public IntVariable selectedElement;
	public BoolVariable isInvisible;

    private void Update()
	{
        if(Input.GetKeyDown(KeyCode.U)){ selectedElement.Value = 0; }
        if(Input.GetKeyDown(KeyCode.I)){ selectedElement.Value = 1; }
        if(Input.GetKeyDown(KeyCode.O)){ selectedElement.Value = 2; }
        if(Input.GetKeyDown(KeyCode.P)){ selectedElement.Value = 3; }

		float rotation = Input.GetAxisRaw("Vertical") * 90;
		transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
		if(Input.GetKeyDown(KeyCode.V))
		{
			Instantiate(projectiles[selectedElement.Value], shotPoint.position, transform.rotation);
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
