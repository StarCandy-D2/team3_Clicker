
using System.Collections;
using System.Collections.Generic;
using PlayerUpgrade;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    [Header("체력")]
    public float maxHP = 100f;
    private float currentHP;

    [Header("Layer 정보")]
    public int layerIndex; // 층 번호
    public EnemyGenerator enemyGenerator; // EnemyGenerator 참조

    [Header("보스 설정")] 
    public bool isBoss = false;
    
    [Header("파편 프리팹")]
    [SerializeField] GameObject RubblePrefab;
    void Start()
    {
        currentHP = maxHP;

    }






    public void Initialized(float hp, EnemyGenerator generator, bool boss = false)
    {
        maxHP = hp;
        currentHP = hp;
        enemyGenerator = generator;
        isBoss = boss;

        if (isBoss)
        {
            Debug.Log($"보스 등장! 체력: {maxHP}");
        }
    }
    
    public void TakeDamage(float damage)
    {
        Debug.Log($"체력{currentHP}/{damage}데미지 \n {currentHP -= damage}");
        currentHP -= damage;


        if (currentHP <= 0)
        {

            Die();

        }
    }
    

    void Die()

    {
        if (isBoss)
        {
            Debug.Log($"게임 승리!");
            // 게임 승리씬 호출?
            
            Destroy(gameObject);
            FadeManager.Instance.FadeOutAndLoadScene("EndingScene");

            return;
        }
        
      
      if (enemyGenerator != null)
        {
            enemyGenerator.OnLayerDestroyed();

        }  
        
        
        if (GameManager.Instance != null && GameManager.Instance.playerData != null)
        {
            var data = GameManager.Instance.playerData;
            
            float goldReward = 10;
            if (StageUIManager.Instance != null)
            {
                goldReward = StageUIManager.Instance.GetCurrentStageGoldReward()
                    * data.GetStat(StatType.goldGain);
                
            }
            data.SetStat(StatType.Gold, data.GetStat(StatType.Gold) + goldReward);

            if (StageUIManager.Instance != null)
            {
                StageUIManager.Instance.AddSessionGold(goldReward);
            }
        }

        if (StageUIManager.Instance != null)
        {
            StageUIManager.Instance.OnLayerCleared();
        }

        
        
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
    
    //
    // // 테스트용 클릭 함수
    void OnMouseDown()
    {
        Debug.Log("층 클릭됨!");
        TakeDamage(maxHP); // 한 번에 파괴
    }


}