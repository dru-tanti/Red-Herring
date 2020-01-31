using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PanelOpen : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public GameObject Panel;
    public PanelText text;
    public void OpenPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
        }
    }

    public void SetText(string element)
    {
        myText.SetText(string.Format("You chose {0}", element, "!"));
    }
}
