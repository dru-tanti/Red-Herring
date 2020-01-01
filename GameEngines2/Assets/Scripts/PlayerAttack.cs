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
	
	[HideInInspector]
	public static Projectile[] elements;
	
	public IntVariable selectedElement;
	public BoolVariable isInvisible;
	public IntVariable multiplier;

	private int _timer = 0;

	//Allows the ManaMeter to read the projectiles array via a proxy
	void Awake(){
		elements = projectiles;
	}

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
			//Player can only fire if they have a minimum of 10 mana
			if(this.GetComponentInParent<PlayerMana>().mana.Value >= 10){
				Instantiate(projectiles[selectedElement.Value], shotPoint.position, transform.rotation);
				this.GetComponentInParent<PlayerMana>().mana.Value -= 10;
				
				//Invokes and cancels a timer
				if(_timer != 0){
					CancelInvoke("OutOfCombatTimer");
					_timer = 0;
				}

				InvokeRepeating("OutOfCombatTimer", 0, 1);
			}
		}

		if(_timer >= 10){
			CancelInvoke("OutOfCombatTimer");
			_timer = 0;
			multiplier.Value = 0;
		} 
	}

	private void OutOfCombatTimer(){
		_timer += 1;
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
