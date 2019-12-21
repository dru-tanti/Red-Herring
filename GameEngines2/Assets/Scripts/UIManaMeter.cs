using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI; 

public class UIManaMeter : MonoBehaviour
{
    private Image element;
    private int hold = 100 ;

    public IntVariable selectedElement;
    public Sprite[] all_elements;

    void Awake()
    {
        element = this.GetComponent<Image>();
    }

    void FixedUpdate()
    {
        if(hold != selectedElement.Value){

            hold = selectedElement.Value;
        }
    }
}
