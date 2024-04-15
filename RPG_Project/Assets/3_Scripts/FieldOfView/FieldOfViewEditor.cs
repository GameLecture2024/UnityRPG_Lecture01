using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRange);

        Vector3 viewAngleLeft = DirectionFromAngle(fov.viewAngle / 2 * -1);
        Vector3 viewAngleRight = DirectionFromAngle(fov.viewAngle / 2);

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov.viewRange);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov.viewRange);

        Handles.color = Color.red;

        foreach(Transform visibleTarget in fov.VisibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }

    }

    private Vector3 DirectionFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
