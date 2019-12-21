using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class UIManaMeter : MonoBehaviour
{
    [SerializeField]
    public IntVariable max_mana;

    [SerializeField]
    public GameObject pool;
    
    [SerializeField]
    public IntVariable selectedElement;

    public Sprite[] all_elements;
    public Image icon;

    private string se_name;
    private string sprite_ref;

    //hold index values
    private int hold = 100;
    private int sprite_dex;

    void FixedUpdate()
    {
        //If the selected element's index value has changed, commit the change in the UI
        if(hold != selectedElement.Value){
            
            //Making sure the selected element's index is the same as the sprite index
            //If not, find the necessary sprite and display that
            se_name = PlayerAttack.elements[selectedElement.Value].name;
            se_name = se_name.Substring(0, se_name.Length - 9);

            sprite_ref = all_elements[selectedElement.Value].name;

            //Name comparison, so PLEASE make sure that the elements HAVE THE SAME NAME THROUGHOUT
            if(se_name.ToLower() == sprite_ref.ToLower()){
                icon.sprite = all_elements[selectedElement.Value];
            }else{
                sprite_dex = System.Array.FindIndex(all_elements, e => e.name == se_name.ToLower());
                icon.sprite = all_elements[sprite_dex];
            }

            hold = selectedElement.Value;
        }
    }

    public void ManaChanged(int current_mana)
    {   
        pool.GetComponent<Image>().fillAmount = 1.0f * current_mana / max_mana.Value;
    }
}
