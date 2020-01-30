using System.Collections;
using System.Collections.Generic;
using UnityAtoms; 
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public IntVariable health;
    public IntVariable max_health;

    void Start()
    {
        InvokeRepeating("HealthRegen", 1.0f, 1.0f);
    }
 
    private void HealthRegen()
    {
        if(health.Value < max_health.Value){
            health.Value += 1;
        }
    }
}
