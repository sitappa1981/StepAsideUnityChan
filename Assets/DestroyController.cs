using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyController : MonoBehaviour {

    private GameObject unitychan;
    private float destroy;

    // Use this for initialization
    void Start () {
        // Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");

        // Unityちゃんと削除する位置(z座標)の差を求める
        this.destroy = unitychan.transform.position.z - this.transform.position.z;
    }

    // Update is called once per frame
    void Update () {
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - destroy);
    }

    // トリガーモードで接触した障害物を破棄
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "CarTag" || 
            other.gameObject.tag == "TrafficConeTag" || 
            other.gameObject.tag == "CoinTag") {
            Destroy(other.gameObject);
        }
    }
}
