using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------------------------------------------------------
// Contains The properties of this element.
// Can be later used to give the element further properties.
//--------------------------------------------------------------------------------------------------------------------------

[CreateAssetMenu(menuName = "Game/Enums/Element Type", fileName = "Element Type")]
public class ElementType : ScriptableObject
{
    // [SerializeField] private Sprite _image;
    // public Sprite image { get => _image; }

    // [SerializeField] private List<ElementType> _counters;

    [SerializeField] private List<ElementEffect> _effects;
    public List<ElementEffect> effects { get => _effects; }
    
    // public bool Counters(ElementType type)
    // {
    //     return _counters.Contains(type);
    // }
}
