using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [Header("Movement Variables")]
    public FloatConstant speed;
    private bool _facingRight;
    [HideInInspector] public Rigidbody2D _playerRB;
    private TerrainControl terrain;
    public FloatConstant jump;
    [Range(0f, 5f)]
    public float fallMultiplier = 2.5f;
    [Range(0f, 5f)]
    public float lowJumpMultiplier = 2f;

    [Header("Jump Variables")]
    private bool _grounded = false;
    // private bool _doubleJump = false;
    private float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float _jumpTimeCounter;
    [Range(0f, 0.5f)]
    public float jumpTime;
    private bool _isJumping;
    private float _moveX;


    [Header("Element")]
	public IntVariable selectedElement;
    public ElementType[] element;

    // Retrieves the players rigidbody and sprite renderer so that we can manipulate them through the script.
    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody2D>();
        terrain = GetComponent<TerrainControl>();
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate() 
    {

        // Every frame we will check which element was chosen
        // and use the effects defined in ElementEffect
        if(Input.GetKeyDown(KeyCode.C) && element[selectedElement.Value] != null)
        {
            foreach(ElementEffect otherEffects in element[selectedElement.Value].otherEffects)
            {
                UseEffect(otherEffects);
            }
        }

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

    void Dash(float dashForce)
    {
        Debug.Log("Gotta Go Fast!");
        // _playerRB.AddForce(Vector2.right * dashForce);
    }

    void Immune()
    {
        // TODO: Code to ignore any projectiles that hit the player.
    }

    void HighJump()
    {
        // TODO: Code that makes the player jump higher.
    }

    private void UseEffect(ElementEffect effect)
    {
        if (effect == null) return;

        if (effect.willDash)
        {
            Dash(effect.dashForce);
            //Mathf.Sign(offset.x), effect.pushForce
        }

        if (effect.immune)
        {
            Immune();
        }

        if (effect.weightLess)
        {
            HighJump();
        }
    }
}
