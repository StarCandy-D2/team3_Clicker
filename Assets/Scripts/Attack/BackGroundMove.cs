using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
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
            material.mainTextureOffset = new Vector2(0, material.mainTextureOffset.y - 0.03f * destroyedCount);

        }

        preChildCount = currentChildCount;
    }
}
