using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRange = 5f;
    [Range(0, 180)]
    public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstalceMask;

    public List<Transform> visibleTargets = new List<Transform>();
    private Transform nearestTarget;
    private float distanceToTarget = 0.0f;

    public Transform NearesetTarget => nearestTarget;

    public float delay = 0.2f;

    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(delay));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        distanceToTarget = 0.0f;
        nearestTarget = null;
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRange, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;

            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstalceMask))
                {
                    visibleTargets.Add(target);
                    if(nearestTarget == null || distanceToTarget > dstToTarget)
                    {
                        nearestTarget = target;
                        distanceToTarget = dstToTarget;
                    }
                }
            }
        }
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0 ,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
