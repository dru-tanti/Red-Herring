using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthMeter : MonoBehaviour
{
    [SerializeField]
    public IntVariable max_health;

    [SerializeField]
    public GameObject pool;
    
    public void HealthChanged(int current_health)
    {   
        Debug.Log(current_health.ToString());
        pool.GetComponent<Image>().fillAmount = 1.0f * current_health / max_health.Value;
    }
}
