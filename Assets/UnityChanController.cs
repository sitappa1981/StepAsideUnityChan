using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

    private Animator myAnimator;                        // アニメーションするためのコンポーネントを入れる
    private AnimatorStateInfo animState;                // アニメの状態の情報を入れる
    private Rigidbody myRigidbody;                      // Unityちゃんを移動させるコンポーネントを入れる
    private float forwardForce = 800.0f;                // 前進する為の力
    private float turnForce = 500.0f;                   // 左右に移動する為の力
    private float movebleRange = 3.4f;                  // 左右の移動できる範囲
    private float upForce = 500.0f;                     // ジャンプするための力
    private float coefficient = 0.95f;                  // 動きを減速させる係数
    private bool isEnd = false;                         // ゲーム終了の判定
    private GameObject stateText;                       // ゲーム終了時にテキストを表示する
    private GameObject scoreText;                       // スコアを表示するテキスト
    private int score = 0;                              // 得点
    private bool isLButtonDown = false;                 // 左ボタン押下の判定
    private bool isRButtonDown = false;                 // 右ボタン押下の判定

	// Use this for initialization
	void Start () {
        // Animaterコンポーネント取得
        this.myAnimator = GetComponent<Animator>();

        // 走るアニメーション開始
        this.myAnimator.SetFloat ("Speed", 1);

        // Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        // シーン中のstateTextオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");

        // シーン中のscoreTextオブジェクトを取得
        this.scoreText = GameObject.Find("ScoreText");

	}
	
	// Update is called once per frame
	void Update () {
        // ゲーム終了ならUnityちゃんの動きを減衰する
        if (this.isEnd) {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        // Unityちゃんに前方向の力を加える
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        // Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる
        if ((Input.GetKey (KeyCode.LeftArrow) || this.isLButtonDown) && -this.movebleRange < this.transform.position.x) {
            //左に移動
            this.myRigidbody.AddForce( -this.turnForce, 0 ,0);
        } else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movebleRange) {
            // 右に移動
            this.myRigidbody.AddForce(this.turnForce, 0 ,0);
        }

        // Junpステートの場合はJunpにfolseをセットする
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump")) {
            this.myAnimator.SetBool("Jump", false);
        }

        // ジャンプしていない時にスペースが押された場合、ジャンプする
        if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f) {
            // ジャンプアニメを再生する
            this.myAnimator.SetBool("Jump", true);
            // Unityちゃんに上方向の力を加える
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }

	}

    // トリガーモードで他のオブジェクトと接触した場合の処理
    private void OnTriggerEnter(Collider other) {

        // 障害物に衝突した場合
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag") {
            this.isEnd = true;
            // stateTextにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        // ゴール地点に到着した場合
        if (other.gameObject.tag == "GoalTag") {
            this.isEnd = true;
            // stateTextにGAME CLEARを表示
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        // コインに衝突した場合
        if (other.gameObject.tag =="CoinTag") {
            // スコアを加算
            this.score += 10;
            // ScoreText獲得した点数を表示
            this.scoreText.GetComponent<Text>().text = "Score " + this.score + "pt";
            // パーティクルを再生
            GetComponent<ParticleSystem>().Play();
            // 接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    // ジャンプボタンを押した場合の処理
    public void GetMyJunpButtonDown() {
        if (this.transform.position.y < 0.5f) {
            this.myAnimator.SetBool("Jump",true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    // 左ボタンを押し続けた場合の処理
    public void GetMyLeftBottonDown() {
        this.isLButtonDown = true;
    }

    // 左ボタンを離した場合の処理
    public void GetMyLeftBootonUp() {
        this.isLButtonDown = false;
    }

    // 右ボタンを押し続けた場合の処理
    public void GetMyRigthButtonDouwn() {
        this.isRButtonDown = true;
    }

    // 右ボタンを離した場合の処理
    public void GetMyRigthButtonUp() {
        this.isRButtonDown = false;
    }


}
