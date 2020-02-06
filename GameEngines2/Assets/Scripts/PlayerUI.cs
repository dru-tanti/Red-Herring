using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public IntVariable selectedElement;
    public Image[] all_elements;


    [Header("Element Abilities")]
    public Sprite[] air_abilities;
    public Sprite[] earth_abilities;
    public Sprite[] fire_abilities;
    public Sprite[] water_abilities;


    [Header("Variables")]
    public BoolVariable[] unlocked;
    public BoolVariable[] air_available;
    public BoolVariable[] earth_available;

    public BoolVariable[] fire_available;

    public BoolVariable[] water_available;
    public BoolVariable[] artefacts_unlocked;


    [Header("Cooldowns")]
    public CooldownManager cooldown;


    [Header("Icons")]
    public Image passive;
    public Image ability_1;
    public Image ability_2;

    public Sprite[] artefacts;

    private List<BoolVariable[]> _available;
    private List<Sprite[]> _icons;


    private Color _icon_alpha;

    private string _se_name;
    private string _sprite_ref;

    //hold index values
    private int _hold = 100;
    private int _sprite_dex;

    void Awake(){
        _available = new List<BoolVariable[]>{air_available, earth_available, fire_available, water_available};
        _icons = new List<Sprite[]>{air_abilities, earth_abilities, fire_abilities, water_abilities};

        for(int i = 0; i < unlocked.Length; i++){
            if(unlocked[i].Value) ChangeAlpha(i, 0.6f);
        }

        for(int i = 0; i < artefacts_unlocked.Length; i++){
            if(artefacts_unlocked[i].Value) all_elements[i].sprite = artefacts[i];
        }
    }

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

    //Icon alpha changes
    void ChangeAlpha(int element, float al){
        _icon_alpha = all_elements[element].color;
        _icon_alpha.a = al;
        all_elements[element].color = _icon_alpha;
    }

    void ChangeAttack(float al){
        _icon_alpha = ability_1.color;
        _icon_alpha.a = al;
        ability_1.color = _icon_alpha;
    }

    void ChangeOther(float al){
        _icon_alpha = ability_2.color;
        _icon_alpha.a = al;
        ability_2.color = _icon_alpha;
    }

    //Swaps element icons depending on the selected element value
    void SwitchAbilities(int element){
        passive.sprite = _icons[element][0];
        ability_1.sprite = _icons[element][1];
        ability_2.sprite = _icons[element][2];

        if(_available[element][0]){
            ChangeAttack(1f);
        }else{
            ChangeAttack(0.6f);
        }

        if(_available[element][1]){
            ChangeOther(1f);
        }else{
            ChangeOther(0.6f);       
        }
    }

    //Enact cooldowns (true value indicates first ability, false is the second)
    void Update(){
        for(int key = 0; key < _available.Count; key++){
            if(selectedElement.Value == key){
                if(_available[key][0].Value == true){
                    ChangeAttack(1f);
                }else{
                    ChangeAttack(0.6f);
                    cooldown.StartCooldown(selectedElement.Value, PlayerAttack.cooldowns[selectedElement.Value].attackEffects[0].cooldown, true);
                }

                if(_available[key][1].Value == true){
                    ChangeOther(1f);
                }else{
                    ChangeOther(0.6f);
                    cooldown.StartCooldown(selectedElement.Value, PlayerAttack.cooldowns[selectedElement.Value].otherEffects[0].cooldown, false);
                }
            }
        }
    }

    //Unlockables
    public void UnlockAir(){
        if(unlocked[0].Value) ChangeAlpha(0, 0.6f);
    }

    public void UnlockEarth(){
        if(unlocked[1].Value) ChangeAlpha(1, 0.6f);
    }

    public void UnlockFire(){
        if(unlocked[2].Value) ChangeAlpha(2, 0.6f);
    }

    public void UnlockWater(){
        if(unlocked[3].Value) ChangeAlpha(3, 0.6f);
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
