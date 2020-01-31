using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Base script that will be inherited by all enemy scripts. 
// Contains the different effects the enemies can have when hit by an element.
//--------------------------------------------------------------------------------------------------------------------------

public abstract class AIBehaviour : BaseController
{
    public static int enemyMaxHealth = 10;
    public int enemyHealth;
    public float speed = 5;
    public float pushForce = 4;
    public bool harmful = false;

    void Awake(){
        enemyHealth = enemyMaxHealth;
    }

    // Reduced the enemies health by the amount defined by the projectile.
    public void Damage(int damage) {
        enemyHealth -= damage;
        Debug.Log(enemyHealth);

        var test = GetComponent<AITest>();
        test.EnemyIsHit(enemyHealth);
    }

    // Triggers the Stun effect.
    public void Stun(float stunDuration) {
        StartCoroutine(StunEnemy(stunDuration));
    }

    // Triggers the Freeze effect.
    public void Freeze(float freezeDuration) {
        StartCoroutine(FreezeEnemy(freezeDuration));
    }

    // Applies a force in the opposite direction the enemy was hit from.
    public void Push(float direction, float pushMultiplier) {   
        _rb.AddForce(Vector2.left * direction  * pushForce * pushMultiplier, ForceMode2D.Impulse);
    }

    // Stunned enemies can still damage you if you touch them.
    private IEnumerator StunEnemy(float stunDuration) {
        float speedtmp = speed;
        speed = 0f;
        yield return new WaitForSeconds(stunDuration);
        speed = speedtmp;
    }


    // Freeze makes the enemies harmless.
    private IEnumerator FreezeEnemy(float freezeDuration) {
        float speedtmp = speed;
        bool harmfultmp = harmful;
        speed = 0f;
        harmful = false;
        yield return new WaitForSeconds(freezeDuration);
        harmful = harmfultmp;
        speed = speedtmp;
    }
}
