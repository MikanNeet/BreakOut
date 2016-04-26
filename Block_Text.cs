using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Block_Text : MonoBehaviour {
    //スコア表示
    public Text text1;
    //スコア
    private int score = 0;
    //ハイスコア表示
    public Text HighScore;
    //ハイスコア
    private int highscore;
    //ハイスコアキー
    private string key = "HIGH SCORE1";

    //自機ライフ
    private int life;
    //ライフ表示
    public Text Life;

    //主人公取得したかのフラッグ
    public bool getflag;




    public Text stage;
	// Use this for initialization
	void Start () {
        //保存してるハイスコアをキーで呼び出し取得
        //なければ0が返す
        highscore = PlayerPrefs.GetInt(key,0);
        HighScore.text = "HighScore" + highscore;
	}

    public void Score(int i,int j) {
        switch (i)
        {
            case 0://破壊点
                score += 10*j;
                text1.text = "Score: " + score;
                break;
            case 1://ボーナス
                score += 1000;
                text1.text = "Score: " + score;
                break;
        }
    }

    public void Start_() {
        life = FindObjectOfType<GameManager>().zanki;
        getflag = true;
    }
    public void Reset_HighScore() {
        highscore = 0;
        PlayerPrefs.SetInt(key, highscore);
        HighScore.text = "HighScore" + highscore;
    }
    public int getScore() {
        return score;
    }
    //スコアのリセット
    public void Reset_Score() {
        score = 0;
        text1.text = "Score: " + score;
    }
    //ライフがなくなったとき
    public void No_Life() {
        getflag = false;
    }
	// Update is called once per frame
	void Update () {

        stage.text = "Stage:" + (FindObjectOfType<GameManager>().Stage_Number() + 1);
        //スコア
        text1.text = "Score: "+score;
        //ハイスコア更新
        if (score > highscore) {
            highscore = score;

            PlayerPrefs.SetInt(key,highscore);
            HighScore.text = "HighScore" + highscore;

        }

        if (getflag) {
            //現在のライフを動的に取得
            life = FindObjectOfType<GameManager>().zanki;
            //自機ライフ
            switch (life)
            {
                case 1:
                    Life.text = "Life:1";
                    break;
                case 2:
                    Life.text = "Life:2";
                    break;
                case 3:
                    Life.text = "Life:3";
                    break;
            }
        }
    }
}
