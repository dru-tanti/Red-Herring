using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

//----------------------------------------------------------------
// Handles the parameters of the individual button.
//----------------------------------------------------------------

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int index;
    public Image circle;
    public Image icon;
    public RadialMenu myMenu;
    Color defaultColor;

    public void OnPointerEnter(PointerEventData eventData)
    {
        myMenu.selected = this;
        defaultColor = circle.color;
        circle.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myMenu.selected = null;
        circle.color = defaultColor;
    }
}
