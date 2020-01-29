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
    private float pushForce = 4f;
    public float pushTime = 2f;
    private float pushMultiplier = 1f;
    private bool _knockback;

    [Header("Jump Variables")]
    private bool _grounded = false;
    // private bool _doubleJump = false;
    public float groundRadius = 0.2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    private float _jumpTimeCounter;
    public float jumpTime;
    private bool _isJumping;
    private float _moveX;

    // Retrieves the players rigidbody and sprite renderer so that we can manipulate them through the script.
    private void Awake() 
    {
        _playerRB = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(!_knockback) Jump();
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
        if(!_knockback) Move();
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

   
    // Applies a force in the direction the player is facing.
    public void Push(float direction, float pushForce) {
        if(_knockback == true) return;
        StartCoroutine(Pushing(pushForce, pushTime, direction));
    }
    // Applies the dash force and stops the player from moving while dash is active.
    private IEnumerator Pushing(float pushForce, float pushTime, float direction) {
        _knockback = true;
        while(_knockback) {
            _playerRB.AddForce(Vector2.left * direction  * pushForce * pushMultiplier, ForceMode2D.Impulse);
            yield return new WaitForSeconds(pushTime);
            _knockback= false;
        }
    }

}
