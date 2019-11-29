using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

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

    private void OnCollisionEnter2D(Collision2D other) 
    {

        if(other.gameObject.tag == "Enemy")
        {
            AIBehaviour ai = other.gameObject.GetComponent<AIBehaviour>();
            _collider.enabled = false;
            _projectileRB.velocity = Vector2.zero;

            if (selectedElement != null)
            {
                foreach (ElementEffect effect in selectedElement.effects)
                {
                    UseEffect(ai, effect);
                }
            }
            else
            {
                Debug.LogWarning("No element assigned to these particles.");
            }
            enabled = false;
            
        }    
    }

    private void UseEffect(AIBehaviour ai, ElementEffect effect)
    {
        if (effect == null) return;

        if (effect.willDamage)
        {
            ai.Damage(effect.damage);
        }

        if (effect.willPush)
        {
            ai.Push();
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
