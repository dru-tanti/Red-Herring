using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthMeter : MonoBehaviour
{
    [SerializeField]
    private IntVariable max_health;

    [SerializeField]
    private GameObject pool;
    
    public void HealthChanged(int health)
    {   
        pool.GetComponent<Image>().fillAmount = health;
    }
}
