using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    //同時出し==========================
    public GameObject player;

    public GameObject ball;

    public GameObject block;

    public GameObject block1;

    public GameObject prize;

    public GameObject prize1;
    //獲得ポイント
    public GameObject point;
    //===================================
    //今のゲームのステージナンバー
    public int num = 0;

    private GameObject stage;
    
    private GameObject title;

    private GameObject gameover;

    private GameObject gameclear;

    private GameObject life;

    private bool start_f;

    public int zanki = 3;

    //景品オブジェクト
    GameObject prize_;
    // Use this for initialization
    void Start() {
        title = GameObject.Find("B_StartText");
        title.SetActive(true);
        stage = GameObject.Find("B_StageText");
        stage.SetActive(true);
        gameover = GameObject.Find("B_GameOver");
        gameover.SetActive(false);
        gameclear = GameObject.Find("B_GameClear");
        gameclear.SetActive(false);
        life = GameObject.Find("LIFE");
        life.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Title_Appear() == true) {
            if (!start_f) {
                //オブジェクト化したか
                start_f = true;
                GameStart();
            }
            if (Input.GetMouseButtonDown(0)) {
                Go();
                }
        }
        if (GameClear_Appear()== true&& Input.GetKeyDown(KeyCode.X)) {
            if (num != 2)
            {
                title.SetActive(true);
                stage.SetActive(true);
                gameclear.SetActive(false);
                prize_.GetComponent<Death>().Break();
            }
            else {
                Application.LoadLevel("Game_Result");
            }
        }
        
        if (GameOver_Appear() == true&& Input.GetMouseButtonDown(0))
        {

                Application.LoadLevel("Game_Result");
            
        }
        
    }
    //残機ゲッター
    public int Zanki_() {
        return zanki;
    }
    //残機減少
    public void Zanki_Dec() {
        zanki--;
    }
    //ボール移動開始
    void Go() {
        GameObject.Find("Ball").GetComponent<Ball2>().Move();
        title.SetActive(false);
        stage.SetActive(false);
        life.SetActive(true);
    }
    //リスタート
    public void ReGame() {
        title.SetActive(true);
    }
    void GameStart()
    {
        Instantiate(player, player.transform.position, player.transform.rotation);
        Instantiate(ball, ball.transform.position, ball.transform.rotation);

        if (num == 0) {//ステージ1の画像
            Instantiate(prize, prize.transform.position, prize.transform.rotation);
            Instantiate(block, block.transform.position, block.transform.rotation);
        }
        else if (num==1)
        {//ステージ2の画像
            Instantiate(prize1, prize1.transform.position, prize1.transform.rotation);
            Instantiate(block1, block1.transform.position, block1.transform.rotation);
        }
        //景品画像を取得
        prize_ = GameObject.Find("Prize");
    }
    //ゲームオーバー
    public void GameOver()
    {
        gameover.SetActive(true);
        start_f = false;
        zanki = 3;
        //ゲームオーバー時にポイント還元
        //GameObject.Find("money").GetComponent<Umr_Coin>().Add(FindObjectOfType<Block_Text>().getScore()/10);
        //運搬用に移行
        B_StageNum stage_score;
        stage_score = GameObject.Find("DeriverPoint").GetComponent<B_StageNum>();
        stage_score.score[num] = FindObjectOfType<Block_Text>().getScore() / 10 - stage_score.since_score_;
        stage_score.since_score_ += stage_score.score[num];
        FindObjectOfType<Block_Text>().Reset_Score();
    }

    //ゲームクリア
    public void GameClear()
    {
        start_f = false;
        gameclear.SetActive(true);

        //最終ゲームでなければ
        if (num != 2)
        {
            //運搬用に移行
            B_StageNum stage_score;
            stage_score = GameObject.Find("DeriverPoint").GetComponent<B_StageNum>();
            stage_score.score[num] = FindObjectOfType<Block_Text>().getScore() / 10 - stage_score.since_score_;
            stage_score.since_score_ += stage_score.score[num];
            num++;

        }
    }

    public void Block_Death() {
        GameObject[] blo = GameObject.FindGameObjectsWithTag("block");

        for (int i = 0; i < blo.Length; i++) {
            Destroy(blo[i].gameObject);
        }
    }
    public bool Title_Appear() {
        return title.activeSelf == true;
    }
    public bool GameOver_Appear() {
        return gameover.activeSelf == true;
    }
    public bool GameClear_Appear()
    {
        return gameclear.activeSelf == true;
    }
    public int Stage_Number() {
        return num;
    }
}
