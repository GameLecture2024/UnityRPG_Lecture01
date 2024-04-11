using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 목표 지점을 순찰하는 클래스. index가 최대가 되면 0으로 초기화한다.
/// </summary>

public class SimplePatrol : MonoBehaviour
{
    [SerializeField] private Transform[] paths;              // 인스팩터 창에서 연결해줘야 한다.
    private int currentPath = 0;
    public float moveSpeed = 3.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (paths[currentPath].position - transform.position).normalized;

        transform.position = transform.position + direction * moveSpeed * Time.deltaTime;

        if ((paths[currentPath].position - transform.position).sqrMagnitude < 0.1f)     // Vector3.Distance()보다 빠르다.
        {
            if (currentPath < paths.Length - 1)
                currentPath++;
            else
                currentPath = 0;         
        }
    }
}
