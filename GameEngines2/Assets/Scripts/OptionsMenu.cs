using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionsMenu : MonoBehaviour
{
    public UnityAction onBackButton;

    public void OnBackButtonPressed()
    {
        onBackButton();
    }
}
