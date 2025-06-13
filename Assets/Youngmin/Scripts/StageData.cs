using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/StageData")]
public class StageData : ScriptableObject
{
   [Header("스테이지 기본 정보")]
   public string stageName;
   public int stageLevel;
    
   [Header("층 설정")]
   public int totalLayers = 100;           //스테이지별 층 개수
   public float baseLayerHP = 1f;          // 층 기본 체력
   public float hpMultiplier = 1f;  //스테이지별 체력 배수
   public float layerHeight = 1f; // 층 높이

   [Header("프리팹")] 
   public GameObject layerPrefab;
    
   [Header("보스")]
   public GameObject bossPrefab;
    
   // 실제 층 체력 계산
   public float GetLayerHP()
   {
      return baseLayerHP * hpMultiplier;
   }
}
