using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Controls the projectile and its effects when it spawns.
//--------------------------------------------------------------------------------------------------------------------------

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    // Controls the speed of the projectile.
    public float speed = 10f;

    // The lifetime of the projectile.
    public float lifetime = 2f;

    // A reference to the laser's rigidbody.
    private Rigidbody2D _projectileRB;
    private Collider2D _collider;
    public ElementType selectedElement;

    private void Awake()
    {
        _projectileRB = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        // Fire the projectile horizontally in relation to the spawn point.
        _projectileRB.velocity = transform.TransformDirection(Vector2.right) * speed;
        // Destroy this object after a specific amount of time.
        Destroy(gameObject, lifetime);
    }

    // When an enemy is hit, trigger the selected properties of the element.
    private void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.tag == "Enemy")
        {
            // Retrieve the base script that is inheritied by every enemy.
            AIBehaviour ai = other.gameObject.GetComponent<AIBehaviour>();

            // Stop and disable the colldier of the projectile.
            _collider.enabled = false;
            _projectileRB.velocity = Vector2.zero;

            if (selectedElement != null)
            {
                foreach (ElementEffect attackEffect in selectedElement.attackEffects)
                {
                    UseEffect(ai, attackEffect);
                }
            } else
            {
                Debug.LogWarning("No element assigned to these particles.");
            }

            enabled = false;
        }    
    }

    // @param ai The AI agent that the projectile has hit.
    // @param effect The effect of the selected element of the projectile. 
    // Defines what properties will trigger which functions in the AIBehaviour script.
    private void UseEffect(AIBehaviour ai, ElementEffect effect)
    {
        if (effect == null) return;

        if (effect.willDamage)
        {
            ai.Damage(effect.damage);
        }

        if (effect.willPush)
        {
            Vector3 offset = transform.position - ai.transform.position;
            ai.Push(Mathf.Sign(offset.x), effect.pushForce);
        }

        if (effect.willFreeze)
        {
            ai.Freeze(effect.freezeDuration);
        }

        if (effect.willStun)
        {
            ai.Stun(effect.stunDuration);
        }
    }
}
