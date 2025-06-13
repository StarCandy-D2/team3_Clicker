using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("현재 스테이지")] 
    [SerializeField] private StageData currentStageData;

    [Header("생성 설정")] 
    [SerializeField] private int preloadLayers = 5;
    [SerializeField] private Transform worldContainer;
    
    private List<GameLayer> activeLayers = new List<GameLayer>();
    private int currentLayerIndex = 0;
    private float currentWorldY = 0f;
    private bool isMovingWorld = false;

    void Start()
    {
        for (int i = 0; i < preloadLayers; i++)
        {
            GenerateLayer();
        }
    }

    void Update()
    {
        CheckLayerDestruction();
    }

    private void CheckLayerDestruction()
    {
        if (activeLayers.Count > 0)
        {
            GameLayer firstLayer = activeLayers[0];

            if (firstLayer.IsDestroyed() && !isMovingWorld)
            {
                OnLayerDestroyed(firstLayer);
            }
        }
    }

    public void GenerateLayer()
    {
        if (currentStageData == null) return;
        
        //층 생성
        GameObject layerObject = Instantiate(currentStageData.layerPrefab, worldContainer);
        layerObject.transform.position = new Vector3(0, -currentLayerIndex * currentStageData.layerHeight, 0);
        
        //게임 레이어 컴포넌트 설정
        GameLayer layer = layerObject.GetComponent<GameLayer>();
        if (layer == null)
        {
            layer = layerObject.AddComponent<GameLayer>();
        }

        layer.Initialize(currentLayerIndex);

        Enemy[] enemies = layerObject.GetComponentsInChildren<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Initialize(currentStageData.GetLayerHP(), currentLayerIndex);
        }
        activeLayers.Add(layer);
        currentLayerIndex++;
    }

    private void OnLayerDestroyed(GameLayer destroyedLayer)
    {
        MoveWorldUp(); //위로이동
        activeLayers.Remove(destroyedLayer);
        GenerateLayer();
        Destroy(destroyedLayer.gameObject, 1f);
        
    }

    private void MoveWorldUp()
    {
        currentWorldY += currentStageData.layerHeight;
        StartCoroutine(SmoothMoveWorld()); //부드러운이동
    }

    private System.Collections.IEnumerator SmoothMoveWorld()
    {
        isMovingWorld = true;

        Vector3 startPos = worldContainer.position; //시작
        Vector3 targetPos = new Vector3(0, currentWorldY, 0); //타겟

        float duration = 0.3f; //이동 시간
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            //부드러운 이동
            worldContainer.position = Vector3.Lerp(startPos, targetPos, progress);
            yield return null;

        }

        worldContainer.position = targetPos;
        isMovingWorld = false;
    }


    public void ChangeStage(StageData newStageData)
    {
        currentStageData = newStageData;
        //스테이지 변경 로직
    }
    

}
