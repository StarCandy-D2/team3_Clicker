using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTile : MonoBehaviour
{
    [SerializeField] GameObject breakDebrisPrefab;
    public float tileHP = 100f;
    public float currentHP;
    void Start()
    {
        currentHP = tileHP;
    }



    public void TakeDamage(float dmg)
    {

        currentHP -= dmg;
        Debug.Log($"데미지 {dmg}\n땅 체력{tileHP}/{currentHP}");
        if (currentHP <= 0)
        {
            Instantiate(breakDebrisPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}
