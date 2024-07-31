using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing : MonoBehaviour
{
    public enum Axis { up, down, right, left, forward, back }

    public Camera refCamera;
    public bool reverseFace = false;
    public Axis axis = Axis.up;

    private void Awake()
    {
        if (!refCamera)
            refCamera = Camera.main;
    }

    public Vector3 GetAxis(Axis axis)
    {
        switch (axis)
        {
            case Axis.down:
                return Vector3.down;
            case Axis.right:
                return Vector3.right;
            case Axis.left:
                return Vector3.left;
            case Axis.forward:
                return Vector3.forward;
            case Axis.back:
                return Vector3.back;
            default:
                return Vector3.up;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPos = transform.position + refCamera.transform.rotation * (reverseFace ? Vector3.forward : Vector3.back);

        Vector3 targetOrientation = refCamera.transform.rotation * GetAxis(axis);

        transform.LookAt(targetPos, targetOrientation);
    }
}
