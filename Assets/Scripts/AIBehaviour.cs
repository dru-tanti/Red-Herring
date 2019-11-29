using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class AIBehaviour : MonoBehaviour
{
    public int enemyHealth = 10;
    public int speed = 5;
    public float pushForce = 4;
    protected Rigidbody2D _enemyRB;

    // Start is called before the first frame update
    private void Awake() 
    {
        _enemyRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log(enemyHealth);
    }

    public void Stun(int stunDuration)
    {
        StartCoroutine(StunEnemy(stunDuration));
    }

    public void Freeze(int freezeDuration)
    {
        StartCoroutine(FreezeEnemy(freezeDuration));
    }

    public void Push(float direction, float pushMultiplier)
    {   
        _enemyRB.AddForce(Vector2.left * direction  * pushForce * pushMultiplier, ForceMode2D.Impulse);
    }

    // Stunned enemies can still damage you if you touch them.
    private IEnumerator StunEnemy(int stunDuration)
    {
        speed = 0;
        yield return new WaitForSeconds(stunDuration);
        speed = 5;
    }


    // Freeze makes the enemies harmless.
    private IEnumerator FreezeEnemy(int freezeDuration)
    {
        // speed = 0;
        Debug.Log("Frozen");
        yield return new WaitForSeconds(freezeDuration);
        Debug.Log("Thawed");
        // speed = 5;
    }
}
