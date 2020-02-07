// @author: Andrew Tanti

using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

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
	[HideInInspector]
	public static Projectile[] element_list;
	[HideInInspector]
	public static ElementType[] cooldowns;
	private PlayerAnimation _anim;
	public ElementType[] element;
	private PlayerControl _player;
	private SpriteRenderer _sprite;
	public BoundsInt slamRange; // Will be used to find breakable tiles within this range
	public Vector3Int offset;
	private void Awake() {
		element_list = projectiles;
		cooldowns = element;
		_anim = gameObject.GetComponentInParent<PlayerAnimation>();
		_sprite = gameObject.GetComponentInParent<SpriteRenderer>();
		_player = gameObject.GetComponentInParent<PlayerControl>();
		checkElements();
	}

    private void Update() {
		slamRange.position = TilemapManager.current.grid.WorldToCell(_player.transform.position) + offset;

		if(Input.GetKeyDown(KeyCode.W)) {
			if(element[0].unlocked.Value == true) {
				selectedElement.Value = 0;
				_anim.changeElement(selectedElement.Value);
				foreach(ElementEffect passiveEffects in this.element[selectedElement.Value].passiveEffects) {
					_player.setPassive(passiveEffects);	
				}
			} 
		}
        if(Input.GetKeyDown(KeyCode.A)) { 
			if(element[1].unlocked.Value == true) {
				selectedElement.Value = 1;
				_anim.changeElement(selectedElement.Value);
				foreach(ElementEffect passiveEffects in this.element[selectedElement.Value].passiveEffects) {
					_player.setPassive(passiveEffects);	
				}			
			}
		}
        if(Input.GetKeyDown(KeyCode.S)) { 
			if(element[2].unlocked.Value == true){
				selectedElement.Value = 2;
				_anim.changeElement(selectedElement.Value);
				foreach(ElementEffect passiveEffects in this.element[selectedElement.Value].passiveEffects) {
					_player.setPassive(passiveEffects);	
				}			
			}
		}
        if(Input.GetKeyDown(KeyCode.D)) { 
			if(element[3].unlocked.Value == true) {
				selectedElement.Value = 3;
				_anim.changeElement(selectedElement.Value);
				foreach(ElementEffect passiveEffects in element[selectedElement.Value].passiveEffects) {
					_player.setPassive(passiveEffects);	
				}			
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
		if(effect.projectile) {
			Instantiate(this.projectiles[selectedElement.Value], shotPoint.position, transform.rotation);
			StartCoroutine(_player.abilityCoolingdown(_player.cooldowns[selectedElement.Value], effect.cooldown, 0));
		}

		if(effect.turnInvisible) {
			Invisible(effect.activeTime);
			StartCoroutine(_player.abilityCoolingdown(_player.cooldowns[selectedElement.Value], effect.cooldown, 0));
		}

		if(effect.groundEffect) {
			groundSlam(this.slamRange);
			StartCoroutine(_player.abilityCoolingdown(_player.cooldowns[selectedElement.Value], effect.cooldown, 0));	
		}
	}

	private void groundSlam(BoundsInt slamRange) {
		for(int x=0; x < slamRange.size.x; x++) {
			for(int y=0; y < slamRange.size.y; y++) {
				Vector3Int pos = new Vector3Int(slamRange.position.x + x, slamRange.position.y + y, 0);
				TileBase tile = TilemapManager.current.tilemap.GetTile(pos);
				if (tile is BreakableTile) {
					TilemapManager.current.tilemap.SetTile(pos, null);
				}
			}
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
			if(element[i].unlocked.Value == true) {
				selectedElement.Value = i;
				_anim.changeElement(i);
				foreach(ElementEffect passiveEffects in this.element[selectedElement.Value].passiveEffects) {
					_player.setPassive(passiveEffects);	
				}			
				return;
			}
		}
	}
	
	private void OnDrawGizmos() {
		if (shotPoint != null) {
			// Draws a crosshair on the shotpoint
			Gizmos.color = Color.blue;
			Gizmos.DrawWireSphere(shotPoint.position, 0.5f);
		}

		// Draws a box around the player to show the range of the ground slam/
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(slamRange.center, slamRange.size);
	}
}
