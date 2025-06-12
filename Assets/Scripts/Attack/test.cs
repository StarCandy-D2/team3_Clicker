using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        float forceX = Random.Range(-1.5f, 1.5f);
        float forceY = Random.Range(1f, 3f);
        rb.AddForce(new Vector2(forceX, forceY), ForceMode2D.Impulse);

        Destroy(gameObject, 2f); // 몇 초 후 자동 제거
    }
}
