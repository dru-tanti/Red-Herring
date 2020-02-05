// @author: Andrew Tanti

using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

// Custom class to hold the cooldowns
[System.Serializable]
public class ElementCooldown {
    public BoolVariable[] abilityAvailable;
}

//--------------------------------------------------------------------------------------------------------------------------
// Contains the main movement controls for the player
//--------------------------------------------------------------------------------------------------------------------------

public partial class PlayerControl : BaseController {

    [Tooltip("Unlockes all the elements and abilites if true. For Testing Purposes")]
    public bool elementsUnlocked = false;

    [Header("Movement Variables")]
    public FloatConstant speed;
    private bool _facingRight = true;
    private PlayerEnvironment terrain;
    public FloatConstant jump;
    public BoolVariable _isAlive;
    private float _moveX;
    public float moveX { get => _moveX; } // To be used by the PlayerAnimation script
    public bool _knockback;

    [Header("Jump Variables")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    [Range(0f, 0.5f)]
    public float jumpTime;
    [Range(0f, 5f)]
    public float fallMultiplier = 2.5f;
    [Range(0f, 5f)]
    public float lowJumpMultiplier = 2f;

    // Used for wall jumping
    public float wallJumpForce;
    public Vector2 wallJumpDirection;

    [Tooltip("Effect of enemies when they push the player.")]
    public float pushTime;
    public float pushMultiplier;

    [HideInInspector] public bool _grounded = false;
    private float groundRadius = 0.2f;
    private float _jumpTimeCounter;
    private bool _isJumping, _canWallJump;
    private float _hardLandingTimer; // Used to trigger the hard landing animation.

    protected override void Awake() {
        base.Awake();
        terrain = GetComponent<PlayerEnvironment>();
        _isAlive.Value = true;
        wallJumpDirection.Normalize();

        // NOTE: FOR TESTING PURPOSES.
        if(elementsUnlocked == true){
            unlockElements();
        } else {
            resetElements();
        }
    }

    void Update() {
        if(!_knockback) Jump();

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

        // NOTE: For testing Purposes Only (No shit tipo...)
        // Kills the player
        if(Input.GetKeyDown(KeyCode.E)) _isAlive.Value = false;

        if(_isAlive.Value == false) {
            PlayerSpawner.current.spawnPlayer();
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate() {
        // Will check if the character is touching the ground
        _grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(_grounded) _canWallJump = true;

        // If the player is falling, we will increase the gravity scale so that the player falls faster.
        if(_rb.velocity.y < 0) {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(_rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            // If the character is rising, but the player is not holding the jump button, apply increased gravity.
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(!_dashing && !_floating && !_knockback) Move();
    }

    void Jump() {
        // Checks if the player is already in the air before executing the jump command.
        if (Input.GetButtonDown("Jump") && (_grounded)) {
            _isJumping = true;
            _jumpTimeCounter = jumpTime;

            _rb.velocity = Vector2.up * jump.Value;
        }

        if(terrain.isTouchingWall && !_grounded && _wallJump.Value && _rb.velocity.y <= 0) {
            if(Input.GetButtonDown("Jump") && _canWallJump) {
                _canWallJump = false;
                StartCoroutine(pushOffWall());
                _jumpTimeCounter = jumpTime;
                _rb.velocity = Vector2.up * jump.Value;
            }
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
        if(Input.GetButtonUp("Jump")) _isJumping = false;
    }

    private IEnumerator pushOffWall() {
        bool walljump = true;
        while(walljump) {
            Gravity(0f);
            _rb.velocity = (_facingRight) ? Vector2.left * speed.Value : Vector2.right * speed.Value;
            yield return new WaitForSeconds(0.2f);
            _canWallJump = false;
            walljump = false;
            Gravity(1f);
        }
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


    // Applies a force in the direction the player is facing.
    public void Push(float direction, float pushForce) {
        if(_knockback == true) return;
        StartCoroutine(Pushing(pushForce, pushTime, direction));
    }
    // Applies the dash force and stops the player from moving while dash is active.
    private IEnumerator Pushing(float pushForce, float pushTime, float direction) {
        _knockback = true;
        while(_knockback) {
            _rb.AddForce(Vector2.left * direction  * pushForce * pushMultiplier, ForceMode2D.Impulse);
            yield return new WaitForSeconds(pushTime);
            _knockback= false;
        }
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

    // Unlocks all the elements for testin purposes.
    public void unlockElements() {
        element[0].unlocked.Value = true;
        element[1].unlocked.Value = true;
        element[3].unlocked.Value = true;
        for(int x = 0; x < cooldowns.Length; x++) {
            for(int y = 0; y < cooldowns[x].abilityAvailable.Length; y++) {
                cooldowns[x].abilityAvailable[y].Value = true;
            }
        }
        cooldowns[2].abilityAvailable[0].Value = true;
    }

    public void killPlayer() {
        // if the player is not currently shielded, kill the player.
        if(!shield.activeSelf) {
            _isAlive.Value = false;
            Destroy(gameObject);
        }
    }
}
