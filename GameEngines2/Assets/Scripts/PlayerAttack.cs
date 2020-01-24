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
	private PlayerAnimation _anim;
	public ElementType[] element;
	private PlayerControl _player;
	private SpriteRenderer _sprite;
	private void Awake() {
		_anim = gameObject.GetComponentInParent<PlayerAnimation>();
		_sprite = gameObject.GetComponentInParent<SpriteRenderer>();
		_player = gameObject.GetComponentInParent<PlayerControl>();
		checkElements();
	}
    private void Update()
	{
        if(Input.GetKeyDown(KeyCode.U)) {
			if(element[0].unlocked.Value == true) {
				selectedElement.Value = 0; 
				_anim.changeElement(selectedElement.Value); 
			} 
		}
        if(Input.GetKeyDown(KeyCode.I)) { 
			if(element[1].unlocked.Value == true) {
				selectedElement.Value = 1; 
				_anim.changeElement(selectedElement.Value); 
			}
		}
        if(Input.GetKeyDown(KeyCode.O)) { 
			if(element[2].unlocked.Value == true){
				selectedElement.Value = 2; 
				_anim.changeElement(selectedElement.Value); 
			}
		}
        if(Input.GetKeyDown(KeyCode.P)) { 
			if(element[3].unlocked.Value == true) {
				selectedElement.Value = 3;
				_anim.changeElement(selectedElement.Value); 
			}
		}

		float rotation = Input.GetAxisRaw("Vertical") * 90;
		transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
		if(Input.GetKeyDown(KeyCode.V) && element[selectedElement.Value] != null) {
			if(_player.cooldowns[selectedElement.Value].abilityAvailable[0].Value) {
				foreach(ElementEffect attackEffects in element[selectedElement.Value].attackEffects) {
					UseEffect(attackEffects);
				}
			} else {
				Debug.Log("Ability not yet available");
				return;
			}
		}
	}

	private void UseEffect(ElementEffect effect) {
		if(effect.projectile){
			Instantiate(this.projectiles[selectedElement.Value], shotPoint.position, transform.rotation);
			StartCoroutine(_player.abilityCoolingdown(_player.cooldowns[selectedElement.Value], effect.cooldown, 0));
		}

		if(effect.turnInvisible) {
			Invisible(effect.invisibleTime);
			StartCoroutine(_player.abilityCoolingdown(_player.cooldowns[selectedElement.Value], effect.cooldown, 0));
		}
	}
	
	private void Invisible(float invisibleTime) {
		if(isInvisible.Value == true) return;
		StartCoroutine(turnInvisible(invisibleTime));
	}

	private IEnumerator turnInvisible(float time) {
		isInvisible.Value = true;
		while(isInvisible.Value) {
			_sprite.color = new Color (1f, 1f, 1f, 0.5f);
			yield return new WaitForSeconds(time);
			isInvisible.Value = false;
			_sprite.color = new Color (1f, 1f, 1f, 1f);
		}
	}

	// Finds the first unlocked element, and sets that as the current active.
	private void checkElements() {
		for(int i = 0; i < element.Length; i++) {
			if(element[i].unlocked.Value == true){
				selectedElement.Value = i;
				_anim.changeElement(i);
				return;
			}
		}
	}
	
	private void OnDrawGizmos() {
		if (shotPoint != null) {
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(shotPoint.position, 0.5f);
		}
	}
}
