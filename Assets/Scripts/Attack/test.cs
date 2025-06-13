using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update

    private int preChildCount;

    public GameObject background;

    void Start()
    {
        preChildCount = transform.childCount;
        Debug.Log($"{transform.childCount}dd");
    }

    
    void Update()
    {
        int currentChildCount = transform.childCount;

        if(currentChildCount < preChildCount)
        {
            background.transform.position += Vector3.up * 0.5f;
            transform.position += Vector3.up * 0.5f;
            Debug.Log($"{transform.childCount} dd");
        }

        preChildCount = currentChildCount;
    }
}
