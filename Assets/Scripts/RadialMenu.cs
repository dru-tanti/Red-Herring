using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Once the Radial menu is spawned, this will handle the behaviour of the menu, and will spawn the buttons.
//--------------------------------------------------------------------------------------------------------------------------

public class RadialMenu : MonoBehaviour
{
    public RadialButton buttonPrefab;
    public RadialButton selected;
    public Attack attack;

    // Finds the Attack script so that we can set the selected element from what the user selects from the menu.
    private void Awake() {
        attack = GameObject.Find("Weapon").GetComponent<Attack>();
    }

    // Needs an instance of where the custom class Action() is located (in this case in the Player Controller Script.).
    public void SpawnButtons(PlayerController obj)
    {
        // Slows down time while the menu is active.
        Time.timeScale = 0.0f;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        // Creates a button around the circumference of a circle depending on how many buttons we defined.
        for(int i=0; i<obj.elements.Length; i++)
        {
            RadialButton newButton = Instantiate(buttonPrefab) as RadialButton;
            newButton.transform.SetParent(transform, false);
            
            // Finds how many buttons need to be created and divides the circumference of the circle accordingly.
            float tetha = (2 * Mathf.PI / obj.elements.Length) * i;
            float xPos = Mathf.Sin(tetha);
            float yPos = Mathf.Cos(tetha);

            // Sets the position and information of every button.
            newButton.transform.localPosition = new Vector2(xPos, yPos) * 100f;
            newButton.circle.color = obj.elements[i].colour;
            newButton.icon.sprite = obj.elements[i].sprite;
            newButton.index = obj.elements[i].index;

            // Sets the menu to this current instance of the script.
            newButton.myMenu = this;
        }
    }

    // If the user releases the right mouse button, set the selectedElement to what was chosen.
    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
            if(selected)
            {
                attack.selectedElement = selected.index;
            }
            Destroy(gameObject);
        }
    }
}
