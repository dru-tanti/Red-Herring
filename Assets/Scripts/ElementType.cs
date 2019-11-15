using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Enums/Element Type", fileName = "Element Type")]
public class ElementType : ScriptableObject
{
    [SerializeField] private Sprite _image;
    public Sprite image { get => _image; }

    [SerializeField] private List<ElementType> _counters;
    
    public bool Counters(ElementType type)
    {
        return _counters.Contains(type);
    }
}
