using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [Header("현재 스테이지")]
    [SerializeField] private StageData currentStageData;

    [Header("생성 설정")]
    [SerializeField] private int preloadLayers = 5;
    [SerializeField] private Transform worldContainer;

    private Queue<System.Action> destroyQueue = new Queue<System.Action>();
    private bool isProcessingQueue = false;


    private List<Enemy> activeLayers = new List<Enemy>(); // GameLayer → Enemy로 변경

    private int currentLayerIndex = 0;
    private float currentWorldY = 0f;


    void Start()
    {
        for (int i = 0; i < preloadLayers; i++)
        {
            GenerateLayer();
        }
    }

    void Update()
    {
        // CheckLayerDestruction();
        ProcessDestroyQueue();
    }

    // private void CheckLayerDestruction()
    // {
    //     if (activeLayers.Count > 0)
    //     {
    //
    //         Enemy firstLayer = activeLayers[0];
    //
    //         // Enemy가 파괴되었는지 확인 (null 체크)
    //         if (firstLayer == null && !isMovingWorld)
    //         {
    //             AddToDestroyQueue();
    //
    //         }
    //     }
    // }

    private void AddToDestroyQueue()
    {
        destroyQueue.Enqueue(ProcessLayerDestruction);
    }

    private void ProcessDestroyQueue()
    {
        if (!isProcessingQueue && destroyQueue.Count > 0)
        {
            isProcessingQueue = true;
            System.Action nextAction = destroyQueue.Dequeue();
            nextAction.Invoke();
        }
    }



    public void GenerateLayer()
    {
        if (currentStageData == null) return;


        // 층 생성 - 항상 맨 아래(가장 낮은 위치)에 생성
        GameObject layerObject = Instantiate(currentStageData.layerPrefab, worldContainer);

        // 현재 활성 층들의 개수를 기준으로 위치 계산
        float newLayerY = -((activeLayers.Count) * currentStageData.layerHeight);
        layerObject.transform.position = new Vector3(0, newLayerY-1.3f, 0); //최초 생성 위치 y값에서 조정가능

        // Enemy 컴포넌트 가져오기
        Enemy layerEnemy = layerObject.GetComponent<Enemy>();
        if (layerEnemy != null)
        {
            layerEnemy.layerIndex = currentLayerIndex; // 층 번호 설정
            layerEnemy.enemyGenerator = this; // 자기 참조 설정
            activeLayers.Add(layerEnemy);
        }

        currentLayerIndex++;
    }

    public void ProcessLayerDestruction()
    {
        // 첫 번째 층 제거 (이미 null이 됨)
        if (activeLayers.Count > 0)
        {
            activeLayers.RemoveAt(0);
        }

        StartCoroutine(HandleLayerDestruction());

    }

    private IEnumerator HandleLayerDestruction()
    {
        currentWorldY += currentStageData.layerHeight;
        yield return StartCoroutine(SmoothMoveWorld());

        GenerateLayer();
        isProcessingQueue = false;
    }

    public void OnLayerDestroyed()
    {
        AddToDestroyQueue();
    }

    // private void MoveWorldUp()
    // {
    //     currentWorldY += currentStageData.layerHeight;
    //
    //     StartCoroutine(SmoothMoveWorld()); // 부드러운 이동
    //
    // }

    private System.Collections.IEnumerator SmoothMoveWorld()
    {



        Vector3 startPos = worldContainer.position; // 시작
        Vector3 targetPos = new Vector3(0, currentWorldY, 0); // 타겟

        float duration = 0.1f; // 이동 시간

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;


            // 부드러운 이동
            worldContainer.position = Vector3.Lerp(startPos, targetPos, progress);
            yield return null;

        }

        worldContainer.position = targetPos;

    }


    public void ChangeStage(StageData newStageData)
    {
        currentStageData = newStageData;
        // 스테이지 변경 로직
    }
}

