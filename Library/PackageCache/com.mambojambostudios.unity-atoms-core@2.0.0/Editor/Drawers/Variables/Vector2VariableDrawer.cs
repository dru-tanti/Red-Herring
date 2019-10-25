#if UNITY_2019_1_OR_NEWER
using UnityEditor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable property drawer of type `Vector2`. Inherits from `AtomDrawer&lt;Vector2Variable&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(Vector2Variable))]
    public class Vector2VariableDrawer : AtomDrawer<Vector2Variable> { }
}
#endif
