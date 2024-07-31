using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 변수를 할당해주지 않으면 transform 회전이 실행 되고, 변수를 false까지 할당하면 rotation회전이 실행 된다.
/// </summary>
public class ShakeCamera : SingletonMonoBehaviour<ShakeCamera>
{
    [SerializeField] private float shakeTime;       // 카메라 흔들리는 시간
    [SerializeField] private float shakeIntensitry;  // 카메라 흔들림 크기
    public void OnShakeCamera(float shakeTime = 1.0f, float shakeIntensitry = 0.1f, 
        bool positionShake = true)
    {
        this.shakeTime = shakeTime;
        this.shakeIntensitry = shakeIntensitry;

        if (positionShake)
            StartCoroutine(ShakeByPosition());
        else
            StartCoroutine(ShakeByRotation());
    }

    IEnumerator ShakeByPosition() 
    {
        Vector3 startCameraPos = transform.position;    // 현재 카메라의 위치 저장.

        while(shakeTime > 0.0f)
        {
            transform.position = startCameraPos + UnityEngine.Random.insideUnitSphere * shakeIntensitry;

            shakeTime -= Time.deltaTime;

            yield return null;
        }
    }
    IEnumerator ShakeByRotation() 
    {
        Vector3 startCameraRot = transform.eulerAngles;    // 현재 카메라의 각도 저장.

        float power = 10f;

        while (shakeTime > 0.0f)
        {
            // 변수 설정
            float x = 0;
            float y = 0;
            float z = UnityEngine.Random.Range(-1, 1);

            // 카메라 각도에 따른 흔들림
            transform.rotation = Quaternion.Euler(startCameraRot 
                + new Vector3(x,y,z) * shakeIntensitry * power);

            // 시간 설정
            shakeTime -= Time.deltaTime;
            yield return null;
        }
    }
}
