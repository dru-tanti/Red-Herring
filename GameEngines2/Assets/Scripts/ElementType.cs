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
    [SerializeField] private List<ElementEffect> _attackEffects;
    public List<ElementEffect> attackEffects { get => _attackEffects; }

    [SerializeField] private List<ElementEffect> _otherEffects;
    public List<ElementEffect> otherEffects { get => _otherEffects; }

    [SerializeField] private List<ElementEffect> _passiveEffects;
    public List<ElementEffect> passiveEffects { get => _passiveEffects; }
    
    // public bool Counters(ElementType type)
    // {
    //     return _counters.Contains(type);
    // }
}
