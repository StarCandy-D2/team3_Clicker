using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update

    private int preChildCount;

    public GameObject background;
    public SpriteRenderer spriteRenderer;
    public Material material;
    void Start()
    {
        preChildCount = transform.childCount;
        Debug.Log($"{transform.childCount}dd");
        //spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        material.mainTextureScale = new Vector2(1f, 1f);
    }

    
    void Update()
    {
        int currentChildCount = transform.childCount;
        int destroyedCound = currentChildCount - preChildCount;

        if(destroyedCound >0 )
        {
            material.mainTextureOffset = new Vector2(0, material.mainTextureOffset.y-0.03f);
            //background.transform.position += Vector3.up * 0.5f;
            transform.position += Vector3.up * 0.5f;
            Debug.Log($"{transform.childCount} dd");
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Vector3 childPosition = child.position;
                if (childPosition.y >= -1.3f)
                {
                    childPosition.y = -1.3f;
                    child.position = childPosition;

                }
            }
        }


        preChildCount = currentChildCount;
    }
}
