using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class UIManaMeter : MonoBehaviour
{
    //[SerializeField]
    //public IntVariable max_mana;

    //[SerializeField]
    //public GameObject pool;
    
    [SerializeField]
    public IntVariable selectedElement;

    public Sprite[] all_elements;
    public Image icon;

    private string _se_name;
    private string _sprite_ref;

    //hold index values
    private int _hold = 100;
    private int _sprite_dex;

    void FixedUpdate()
    {
        //If the selected element's index value has changed, commit the change in the UI
        if(_hold != selectedElement.Value){
            
            //Making sure the selected element's index is the same as the sprite index
            //If not, find the necessary sprite and display that
            _se_name = PlayerAttack.element_list[selectedElement.Value].name;
            _se_name = _se_name.Substring(0, _se_name.Length - 9);

            _sprite_ref = all_elements[selectedElement.Value].name;

            //Name comparison, so PLEASE make sure that the elements HAVE THE SAME NAME THROUGHOUT
            if(_se_name.ToLower() == _sprite_ref.ToLower()){
                icon.sprite = all_elements[selectedElement.Value];
            }else{
                _sprite_dex = System.Array.FindIndex(all_elements, e => e.name == _se_name.ToLower());
                icon.sprite = all_elements[_sprite_dex];
            }

            _hold = selectedElement.Value;
        }
    }

    /* Previous version: Mana had a pool and regen
    public void ManaChanged(int current_mana)
    {   
        pool.GetComponent<Image>().fillAmount = 1.0f * current_mana / max_mana.Value;
    }
    */
}
