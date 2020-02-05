using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PauseMenu : MonoBehaviour
{
    public UnityAction onResume;

    public void onResumeActivate()
    {
        onResume();
    }
}
