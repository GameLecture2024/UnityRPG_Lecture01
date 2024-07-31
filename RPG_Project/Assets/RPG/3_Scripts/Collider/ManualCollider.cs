using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격할 대상의 LayerMask를 manualCollider 컴포넌트에서 지정해주세요.
/// </summary>
public class ManualCollider : MonoBehaviour
{
    public Vector3 boxSize = new Vector3(2, 2, 2);
    public LayerMask layermask;

    /// <summary>
    /// 객체의 회전 방향으로 충돌을 검사하는 LayerBox를 생성하고, 충돌한 객체를 반환한다.
    /// </summary>
    public Collider[] GetColliderObject() 
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxSize / 2, transform.rotation, layermask);
        return colliders;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, boxSize);
    }
}
