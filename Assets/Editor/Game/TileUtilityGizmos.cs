using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileUtilityGizmos
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NonSelected )]
    static void DrawPlayerCoordinates(TileTest tileScript, GizmoType type)
    {
        Vector3 position = tileScript.transform.position;
        float radius = tileScript.radius;

        float alpha = Is(type, GizmoType.NonSelected) ? 0.5f : 1f;
        
        Color handlesColor = Color.red;
        handlesColor.a = alpha;
        Handles.color = handlesColor;

        Handles.DrawWireDisc(tileScript.playerTop, Vector3.up, 0.1f);
        Handles.DrawWireDisc(tileScript.playerBack, Vector3.back, 0.1f);

    }

    static bool Is(GizmoType check, GizmoType desired)
        => (check & desired) == desired;
}
