using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    [SerializeField] private int enemyIndex; //몇번째  
    
    public System.Action<Enemy> OnEnemyDestroyed;
    
    public void Initialize(float hp, int index)
    {
        maxHP = hp;
        currentHP = hp;
        enemyIndex = index;
    }
    
    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        
        // 체력바 업데이트,이펙트 등
        
        if (currentHP <= 0)
        {
            DestroyEnemy();
        }
    }
    
    private void DestroyEnemy()
    {
        OnEnemyDestroyed?.Invoke(this);
        // 파괴 이펙트, 사운드 등
        Destroy(gameObject);
    }
    
    public float GetHPPercentage() // 체력바에 필요하면?
    {
        return currentHP / maxHP;
    }
}
