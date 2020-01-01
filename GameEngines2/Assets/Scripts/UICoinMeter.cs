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
    public IntVariable multiplier;


    [SerializeField]
    public TextMeshProUGUI wallet;

    [SerializeField]
    public TextMeshProUGUI add_coins;

    [SerializeField]
    public TextMeshProUGUI multiply_text;

    private const float _fade = 2.0f;
    private bool _update = false;

    void Awake(){
        wallet.SetText("0");
        add_coins.SetText("");
        multiply_text.SetText("");
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

    public void MultiplierFlavour(){
        if(multiplier.Value == 0){ 
            multiply_text.SetText("");
        }else{
            multiply_text.SetText("x" + multiplier.Value);
        }
    }

    //multiplier text is affected by enemy death and player being hit
    public void WalletUpdated(int value)
    {   
        int excess = 1;

        if(multiplier.Value > 0) excess = multiplier.Value;

        add_coins.SetText("+" + ((value - coins.OldValue) * excess));
        _update = true;
        wallet.SetText(coins.Value.ToString());
    }
}