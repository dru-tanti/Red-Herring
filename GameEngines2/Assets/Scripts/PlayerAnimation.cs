using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

//--------------------------------------------------------------------------------------------------------------------------
// Manages the players animation variables to avoid clutter in the PlayerControl script.
//--------------------------------------------------------------------------------------------------------------------------

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private PlayerControl _player;
    private Rigidbody2D _playerRB;
    public IntVariable selectedElement;
    public RuntimeAnimatorController[] anims;
    private bool _falling = false;
    private void Awake() {
        _anim = GetComponent<Animator>();
        _player = GetComponent<PlayerControl>();
        _playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(_player.moveX != 0f){
            _anim.SetBool("IsRunning", true);
        } else {
            _anim.SetBool("IsRunning", false);
        }

        if(Input.GetButtonDown("Jump") && _player._grounded) {
            _anim.SetBool("IsJumping", true);
        }

        // Changes the animation controller when the player changes elements
        // ! Button needs to be double clicked for some reason.
        // if(Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.P)){ 
        // }

        if(!_player._grounded && _playerRB.velocity.y < 0f) {
            _anim.SetBool("IsJumping", false);
            _anim.SetBool("IsFalling", true);
            _falling = true;
        }

        if(_player._grounded && _falling) {
            _anim.SetBool("IsFalling", false);
            _falling = false;
        }

        // Only sets the animation boolean to true if the ability is available to use.
        if(_player.cooldowns[selectedElement.Value].abilityAvailable[0]) {
            if(Input.GetKeyDown(KeyCode.V)) {
                _anim.SetBool("IsAttacking", true);
            } else {
                _anim.SetBool("IsAttacking", false);
            }
        }
    }

    public void changeElement(int selectedElement) {
        _anim.runtimeAnimatorController = anims[selectedElement];
    }
}
