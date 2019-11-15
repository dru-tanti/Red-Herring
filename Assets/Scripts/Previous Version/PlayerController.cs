using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    // TODO: Add scriptable objects to define what each element does. Then create a script for all the possible effects that an element can have.
    [Header("Movement Variables")]
    public FloatConstant speed;
    private bool _facingRight;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _playerRB;
    public int jump = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Jump Variables")]
    private bool _grounded = false;
    private bool _doubleJump = false;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float _jumpTimeCounter;
    public float jumpTime;
    private bool _isJumping;
    private float _moveX;

    // Custom class that will contain the colour and the sprites we will use for the radial mennu
    [System.Serializable]
    public class Action {
        public Color colour;
        public Sprite sprite;
        public int index;
    }

    [Header("Radial Menu Variables")]
    // Passes the information set in the Action class to the Radial menu.
    public Action[] elements;


    // Retrieves the players rigidbody and sprite renderer so that we can manipulate them through the script.
    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Jump();
        // When the Right Mouse button is clicked, it will spawn a menu.
        if(Input.GetMouseButtonDown(1))
        {
            RadialMenuSpawner.ins.SpawnMenu(this);
        }
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
            _playerRB.velocity = Vector2.up * jump;   
        }

        // If the player holds down the spacebar the character will jump higher
        if(Input.GetButton("Jump") && _isJumping == true)
        {
            if(_jumpTimeCounter > 0) {
                // Applies force when the player presses the Jump Button.
                _playerRB.velocity = Vector2.up * jump;   
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

        // Inverts the player model through the sprite renderer if they are moving to the left.
        if (_moveX < 0f && _facingRight == false)
        {
            _facingRight = !_facingRight;
        } else if (_moveX > 0f && _facingRight == true) 
        {
            _facingRight = !_facingRight;
        }
        _spriteRenderer.flipX = _facingRight;

        // Moves the players rigidbody.
        _playerRB.velocity = new Vector2 (_moveX * speed.Value, _playerRB.velocity.y);
    }
}
