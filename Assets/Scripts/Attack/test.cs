using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private int preChildCount;
    public GameObject background;
    public SpriteRenderer spriteRenderer;
    public Material material;

    void Start()
    {
        preChildCount = transform.childCount;
        material = spriteRenderer.material;
        material.mainTextureScale = new Vector2(1f, 1f);
    }

    void Update()
    {
        int currentChildCount = transform.childCount;
        int destroyedCount = preChildCount - currentChildCount;

        if (destroyedCount > 0)
        {
            // 배경 위치 및 텍스처 이동
            transform.position += Vector3.up * 0.5f * destroyedCount;
            material.mainTextureOffset = new Vector2(0, material.mainTextureOffset.y - 0.03f * destroyedCount);

            // 자식들의 Y 위치 보정
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                Vector3 childPos = child.position;
                if (childPos.y >= -1.3f)
                {
                    childPos.y = -1.3f;
                    child.position = childPos;
                }
            }
        }

        preChildCount = currentChildCount;
    }
}
