using System.Collections;
using System.Collections.Generic;
using UnityAtoms;
using UnityEngine;

public class IsInvisibleListener : MonoBehaviour
{
    [SerializeField]
    private BoolEvent _event = null;

    void Start() {
        _event.Register(OnInvisible);
    }

    void OnDestroy() {
        _event.Unregister(OnInvisible);
    }

    private void OnInvisible(bool isInvisible) {
        // Debug.Log(isInvisible);
    }
}
