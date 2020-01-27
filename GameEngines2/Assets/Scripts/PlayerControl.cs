using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    // TODO: Add scriptable objects to define what each element does. Then create a script for all the possible effects that an element can have.
    [Header("Movement Variables")]
    public FloatConstant speed;
    private bool _facingRight;
    public Rigidbody2D _playerRB;
    public FloatConstant jump;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Jump Variables")]
    private bool _grounded = false;
    // private bool _doubleJump = false;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float _jumpTimeCounter;
    public float jumpTime;
    private bool _isJumping;
<<<<<<< Updated upstream
    private float _moveX;

=======
    private float _hardLandingTimer; // Used to trigger the hard landing animation.
    private bool _floating;

    [Header("Element")]
	public IntVariable selectedElement;
    public ElementType[] element;
    public ElementCooldown[] cooldowns;

    [SerializeField] private GameObject shield = null;
    private Coroutine _highJumpCoroutine;
    private Coroutine _shieldBubble;
>>>>>>> Stashed changes
    // Retrieves the players rigidbody and sprite renderer so that we can manipulate them through the script.
    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Jump();
    }

    private void FixedUpdate() 
    {
        // Will check if the character is touching the ground
        _grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // If the player is falling, we will increase the gravity scale so that the player falls faster.
        if(_playerRB.velocity.y < 0) {
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(_playerRB.velocity.y > 0 && !Input.GetButton("Jump")) {
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        Move();
    }

    void Jump()
    {
        // Checks if the player is already in the air before executing the jump command.
        if (Input.GetButtonDown("Jump") && _grounded)
        {
            _isJumping = true;
            _jumpTimeCounter = jumpTime;

            // Applies force when the player presses the Jump Button.
            _playerRB.velocity = Vector2.up * jump.Value;   
        }

        // If the player holds down the spacebar the character will jump higher
        if(Input.GetButton("Jump") && _isJumping == true)
        {
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
    void Move()
    {
        _moveX = Input.GetAxisRaw("Horizontal");

        // Inverts the player model if they are moving to the left.
        if (_moveX < 0f && _facingRight == false)
        {
            FlipPlayer();
        } else if (_moveX > 0f && _facingRight == true) 
        {
            FlipPlayer();
        }
        

        // Moves the players rigidbody.
        _playerRB.velocity = new Vector2 (_moveX * speed.Value, _playerRB.velocity.y);
    }

    // Rotates the gameObject to flip the player to make it look as if they are moving left and right.
    void FlipPlayer()
    {
        _facingRight = !_facingRight;
        
        transform.Rotate(0f, 180f, 0f);
    }
<<<<<<< Updated upstream
=======

    // Triggers different methods depending on the effects active.
    private void UseEffect(ElementEffect effect) {
        if (effect == null) return;

        if (effect.willDash && !_dashing) {
            Dash(effect.dashForce, effect.activeTime);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));
        }

        if (effect.immune) {
            Immune(effect.activeTime);
            StartCoroutine(this.abilityCoolingdown(this.cooldowns[selectedElement.Value], effect.cooldown, 1));

        }

        if (effect.willFloat) {
            HighJump(effect.floatSpeed, effect.activeTime);
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
        while(_dashing) {
            Gravity(0f);
            _rb.velocity = (_facingRight) ? Vector2.right * dashForce : Vector2.left * dashForce;
            yield return new WaitForSeconds(dashTime);
            _dashing = false;
            Gravity(1f);
        }
    }

    void Immune(float activeTime) {
        if(!shield.activeSelf) _shieldBubble = StartCoroutine(spawnShield(activeTime));
        else StopCoroutine(_shieldBubble);
    }

    // Lets the player float upwards. If the player moves, the float is cancelled.
    void HighJump(float floatSpeed, float floatTime) {
        if(!_floating) _highJumpCoroutine = StartCoroutine(Floating(floatSpeed, floatTime));
        else StopCoroutine(_highJumpCoroutine);
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

    // Sets an ability as not available for a specified time.
    public IEnumerator abilityCoolingdown(ElementCooldown cooldowns, float cooldownTime, int index) {
        cooldowns.abilityAvailable[index].Value = false;
        yield return new WaitForSeconds(cooldownTime);
        cooldowns.abilityAvailable[index].Value = true;
    }

    public IEnumerator spawnShield(float activeTime) {
        shield.SetActive(true);
        yield return new WaitForSeconds (activeTime);
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
>>>>>>> Stashed changes
}
