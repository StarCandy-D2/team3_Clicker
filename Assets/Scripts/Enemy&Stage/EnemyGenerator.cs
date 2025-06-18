using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    [Header("스테이지별 데이터")] 
    [SerializeField] private StageData[] stageDataArray;

    public int totalLayerGenerated = 0;
    // [Header("현재 스테이지")] 
    // [SerializeField] private StageData currentStageData;

    [Header("생성 설정")] 
    [SerializeField] private int preloadLayers = 10;

    [SerializeField] private Transform worldContainer;

    private Queue<System.Action> destroyQueue = new Queue<System.Action>();
    private bool isProcessingQueue = false;


    public List<Enemy> activeLayers = new List<Enemy>(); // GameLayer → Enemy로 변경

    private int currentLayerIndex = 0;
    private float currentWorldY = 0f;
    private bool bossGenerated = false;

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
    
        // 테스트용 
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     // B키: 바로 보스 직전(495층)으로 점프
        //     totalLayerGenerated = 495;
        //     Debug.Log($"totalLayerGenerated를 {totalLayerGenerated}로 설정");
        // }
        //
        // if (Input.GetKeyDown(KeyCode.G))
        // {
        //     // G키: 바로 보스 생성
        //     Debug.Log("보스 생성");
        //     GenerateBossLayer();
        // }
    }

    // 레이어사용시
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
        

      
            int generationStage = (totalLayerGenerated / 100) + 1;
            StageData stageData = GetStageData(generationStage);

            if (generationStage > 5 && !bossGenerated)
            {
                Debug.Log("6스테이지 도달 보스 생성");
                GenerateBossLayer();
                bossGenerated = true;
                return;
            }

            if (generationStage > 5 && bossGenerated)
            {
                Debug.Log("보스 이미 생성됨. 더 이상 층 생성 안 함");
                return;
            }


            if (stageData == null) return;

            StageData baseStage = stageDataArray[0];
            float standardHeight = stageData.layerHeight;

            // 층 생성 - 항상 맨 아래(가장 낮은 위치)에 생성
            GameObject layerObject = Instantiate(stageData.layerPrefab, worldContainer);

            // 현재 활성 층들의 개수를 기준으로 위치 계산
            float newLayerY = -((activeLayers.Count) * standardHeight);
            layerObject.transform.position = new Vector3(0, newLayerY - 1.3f, 0); //최초 생성 위치 y값에서 조정가능


            // Enemy 컴포넌트 가져오기
            Enemy layerEnemy = layerObject.GetComponent<Enemy>();
            if (layerEnemy != null)
            {
                layerEnemy.layerIndex = currentLayerIndex; // 층 번호 설정
                layerEnemy.enemyGenerator = this; // 자기 참조 설정
                activeLayers.Add(layerEnemy);
            }


            currentLayerIndex++;
            totalLayerGenerated++;
        
    }

    private void GenerateBossLayer()
    {
       
        var bossStage = GetStageData(6);
        if (bossStage.bossPrefab != null)
        {
            
            var bossLayer = Instantiate(bossStage.bossPrefab, worldContainer);
            var newLayerY = -(activeLayers.Count * bossStage.layerHeight);
            bossLayer.transform.position = new Vector3(0, newLayerY - 4.85f, 0);
            
            var bossEnemy = bossLayer.GetComponent<Enemy>();
            if (bossEnemy != null)
            {
                
                bossEnemy.Initialized(bossStage.bossHP, this, true);
                activeLayers.Add(bossEnemy);
                
            }

            
        }
    }
    private StageData GetStageData(int stage)
    {
        int index = Mathf.Clamp(stage -1, 0, stageDataArray.Length - 1);
        return stageDataArray[index];
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
        int currentDisplayStage = ((currentLayerIndex - activeLayers.Count) / 100) + 1;
        StageData stageData = GetStageData(currentDisplayStage);
        
        currentWorldY += stageData.layerHeight;
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
    
    
    
    
    // public void ChangeStage(StageData newStageData)
    // {
    //     currentStageData = newStageData;
    //     // 스테이지 변경 로직
    // }
}

