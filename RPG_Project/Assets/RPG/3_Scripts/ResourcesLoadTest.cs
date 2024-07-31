using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesLoadTest : MonoBehaviour
{
    public Image testImage;    // Canvas/BG

    private int currentNumber;

    // Start is called before the first frame update
    void Start()
    {
        testImage.sprite = Resources.Load<Sprite>("Album/Album_01") as Sprite;     // Resouces.Load 호출하는 데이터 -> Asset 저장이 된다. <T> 제네릭 형변환 구현
    }


    private void Update()
    {
        // 키보드에 입력하는 1번이. - 우리가 호출하는 앨범 이미지의 1번과 매칭시켜서 사용했다.

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCurrentNumber();
            ChangeTestImageDynamic(GetCurrentImageNumber());
        }
    }

    private void ChangeCurrentNumber()
    {
        // 클래스 - 정보를 담는 

        currentNumber = Random.Range(0, 3);  // 0 ~ 2 숫자를 반환하는 함수
    }

    private int GetCurrentImageNumber()
    {
        currentNumber = 0;

        return currentNumber;
    }


    public void ChangeTestImageDynamic(int imageNumber)
    {
        string path = "Album/Album_";

        path += imageNumber.ToString("D2"); // 01 Format형식

        Debug.Log(path);

        testImage.sprite = Resources.Load<Sprite>(path) as Sprite; // Resources.Load로 동적으로 이미지 변환
    }
}
