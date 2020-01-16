using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

[System.Serializable]
public class ElementCooldown {
    // [System.Serializable]
    // public FloatVariable[] cooldown;
    public BoolVariable[] abilityAvailable;
}

public class PlayerControl : BaseController {
    [Header("Movement Variables")]
    public FloatConstant speed;
    private bool _facingRight = true;
    private TerrainControl terrain;
    public FloatConstant jump;
    public BoolVariable _isInvisible;
    [Range(0f, 5f)]
    public float fallMultiplier = 2.5f;
    [Range(0f, 5f)]
    public float lowJumpMultiplier = 2f;
    private float _moveX;
    public float moveX { get => _moveX; } // To be used by the PlayerAnimation script
    [Range(0f, 1f)]
    private bool _dashing = false;

    [Header("Jump Variables")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    [Range(0f, 0.5f)]
    public float jumpTime;
    [HideInInspector] public bool _grounded = false;
    private float groundRadius = 0.2f;
    private float _jumpTimeCounter;
    private bool _isJumping;
    private float _hardLandingTimer; // Used to trigger the hard landing animation.
    private bool _floating;

    [Header("Element")]
	public IntVariable selectedElement;
    public ElementType[] element;
    public ElementCooldown[] cooldowns;

    // Retrieves the players rigidbody and sprite renderer so that we can manipulate them through the script.
    protected override void Awake() {
        base.Awake();
        terrain = GetComponent<TerrainControl>();
        resetElements();
    }

    void Update() {
        Jump();
        // Every frame we will check which element was chosen and use the effects defined in ElementEffect
        if(Input.GetKeyDown(KeyCode.C) && element[selectedElement.Value] != null) {
            if(cooldowns[selectedElement.Value].abilityAvailable[1].Value) {
                foreach(ElementEffect otherEffects in element[selectedElement.Value].otherEffects) {
                    UseEffect(otherEffects);
                }
            } else {
                Debug.Log("Ability not yet available");
                return;
            }
        }
    }

    private void FixedUpdate() {
        // Will check if the character is touching the ground
        _grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // If the player is falling, we will increase the gravity scale so that the player falls faster.
        if(_rb.velocity.y < 0) {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(_rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            // If the character is rising, but the player is not holding the jump button, apply increased gravity.
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(!_dashing && !_floating){ Move(); }
    }

    void Jump() {
        // Checks if the player is already in the air before executing the jump command.
        if (Input.GetButtonDown("Jump") && _grounded) {
            _isJumping = true;
            _jumpTimeCounter = jumpTime;

            _rb.velocity = Vector2.up * jump.Value;   
        }

        // If the player holds down the spacebar the character will jump higher
        if(Input.GetButton("Jump") && _isJumping == true) {
            if(_jumpTimeCounter > 0) {
                // Applies force when the player presses the Jump Button.
                _rb.velocity = Vector2.up * jump.Value;   
                _jumpTimeCounter -= Time.deltaTime;
            } else {
                _isJumping = false;
            }
        }

        // When the space key is released, disable the jump.
        if(Input.GetButtonUp("Jump")) { _isJumping = false; }
    }
    
    // Controls the movement of the player.
    void Move() {
        _moveX = Input.GetAxisRaw("Horizontal");

        // Inverts the player model if they are moving to the left.
        if (_moveX < 0f && _facingRight) {
            FlipPlayer();
        } else if (_moveX > 0f && !_facingRight) {
            FlipPlayer();
        }

        _rb.velocity = new Vector2 (_moveX * speed.Value, _rb.velocity.y);
    }

    // Rotates the gameObject to flip the player to make it look as if they are moving left and right.
    void FlipPlayer() {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Triggers different methods depending on the effects active.
    private void UseEffect(ElementEffect effect) {
        if (effect == null) return;

        if (effect.willDash && !_dashing) {
            Dash(effect.dashForce, effect.dashTime);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));
        }

        if (effect.immune) {
            Immune();
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));

        }

        if (effect.willFloat) {
            HighJump(effect.floatSpeed, effect.floatTime);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));
        }
    }

    // Applies a force in the direction the player is facing.
    void Dash(float dashForce, float dashTime) {
        if(_dashing == true) return;
        StartCoroutine(Dashing(dashForce, dashTime));
    }

    // Applies the dash force and stops the player from moving while dash is active.
    private IEnumerator Dashing(float dashForce, float dashTime) {
        _dashing = true;
        while(_dashing){
            Gravity(0f);
            _rb.velocity = (_facingRight) ? Vector2.right * dashForce : Vector2.left * dashForce;
            yield return new WaitForSeconds(dashTime);
            _dashing = false;
            Gravity(1f);
        }
    }

    void Immune() {
        // TODO: Code to ignore any projectiles that hit the player.
    }

    // Lets the player float upwards. If the player moves, the float is cancelled.
    void HighJump(float floatSpeed, float floatTime) {
        if(!_floating) StartCoroutine(Floating(floatSpeed, floatTime));
    }

    private IEnumerator Floating(float floatSpeed, float floatTime) {
        _floating = true;
        while(_floating){
            Gravity(floatSpeed);
            yield return new WaitForSeconds(floatTime);
            _floating = false;
            Gravity(1f);
        }
    }

    // Sets an ability as not available for a specified time.
    public IEnumerator abilityCoolingdown(ElementCooldown cooldowns, float cooldownTime, int index) {
        cooldowns.abilityAvailable[index].Value = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldowns.abilityAvailable[index].Value = true;
    }

    // NOTE: Mainly for Testing Purposes
    // Will be used to manually reset the elements at the start of the game
    public void resetElements() {
        element[0].unlocked.Value = false;
        element[1].unlocked.Value = false;
        element[3].unlocked.Value = false;
        for(int x = 0; x < cooldowns.Length; x++) {
            for(int y = 0; y < cooldowns[x].abilityAvailable.Length; y++) {
                cooldowns[x].abilityAvailable[y].Value = false;
            }
        }
        cooldowns[2].abilityAvailable[0].Value = true;
    }
}
