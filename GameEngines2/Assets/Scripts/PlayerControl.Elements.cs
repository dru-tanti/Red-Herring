using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityAtoms;

//--------------------------------------------------------------------------------------------------------------------------
// Contains the main element controls and effects of the player
//--------------------------------------------------------------------------------------------------------------------------
public partial class PlayerControl
{
    [Header("Element")]
	public IntVariable selectedElement;
    public ElementType[] element;
    public ElementCooldown[] cooldowns;

    [SerializeField] private GameObject shield = null;
    private Coroutine _floatCoroutine;
    private Coroutine _shieldBubble;
    private bool _floating;
    private bool _dashing = false;

    // Passive Ability Booleans.
    public BoolVariable _lavaResistant, _rockStrength, _wallJump, _swimming;

    // Triggers different methods depending on the effects active.
    private void UseEffect(ElementEffect effect) {
        if (effect == null) return;

        // Primary abilities are located in PlayerAttack, and in Projectile.

        // Secondary Abilities
        if (effect.willDash && !_dashing) {
            Dash(effect.dashForce, effect.activeTime);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));
        }

        if(effect.willDig) {
            Dig(terrain.cellAim, terrain.tileAim);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));
        }

        if (effect.immune) {
            Immune(effect.activeTime);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));
        }

        if (effect.willFloat) {
            Float(effect.floatSpeed, effect.activeTime);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));
        }
    }

    //--------------------------------------------------------------
    // Secondary Ability Effects
    //--------------------------------------------------------------

    // Applies a force in the direction the player is facing.
    void Dash(float dashForce, float dashTime) {
        if(_dashing == true) return;
        StartCoroutine(Dashing(dashForce, dashTime));
    }
    // Applies the dash force and stops the player from moving while dash is active.
    private IEnumerator Dashing(float dashForce, float dashTime) {
        _dashing = true;
        while(_dashing) {
            Gravity(0f);
            _rb.velocity = (_facingRight) ? Vector2.right * dashForce : Vector2.left * dashForce;
            yield return new WaitForSeconds(dashTime);
            _dashing = false;
            Gravity(1f);
        }
    }

    // If the player is aiming at a diggable tile, replace it with a dug tile.
    public void Dig(Vector3Int cellAim, TileBase tileAim) {
        if (tileAim is GroundTile && (tileAim as GroundTile).Dug == true) return;
        if (tileAim is GroundTile && (tileAim as GroundTile).Dug == false) {
            TilemapManager.current.tilemap.SetTile(cellAim, (tileAim as GroundTile).dugVersion);
        }
    }

    // Makes the player immune to any damage.
    void Immune(float activeTime) {
        if(!shield.activeSelf) _shieldBubble = StartCoroutine(spawnShield(activeTime));
        else StopCoroutine(_shieldBubble);
    }
    public IEnumerator spawnShield(float activeTime) {
        shield.SetActive(true);
        yield return new WaitForSeconds (activeTime);
        shield.SetActive(false);
    }

    // Lets the player float upwards. If the player moves, the float is cancelled.
    void Float(float floatSpeed, float floatTime) {
        if(!_floating) _floatCoroutine = StartCoroutine(Floating(floatSpeed, floatTime));
        else StopCoroutine(_floatCoroutine);
    }
    private IEnumerator Floating(float floatSpeed, float floatTime) {
        _floating = true;
        while(_floating) {
            Gravity(floatSpeed);
            yield return new WaitForSeconds(floatTime);
            _floating = false;
            Gravity(1f);
        }
    }

    //--------------------------------------------------------------
    // Passive Ability Effects
    //--------------------------------------------------------------
    public void setPassive(ElementEffect effect) {
        Debug.Log(effect);
        if (effect == null) return;
        _lavaResistant.Value = effect.resistance;
        _rockStrength.Value = effect.strength;
        _swimming.Value = effect.swim;
        _wallJump.Value = effect.wallJump;
    }

    // Sets an ability as not available for a specified time.
    public IEnumerator abilityCoolingdown(ElementCooldown cooldowns, float cooldownTime, int index) {
        cooldowns.abilityAvailable[index].Value = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldowns.abilityAvailable[index].Value = true;
    }
}
