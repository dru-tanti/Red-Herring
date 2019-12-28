using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using TMPro;

public class UICoinMeter : MonoBehaviour
{
    [SerializeField]
    public IntVariable coins;

    [SerializeField]
    public TextMeshProUGUI wallet;

    [SerializeField]
    public TextMeshProUGUI add_coins;

    private const float _fade = 2.0f;
    private Color _fade_colour;
    private bool _update = false;

    void Awake(){
        wallet.SetText("0");
        add_coins.SetText("");

        _fade_colour = add_coins.color;
    }

    void FixedUpdate(){

        //Fade out coin increase
        if(_update){
            Color fade_out = add_coins.color;

            if(fade_out.a <= 0f){
                _update = false;
                add_coins.SetText("");
                add_coins.color = Color.white;
            }else{      
                fade_out.a -= Time.deltaTime/_fade;
                add_coins.color = fade_out;
            }
        }
    }
    
    public void WalletUpdated(int value)
    {   
        add_coins.SetText("+" + (value - coins.OldValue));
        _update = true;
        wallet.SetText(coins.Value.ToString());
    }
}