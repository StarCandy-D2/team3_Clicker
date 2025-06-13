using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    [SerializeField] private int enemyIndex;
    
    public GameLayer parentLayer; // 부모 레이어 참조
    
    public void Initialize(float hp, int index)
    {
        maxHP = hp;
        currentHP = hp;
        enemyIndex = index;
    }
    
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        Debug.Log($"데미지 {damage}\n땅 체력{maxHP}/{currentHP}");

        if (currentHP <= 0)
        {
            DestroyEnemy();
        }
    }
    
    private void DestroyEnemy()
    {
        // 파괴 신호
        if (parentLayer != null)
        {
            parentLayer.OnEnemyDestroyed();
        }
        //이펙트 등
        Destroy(gameObject);
    }
    
    public float GetHPPercentage()
    {
        return currentHP / maxHP;
    }
}