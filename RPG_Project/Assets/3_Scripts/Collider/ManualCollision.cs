using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualCollision : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(2, 2, 2);
    public LayerMask targetLayer;

    public Collider[] CheckOverlapBox()
    {
        return Physics.OverlapBox(transform.position, boxSize / 2, transform.rotation, targetLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);

    }
}
