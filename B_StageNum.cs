
/*このスクリプトシート付けた空オブジェクトはゲーム画面においてください
すべてのゲーム終了時またはゲームオーバー時にリザルト画面でポイント移行後破壊されます。

以下のスクリプトを自分の得点を取得している位置に置き換えて使ってください。
           //運搬用に移行(すえろー用)
            B_StageNum stage_score;//B_StageNumのインスタンス作成
            stage_score = GameObject.Find("DeriverPoint").GetComponent<B_StageNum>();//コンポーネント取得
            stage_score.score[ステージナンバー] = FindObjectOfType<Block_Text>().getScore() / 割る数 - stage_score.since_score_;//各ステージの取得金
            stage_score.since_score_ += stage_score.score[ステージナンバー];//総取得金に加算

*/
using UnityEngine;
using System.Collections;

public class B_StageNum : MonoBehaviour {
    /*ステージナンバー（Update文にステージナンバーを入れるところ作ってあるからコメントアウトのところいじって使う事
    Stage1は存在しないだろうからコメントアウトしていい。あくまでいまは確認用）*/
    public int stagenum_;

    //確認のためにpublic
    public int[] score = new int[5];//とりあえず5ステージ分

    //前ステージまでの総取得金
    public int since_score_ = 0;

    //ゲームの名前（ゲームによりアクション動作及び次の遷移ページを分岐するため）
    public string game_name;
    // Use this for initialization
    void Awake() {
        if (Application.loadedLevelName == "Stage1")
        {
            game_name = "block";
        }
        else if (Application.loadedLevelName == "Typing")
        {
            game_name = "typing";
        }
        //ゲームの識別名の設定
        /*
         if (Application.loadedLevelName == "")
        {
            game_name=
        }
        */
    }


    void Start () {
        DontDestroyOnLoad(this);
        //名前を固定
        this.name = "DeriverPoint";
    }


    public void Death() {
        Destroy(this.gameObject);
    }

    //1ずつ減少用
    public void Dec(int dec)
    {
        StartCoroutine(Cor(dec));
    }

    IEnumerator Cor(int point) {//減算
        for (int i = 0; i < point; i++)
        {
            since_score_--;
            yield return new WaitForSeconds(0.1f / point);
        }
    }
    // Update is called once per frame
    void Update () {
        //シルフィン
        if (Application.loadedLevelName == "Stage1")
        {
            stagenum_ = FindObjectOfType<GameManager>().num + 1;
        }
        //海老菜
        if (Application.loadedLevelName == "Typing")
        {
            stagenum_ = FindObjectOfType<TypeObject>().stagenum;
        }
        //うまる/きりえ
        /*
        if(Application.loadedLevelName=="シーン名"){
            //stagenum =そのシーンのステージナンバー
        }
        */

    }
}
