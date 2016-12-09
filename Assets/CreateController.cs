using UnityEngine;
using System.Collections;

public class CreateController : MonoBehaviour {

    private GameObject unitychan;                           // Unityちゃんのオブジェクト
    private float create;                                   // 壁とユニティちゃんとの差の入れる
    public GameObject carPrefab;                            // carPrefobを入れる
    public GameObject coinPrefab;                           // coinPrefabを入れる
    public GameObject conePrefab;                           // cornPrefabを入れる
    private float posRange = 3.4f;                          // アイテムを出すx方向の範囲
    private float goalpos;                                  // アイテム生成50f先地点
    private float span = 3.0f;                              // 生成時間を設定
    private float delta = 2.0f;                             // タイマー用の数値
    private float endPos = 100.0f;                          // アイテム生成の終了地点

    // Use this for initialization
    void Start () {
        // Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");

        // Unityちゃんと壁の差を求める
        this.create = unitychan.transform.position.z - this.transform.position.z;


    }

    // Update is called once per frame
    void Update () {
        // Unityちゃんの位置に合わせて壁を移動
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - create);

        // 壁から50.0f先までアイテムを生成する
        goalpos = transform.position.z + 50.0f;

        // アイテム生成のための時間カウント
        this.delta += Time.deltaTime;

        // アイテム生成の最終地点までの場合はアイテムを生成する
        if (endPos > transform.position.z) {
            // 一定の時間が経過したらアイテムを生成する
            if (this.delta > this.span) {

                // アイテム生成の間隔
                for (float i = transform.position.z; i < goalpos; i += 15) {
                    // どのアイテムを出すのかをランダムに設定
                    int num = Random.Range(0, 10);
                    if (num <= 1) {
                        // コーンをx軸方向に一直線に生成
                        for (float j = -1; j <= 1; j += 0.4f) {
                            GameObject cone = Instantiate(conePrefab) as GameObject;
                            cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                            this.delta = 0.0f;
                        }
                    } else {
                        // レーンごとにアイテムを生成
                        for (int j = -1; j < 2; j++) {
                            // アイテムの種類を決める
                            int item = Random.Range(1, 11);
                            // アイテムを置くz座標のオフセットをランダムに設定
                            int offsetZ = Random.Range(-5, 6);
                            // 60%コイン配置:30%車配置:10%何もなし
                            if (1 <= item && item <= 6) {
                                // コインを生成
                                GameObject coin = Instantiate(coinPrefab) as GameObject;
                                coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                                this.delta = 0.0f;
                            } else if (7 <= item && item <= 9) {
                                // 車を生成
                                GameObject car = Instantiate(carPrefab) as GameObject;
                                car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                                this.delta = 0.0f;
                            }
                        }
                    }
                }
            }
        }

    }
}
