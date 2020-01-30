using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyMeter : MonoBehaviour
{
    [SerializeField]
    public IntVariable keys;

    [SerializeField]
    public GameObject[] keyring;
    
    public void KeyCollected(int qui)
    {   
        Debug.Log("key collected");
        keyring[keys.Value - 1].GetComponent<Image>().color = Color.white;
    }
}
