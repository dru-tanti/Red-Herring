using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public IntVariable mana;
    public IntVariable max_mana;
 
    void Start()
    {
        InvokeRepeating("ManaGeneration", 1.0f, 1.0f);
    }
 
    private void ManaGeneration()
    {
        if(mana.Value < max_mana.Value){
            mana.Value += 1;
        }
    }
}
