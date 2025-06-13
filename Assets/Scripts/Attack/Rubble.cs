using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubble : MonoBehaviour 
{
    //땅이 파괴 될 때 파편 튀는 스크립트입니다.
    //사용법
    //1. 파편이 들어있는 오브젝트 프리팹 준비
    //2. 프리팹 내부에 있는 파편에 전부 이 스크립트를 붙여줌
    //3. 각각 파편 오브젝트에는 Rigidbody 2D가 있어야함.
    //4. 메인 타일 인스팩터에 프리팹을 붙여줌.
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float forceX = Random.Range(-1.5f, 1.5f);
        float forceY = Random.Range(4f, 7f);
        rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);

        Destroy(gameObject, 2f); // 2초후 제거
    }
}
