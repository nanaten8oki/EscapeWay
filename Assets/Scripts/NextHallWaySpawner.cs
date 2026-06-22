using UnityEngine;

public class NextHallWaySpawner : MonoBehaviour
{
    public Transform spawnBaseTransform; //　新規生成する廊下の基準位置
    public Vector3 relativeWorldOffset = new Vector3(-42, 0f, 56f); //　基準とする廊下に対するワールドオフセット
    private bool hasSpawned = false;//　既に廊下を生成済みか真偽
    public bool isAbnormal = false; //　初期フラグを異変なしに設定

    private void OnTriggerEnter(Collider other)
    {
        if(hasSpawned) return; // 既に生成済みなら処理をしない
        if(!other.CompareTag("Player")) return; // 侵入したのがプレイヤー以外なら処理をしない

        SpawnHallway();// 廊下を生成する関数
        hasSpawned = true; //　生成済みフラグを立てる
    }

    //　次の廊下のプレファブを生成
    private void SpawnHallway()
    {
        int displayScore = 0; //  表示用初期スコア


        // ワールド座標での生成位置を基準位置からオフセット分足して算出
        Vector3 spawnPosition =
            spawnBaseTransform.position
            + spawnBaseTransform.right * relativeWorldOffset.x
            + spawnBaseTransform.up * relativeWorldOffset.y
            + spawnBaseTransform.forward * relativeWorldOffset.z;

        Quaternion spawnRotation = spawnBaseTransform.rotation; // 回転は基準オブジェクトと同じ

        GameObject newHallway = Instantiate(selectedPrefab, spawnPosition, spawnRotation); //　選択したオブジェクトをインスタンス化
    }


}
