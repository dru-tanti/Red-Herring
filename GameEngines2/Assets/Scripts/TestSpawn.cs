using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    [SerializeField]
    public IntVariable death_toll;

    public AITest enemy_spawn;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemy_spawn, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Spawn(){
        if(death_toll.Value != 0){
            Instantiate(enemy_spawn, this.transform.position, Quaternion.identity);
        }
    }
}
