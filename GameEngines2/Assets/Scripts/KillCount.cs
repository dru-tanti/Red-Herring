using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class KillCount : MonoBehaviour
{
    [SerializeField]
    public BoolVariable hit_player;

    [SerializeField]
    public IntVariable death_toll;

    [SerializeField]
    public IntVariable multiplier;

    public void KillingSpreeEnded(bool is_hit){
        if(is_hit){
            death_toll.Value = 0;
            multiplier.Value = 0;
        }
    }

    public void KillingSpree(){
        if(hit_player.Value) hit_player.Value = false;

        if(multiplier.Value <= 10){
            if(death_toll.Value == 3){
                multiplier.Value = 2;
            }

            if(death_toll.Value > 3 && (death_toll.Value%2 == 1)){
                multiplier.Value += 1;
            }
        }
    }
}
