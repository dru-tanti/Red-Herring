using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class FireText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI myText;
    public string element;
 
     void Awake (){
         myText = GetComponentInChildren<TextMeshProUGUI>();
     }

     void Start () {
         myText.color = new Color(0F, 0F, 0F, 0F);
         myText.text = element;
     }
 
     public void OnPointerEnter (PointerEventData eventData)
     {
         myText.color = new Color(1F, 1F, 1F, 1F);
     }
 
     public void OnPointerExit (PointerEventData eventData)
     {
        myText.color = new Color(0F, 0F, 0F, 0F);

     }
   
}
