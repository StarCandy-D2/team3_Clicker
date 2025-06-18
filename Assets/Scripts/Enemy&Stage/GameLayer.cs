
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class GameLayer : MonoBehaviour
// {
//
//     [SerializeField] private Enemy[] enemiesInLayer;
//     [SerializeField] private int layerIndex;
//     [SerializeField] private int destroyedEnemyCount = 0;
//     [SerializeField] private int totalEnemyCount = 0;
//     
//     public bool isLayerDestroyed = false;
//
//     public void Initialize(int index)
//     {
//         layerIndex = index;
//
//         enemiesInLayer = GetComponentsInChildren<Enemy>();
//         totalEnemyCount = enemiesInLayer.Length;
//
//         for (int i = 0; i < enemiesInLayer.Length; i++)
//         {
//             enemiesInLayer[i].parentLayer = this;
//         }
//         
//     }
//
//     public void OnEnemyDestroyed() //Enemy에서 호출
//     {
//         destroyedEnemyCount++;
//
//         if (destroyedEnemyCount >= totalEnemyCount)
//         {
//             isLayerDestroyed = true;
//             Debug.Log("Layer "+layerIndex+" destroyed");
//         }
//
//     }
//
//     public bool IsDestroyed()
//     {
//         return isLayerDestroyed;
//     }
//
// }

