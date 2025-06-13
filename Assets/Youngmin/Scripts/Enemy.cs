
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("체력")]
    public float maxHP = 100f;
    private float currentHP;

    [Header("Layer 정보")]
    public int layerIndex; // 층 번호
    public EnemyGenerator enemyGenerator; // EnemyGenerator 참조
    
    void Start()
    {
        currentHP = maxHP;

    }
    
    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        Debug.Log($"Enemy damaged! HP: {currentHP}/{maxHP}");
        
        if (currentHP <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        Debug.Log("Enemy destroyed!");
        
        // 파괴 이펙트 실행
        StartCoroutine(DestroyEffect());
    }
    
   
    IEnumerator DestroyEffect()
    {
        // 모든 자식 타일들에게 물리 효과 적용
        foreach (Transform child in transform)
        {
            child.SetParent(null);
            Rigidbody2D rb = child.gameObject.AddComponent<Rigidbody2D>();
            
            
            float forceX = Random.Range(-1.5f, 1.5f);
            float forceY = Random.Range(2f, 3f);
            rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);
            
            
            // 2초 후 파괴
            Destroy(child.gameObject, 1.5f);
        }

        yield return new WaitForSeconds(0.05f);
        
        Destroy(gameObject);
    }
    
   // // 테스트용 클릭 함수
   //  void OnMouseDown()
   //  {
   //      Debug.Log("층 클릭됨!");
   //      TakeDamage(maxHP); // 한 번에 파괴
   //  }

}