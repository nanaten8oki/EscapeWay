using UnityEngine;
using System.Collections.Generic;

public class HallwayManager : MonoBehaviour
{
 public static HallwayManager Instance; // シングルドン  （他クラスで参照可能な静的メンバ変数）

 public GameObject currentHallway; //　現在プレイヤーのいる廊下
 public GameObject lastestHallway;  //前回生成の廊下
 public GameObject generatedHallway; //前々回の廊下

 public int currentScore = 0; // 現在の廊下番号スコア
 public List<GameObject> hallwayPrefabs = new List<GameObject>(); //　異変のある廊下のプレハブリスト
 public GameObject noAbnormalHallwayPrefab; // 異変なしの廊下プレハブ

// 出現確率をインスペクター表示
 [Range(0f, 1f)]
 public float noAbnormalHallwayPrefabWeight = 1.0f; //　異変なし廊下の出現確率
 private List<GameObject> usedPrefabs = new List<GameObject>(); // 使用済みプレハブリスト

    public GameObject goalHallwayPrefab; // ゴール用の廊下
 public int goalScoreThreshold = 3;  //　ゴール出現スコアの番号

    //　スタートメソッドより早い初期化処理
    private void Awake()
    {
        if(Instance == null) { Instance = this; } // 初回（null時）生成インスタンスとして登録
        else { Destroy(gameObject); } //　既に他のインスタンスが存在する際、この重複インスタンスは破棄
    }

    // 次に生成する廊下プレハブを返す（出現済みを除外し、異変なしを一定確率で混在）
    public GameObject GetNextPrefab()
    {
        // スコア０の際は異変なし廊下を返して処理を終了
        if (currentScore == 0)
        {
            return noAbnormalHallwayPrefab;
        }
        //　ゴールスコアに達していたらゴール廊下を返して処理を終了
        if (currentScore >= goalScoreThreshold)
        {
            return goalHallwayPrefab;
        }
        //　未使用の廊下プレハブを使用可能廊下リストに追加
        List<GameObject> availablePrefabs = new List<GameObject>();
        foreach (GameObject prefab in hallwayPrefabs)
        {
            if (!usedPrefabs.Contains(prefab))
            {
                availablePrefabs.Add(prefab);
            }
        }

        //　廊下が全て使用済みならリストを初期化して使用可能
        if (availablePrefabs.Count == 0)
        {
            usedPrefabs.Clear();
            availablePrefabs.AddRange(hallwayPrefabs);
        }

        //0.0以上1.0以下の指定確率で異変なし廊下を使用するかの判定
        bool noAbnormal = Random.value <= noAbnormalHallwayPrefabWeight;
        Debug.Log($"[GetNextPrefab]異変なし廊下使用：{noAbnormal}, 異変なし廊下出現確率Weight：{noAbnormalHallwayPrefabWeight}");//　デバッグ確認

        // 指定確率が起こった際に、異変なし廊下を返して処理を終了
        if (noAbnormal)
        {
            return noAbnormalHallwayPrefab;
        }

        // 使用可能な廊下リストの中からランダムに一つ選ぶ
        GameObject selectedHallway =availablePrefabs[Random.Range(0, availablePrefabs.Count)];

        usedPrefabs.Add(selectedHallway); //　選んだ廊下プレハブを使用済みリストに追加
        return selectedHallway;//　選んだ廊下プレハブを返す

    }


}
