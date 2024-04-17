using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = target as FieldOfView;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRange);

        Vector3 viewAngleLeft = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleRight = fov.DirectionFromAngle(fov.viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.viewRange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.viewRange);

        Handles.color = Color.red;
        foreach(Transform visibleTarget in fov.visibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }
}
