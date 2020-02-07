using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class ArtefactPickUp : MonoBehaviour
{
    public BoolVariable air_artefact;
    public BoolVariable earth_artefact;
    public BoolVariable fire_artefact;
    public BoolVariable water_artefact;

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player"){ 
            if(this.name.ToLower() == "breatheoflife") air_artefact.Value = true;       
            if(this.name.ToLower() == "treeofwisdom") earth_artefact.Value = true;       
            if(this.name.ToLower() == "eternalflame") fire_artefact.Value = true;       
            if(this.name.ToLower() == "masteredtides") water_artefact.Value = true;       

            Destroy(gameObject);
        }
    }
}
