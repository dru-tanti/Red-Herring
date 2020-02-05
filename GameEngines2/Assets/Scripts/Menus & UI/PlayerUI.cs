using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    public IntVariable selectedElement;

    public Image[] all_elements;

    [Header("Element Abilities")]
    public Sprite[] air_abilities;
    public Sprite[] earth_abilities;
    public Sprite[] fire_abilities;
    public Sprite[] water_abilities;

    [Header("Variables")]
    public BoolVariable[] unlocked;
    public BoolVariable[] air_unlocked;
    public BoolVariable[] earth_unlocked;

    public BoolVariable[] fire_unlocked;

    public BoolVariable[] water_unlocked;


    [Header("Icons")]
    public Image passive;
    public Image ability_1;
    public Image ability_2;

    public Sprite[] artefacts;


    private Color _icon_alpha;

    private string _se_name;
    private string _sprite_ref;

    //hold index values
    private int _hold = 100;
    private int _sprite_dex;


    void FixedUpdate(){
         //If the selected element's index value has changed, commit the change in the UI
        if(_hold != selectedElement.Value){
            //Changing alpha back to unselected value
            if(_hold < 4) ChangeAlpha(_hold, 0.6f);
            
            if(unlocked[selectedElement.Value]){
                //Making sure the selected element's index is the same as the sprite index
                _se_name = PlayerAttack.element_list[selectedElement.Value].name;
                _se_name = _se_name.Substring(0, _se_name.Length - 9);

                _sprite_ref = all_elements[selectedElement.Value].name;

                //Name comparison, so PLEASE make sure that the elements HAVE THE SAME NAME THROUGHOUT
                if(_se_name.ToLower() == _sprite_ref.ToLower()){
                    ChangeAlpha(selectedElement.Value, 1f);
                    SwitchAbilities(selectedElement.Value);
                }else{
                    _sprite_dex = System.Array.FindIndex(all_elements, e => e.name == _se_name.ToLower());
                    ChangeAlpha(_sprite_dex, 1f);
                    SwitchAbilities(_sprite_dex);
                }

                _hold = selectedElement.Value;
            }
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.X)) {
            switch(selectedElement.Value){
                case 0: 
                    if(air_unlocked[0])ShowAttack(1f);
                break;
                case 1: 
                    if(earth_unlocked[0]) ShowAttack(1f);
                break;
                case 2: 
                    if(fire_unlocked[0]) ShowAttack(1f);
                break;
                case 3: 
                    if(water_unlocked[0]) ShowAttack(1f);
                break;
            }
        }

        if(Input.GetKeyDown(KeyCode.C)) {
            switch(selectedElement.Value){
                case 0: 
                    if(air_unlocked[1])ShowOther(1f);
                break;
                case 1: 
                    if(earth_unlocked[1]) ShowOther(1f);
                break;
                case 2: 
                    if(fire_unlocked[1]) ShowOther(1f);
                break;
                case 3: 
                    if(water_unlocked[1]) ShowOther(1f);
                break;
            }
        }

    }

    void ChangeAlpha(int element, float al){
        _icon_alpha = all_elements[element].color;
        _icon_alpha.a = al;
        all_elements[element].color = _icon_alpha;
    }

    void ShowAttack(float al){
        _icon_alpha = ability_1.color;
        _icon_alpha.a = al;
        ability_1.color = _icon_alpha;
    }

    void ShowOther(float al){
        _icon_alpha = ability_2.color;
        _icon_alpha.a = al;
        ability_2.color = _icon_alpha;
    }

    void SwitchAbilities(int element){
        switch(element){
            case 0: 
                passive.sprite = air_abilities[0];
                ability_1.sprite = air_abilities[1];
                ability_2.sprite = air_abilities[2];

                if(air_unlocked[0]){
                    ShowAttack(0.6f);
                }else{
                    ShowAttack(0.1f);
                }

                if(air_unlocked[1]){
                    ShowOther(0.6f);
                }else{
                    ShowOther(0.1f);
                }
            break;
            case 1:
                passive.sprite = earth_abilities[0];
                ability_1.sprite = earth_abilities[1];
                ability_2.sprite = earth_abilities[2];

                if(earth_unlocked[0]){
                    ShowAttack(0.6f);
                }else{
                    ShowAttack(0.1f);
                }

                if(earth_unlocked[1]){
                    ShowOther(0.6f);
                }else{
                    ShowOther(0.1f);
                }
            break;
            case 2: 
                passive.sprite = fire_abilities[0];
                ability_1.sprite = fire_abilities[1];
                ability_2.sprite = fire_abilities[2];
                
                if(fire_unlocked[0]){
                    ShowAttack(0.5f);
                }else{
                    ShowAttack(0.15f);
                }

                if(fire_unlocked[1]){
                    ShowOther(0.5f);
                }else{
                    ShowOther(0.15f);
                }
            break;
            case 3: 
                passive.sprite = water_abilities[0];
                ability_1.sprite = water_abilities[1];
                ability_2.sprite = water_abilities[2];

                if(water_unlocked[0]){
                    ShowAttack(0.5f);
                }else{
                    ShowAttack(0.15f);
                }

                if(water_unlocked[1]){
                    ShowOther(0.5f);
                }else{
                    ShowOther(0.15f);
                }
            break;
        }
    }

    public void UnlockAir(){
        if(unlocked[0].Value) ChangeAlpha(0, 0.5f);
    }
    
    public void UnlockAirAttack(){
        if(air_unlocked[0].Value) ChangeAlpha(0, 0.5f);
    }

    public void UnlockAirOther(){
        if(air_unlocked[1].Value) ChangeAlpha(0, 0.5f);
    }

    public void UnlockEarth(){
        if(unlocked[1].Value) ChangeAlpha(1, 0.5f);
    }
    public void UnlockEarthAttack(){
        if(earth_unlocked[0].Value) ChangeAlpha(0, 0.5f);
    }

    public void UnlockEarthOther(){
        if(earth_unlocked[1].Value) ChangeAlpha(0, 0.5f);
    }

    public void UnlockFire(){
        if(unlocked[2].Value) ChangeAlpha(2, 0.5f);
    }
    public void UnlockFireAttack(){
        if(fire_unlocked[0].Value) ChangeAlpha(0, 0.5f);
    }

    public void UnlockFireOther(){
        if(fire_unlocked[1].Value) ChangeAlpha(0, 0.5f);
    }

    public void UnlockWater(){
        if(unlocked[3].Value) ChangeAlpha(3, 0.5f);
    }
    public void UnlockWaterAttack(){
        if(water_unlocked[0].Value) ChangeAlpha(0, 0.5f);
    }

    public void UnlockWaterOther(){
        if(water_unlocked[1].Value) ChangeAlpha(0, 0.5f);
    }


    public void AirArtefact(){
        all_elements[0].sprite = artefacts[0];
    }
    public void EarthArtefact(){
        all_elements[1].sprite = artefacts[1];
    }
    public void FireArtefact(){
        all_elements[2].sprite = artefacts[2];
    }
    public void WaterArtefact(){
        all_elements[3].sprite = artefacts[3];
    }
}
