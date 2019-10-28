using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Variables")]
    public int speed = 10;
    private bool facingRight = false;
    private SpriteRenderer _spriteRenderer;
    public int jump = 10;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Jump Variables")]
    private bool grounded = false;
    private bool doubleJump = false;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private float moveX;

    private Rigidbody2D _playerRB;

    // Retrieves the players rigidbody so that we can move it.
    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Jump();
        if(_playerRB.velocity.y < 0) {
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if(_playerRB.velocity.y > 0 && !Input.GetButton("Jump")) {
            _playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {
        // Will check if the character is touching the ground
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        Move();
    }

    void Jump()
    {
        // Checks if the player is already in the air before executing the jump command.
        if (Input.GetButtonDown("Jump") && grounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            // Applies force when the player presses the Jump Button.
            _playerRB.velocity = Vector2.up * jump;   
        }

        // If the player holds down the spacebar the character will jump higher
        if(Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0) {
                // Applies force when the player presses the Jump Button.
                _playerRB.velocity = Vector2.up * jump;   
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        // When the space key is released, disable the jump.
        isJumping = Input.GetButtonUp("Jump");
    }
    
    // Controls the movement of the player.
    void Move()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        // Inverts the player model if they are moving to the left.
        if (moveX < 0f && facingRight == false)
        {
            facingRight = !facingRight;
            _spriteRenderer.flipX = facingRight;
        } else if (moveX > 0f && facingRight == true) 
        {
            facingRight = !facingRight;
            _spriteRenderer.flipX = facingRight;
        }

        // Moves the players rigidbody.
        _playerRB.velocity = new Vector2 (moveX * speed, _playerRB.velocity.y);
    }
}
