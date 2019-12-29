using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [Header("Movement Variables")]
    public FloatConstant speed;
    private bool _facingRight = true;
    [HideInInspector] public Rigidbody2D _playerRB;
    private TerrainControl terrain;
    public FloatConstant jump;
    [Range(0f, 5f)]
    public float fallMultiplier = 2.5f;
    [Range(0f, 5f)]
    public float lowJumpMultiplier = 2f;
    private float _moveX;
    [Range(0f, 1f)]
    [Tooltip("Time dashForce will be active for")]
    public float dashTime = 0f;
    private bool _dashing = false;

    [Header("Jump Variables")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    [Range(0f, 0.5f)]
    public float jumpTime;
    private bool _grounded = false;
    private float groundRadius = 0.2f;
    private float _jumpTimeCounter;
    private bool _isJumping;
    private float _hardLandingTimer;
    private float _gravityScale;

    [Header("Element")]
	public IntVariable selectedElement;
    public ElementType[] element;

    // Retrieves the players rigidbody and sprite renderer so that we can manipulate them through the script.
    private void Awake() {
        _playerRB = GetComponent<Rigidbody2D>();
        terrain = GetComponent<TerrainControl>();
        _gravityScale = _playerRB.gravityScale;
    }

    void Update() {
        Jump();
        // Every frame we will check which element was chosen and use the effects defined in ElementEffect
        if(Input.GetKeyDown(KeyCode.C) && element[selectedElement.Value] != null) {
            foreach(ElementEffect otherEffects in element[selectedElement.Value].otherEffects) {
                UseEffect(otherEffects);
            }
        }
    }

    private void FixedUpdate() {
        // Will check if the character is touching the ground
        _grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // If the player is falling, we will increase the gravity scale so that the player falls faster.
        if(_playerRB.velocity.y < 0) {
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(_playerRB.velocity.y > 0 && !Input.GetButton("Jump")) {
            // If the character is rising, but the player is not holding the jump button, apply increased gravity.
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(!_dashing){ Move(); }
    }

    void Jump() {
        // Checks if the player is already in the air before executing the jump command.
        if (Input.GetButtonDown("Jump") && _grounded) {
            _isJumping = true;
            _jumpTimeCounter = jumpTime;

            _playerRB.velocity = Vector2.up * jump.Value;   
        }

        // If the player holds down the spacebar the character will jump higher
        if(Input.GetButton("Jump") && _isJumping == true) {
            if(_jumpTimeCounter > 0) {
                // Applies force when the player presses the Jump Button.
                _playerRB.velocity = Vector2.up * jump.Value;   
                _jumpTimeCounter -= Time.deltaTime;
            } else {
                _isJumping = false;
            }
        }

        // When the space key is released, disable the jump.
        if(Input.GetButtonUp("Jump"))
        {
            _isJumping = false;
        }
    }
    
    // Controls the movement of the player.
    void Move() {
        _moveX = Input.GetAxisRaw("Horizontal");
        // Inverts the player model if they are moving to the left.
        if (_moveX < 0f && _facingRight == true) {
            FlipPlayer();
        } else if (_moveX > 0f && _facingRight == false) {
            FlipPlayer();
        }
        // Moves the players rigidbody.
        _playerRB.velocity = new Vector2 (_moveX * speed.Value, _playerRB.velocity.y);
    }

    // Rotates the gameObject to flip the player to make it look as if they are moving left and right.
    void FlipPlayer() {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Applies a force in the direction the player is facing.
    void Dash(float dashForce) {
        if(_dashing == true){ return; }
        StartCoroutine(Dashing(dashForce));
    }

    void Immune() {
        // TODO: Code to ignore any projectiles that hit the player.
    }

    void HighJump() {
        // TODO: Code that makes the player jump higher.
    }

    private void UseEffect(ElementEffect effect) {
        if (effect == null) return;

        if (effect.willDash && !_dashing) {
            Dash(effect.dashForce);
        }

        if (effect.immune) {
            Immune();
        }

        if (effect.weightLess) {
            HighJump();
        }
    }

    // Applies the dash force and stops the player from moving while dash is active.
    private IEnumerator Dashing(float dashForce) {
        _dashing = true;
        while(_dashing){
            Gravity(false);
            _playerRB.velocity = (_facingRight) ? Vector2.right * dashForce : Vector2.left * dashForce;
            yield return new WaitForSeconds(dashTime);
            _dashing = false;
            Gravity(true);
        }
    }

    // Used to turn gravity on and off as needed.
    public void Gravity(bool applyGravity) {
        if (this._playerRB.gravityScale > Mathf.Epsilon && !applyGravity) {
            this._playerRB.gravityScale = 0.0f;
        } else {
            if (this._playerRB.gravityScale > Mathf.Epsilon || !applyGravity)
                return;
            this._playerRB.gravityScale = _gravityScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Water")) {
            
        }
    }
}
