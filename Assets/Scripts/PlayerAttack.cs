using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Version of the attack script that does not rely on the radial menu.
public class PlayerAttack : MonoBehaviour
{
	public Transform shotPoint;
	public Projectile[] projectiles;
	public int selectedElement;
    [Header("Fire Variables")]
    public float dashSpeed;
    public PlayerControl player;


    private void Awake() 
    {
        player = GetComponent<PlayerControl>();
    }
    private void Update()
	{
        Fire();
		// if(Input.GetKeyDown(KeyCode.V))
		// {
		// 	Instantiate(projectiles[selectedElement], shotPoint.position, transform.rotation);
		// }
	}

	private void OnDrawGizmos()
	{
		if (shotPoint != null)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(shotPoint.position, 0.5f);
		}
	}

    private void Fire()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            Instantiate(projectiles[2], shotPoint.position, transform.rotation);
        }
    }
}
