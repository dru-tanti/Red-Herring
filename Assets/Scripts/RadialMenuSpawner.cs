using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Handles the spawning of the Radial Menu. Once the Menu has been spawned the script radialMenu takes over.
//--------------------------------------------------------------------------------------------------------------------------

public class RadialMenuSpawner : MonoBehaviour
{
    public static RadialMenuSpawner ins;
    public RadialMenu menuPrefab;

    // Sets this as the current instance of the Radial Menu Spawner.
    void Awake() 
    {
        ins = this;
    }

    public void SpawnMenu(PlayerController obj)
    {
        RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
        newMenu.transform.SetParent(transform, false);
        newMenu.transform.position = Input.mousePosition;

        newMenu.SpawnButtons(obj);
    }
}
