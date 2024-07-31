using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 각도를 통해 타겟을 감지하는 시야를 구현하는 클래스
/// </summary>
public class FieldOfView : MonoBehaviour
{
    Enemy_Zombie enemy;

    public float viewRange = 5f;
    [Range(0, 180)]
    public float viewAngle = 90f;

    public LayerMask targetMask;
    public LayerMask obstalceMask;

    [Header("시야 각 내에 있는 타겟 중 가장 가까이에 있는 대상을 찾는 변수")]
    private List<Transform> visibleTargets = new List<Transform>();
    private Transform nearestTarget;
    private float distanceToTarget = 0.0f;

    public float findDelay = 0.2f;  // 이 시간마다 Target을 찾는 함수를 실행한다.

    #region 프로퍼티
    public Transform NearestTarget => nearestTarget;
    public List<Transform> VisibleTargets => visibleTargets;
    #endregion

    private void Awake()
    {
        enemy = GetComponent<Enemy_Zombie>();
    }

    private void Start()
    {
        viewRange = enemy.ViewRange;

        StartCoroutine(FindTargetsWithDelay(findDelay));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    /// <summary>
    /// 시야 각도 내에 있는 객체를 거리로 비교하여 최소 거리의 객체를 반환한다.
    /// </summary>
    void FindVisibleTargets()
    {
        // 함수 실행마다 데이터를 초기화
        distanceToTarget = 0.0f;        // 가장 가까이 있는 타겟의 거리
        nearestTarget = null;
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRange, targetMask);

        for(int i =0; i< targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;   // 범위 안의 있는 랜덤한 타겟 하나를 지정
            Vector3 dirToTarget = (target.position - transform.position).normalized;  // 타겟과의 방향을 구한다.

            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstalceMask)) // 방해물이 없을 경우
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
}
