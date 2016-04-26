using UnityEngine;
using System.Collections;

public class Ball2 : MonoBehaviour
{

    public float speed = 6;//移動速度
    private float sp;//初期速度
    public int cnt = 0;//壊した数
    Rigidbody rb;
    SphereCollider _collider;
    public bool flag_mode = false;//通常モード無敵モードの変更(false:通常 true:無敵モード)
    //景品オブジェクト
    GameObject prize;
    //ブロック最大個数
    public int Max_block;
    //壊れないブロック
    public int Dont_Break;
    //初期位置
    Vector3 initial;
    private bool game_start;
    //モード
    public byte mode=0;//0:通常,1:無敵,2:加速(今のところ加速と無敵は共存しないように)
    //アイテムの取得範囲
    private int random;
    void Start()
    {
        sp = speed;
        initial = this.gameObject.transform.position;
        //ブロック集団の合計個数
        Max_block = GameObject.Find("Block").GetComponent<Death>().child_Max;
        //壊れないブロックの個数
        Dont_Break = GameObject.Find("Block").GetComponent<Death>().F();
        //景品画像を取得
        prize = GameObject.Find("Prize");
        //フレームレート60
        Application.targetFrameRate = 60;
        //球の小ライダー取得
        _collider = GetComponent<SphereCollider>();
        //衝突判定ありに設定
        _collider.isTrigger = false;
        //自分の名前はBall固定
        this.gameObject.name = "Ball";
        FindObjectOfType<Block_Text>().Start_();

    }
    public void Move() {
        //物理演算導入
        rb = GetComponent<Rigidbody>();
        rb.constraints=RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        //ボールの押し出し
        if (GameObject.Find("Player").GetComponent<Player>().direction_flag)
        {
            rb.AddForce((transform.up + 2 * transform.right).normalized * speed, ForceMode.VelocityChange);
        }
        else {
            rb.AddForce((transform.up - 2 * transform.right).normalized * speed, ForceMode.VelocityChange);
        }
        game_start = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!game_start)
        {
            this.transform.position = new Vector3(GameObject.Find("Player").transform.position.x, this.transform.position.y, this.transform.position.z);
        }

        else
        {
            //スペースキーで加速する
            if (Input.GetButtonDown("Jump") && speed < 20)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                speed *= 2;
                rb.AddForce((transform.up + transform.right).normalized * speed, ForceMode.VelocityChange);
            }
            else if (Input.GetButtonDown("Jump") && speed >= 20)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX;
                rb.constraints = RigidbodyConstraints.FreezePositionY;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.constraints = RigidbodyConstraints.FreezePositionZ;
                speed /= 2;
                rb.AddForce((transform.up + transform.right).normalized * speed, ForceMode.VelocityChange);
            }
            //壊せるものをすべて壊したら
            if (cnt >= Max_block - Dont_Break)
            {
                FindObjectOfType<Block_Text>().No_Life();
                Destroy(gameObject);
                //FindObjectOfType<Block_Item_>().Death();
                FindObjectOfType<Player>().Death();
                FindObjectOfType<Death>().Break();
                FindObjectOfType<Block_Text>().Score(1, 0);
                FindObjectOfType<GameManager>().GameClear();

            }
            //無敵モード
            //if (Input.GetKeyDown(KeyCode.M) && flag_mode == false)
            if (mode==1 && flag_mode == false)
            {
                mode = 0;
                _collider.isTrigger = true;
                flag_mode = true;
            }
            if (flag_mode == true)
            {
                //N押したら旋回モード終了
                if (Input.GetKeyDown(KeyCode.N))
                {
                    _collider.isTrigger = false;
                    flag_mode = false;
                }

            }
        }
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag != "block" && c.transform.tag != "item")//item/block以外はトリガー領域で以下の反応をする
        {
            _collider.isTrigger = false;
            flag_mode = false;
        }
        else if(c.transform.tag == "block")
        {
            if (c.gameObject.GetComponent<Death>().break_ == true)//壊れるブロックならば　
            {
                c.gameObject.GetComponent<Death>().One_Kill();//ワンパン

                if (c.gameObject.GetComponent<Death>().life <= 0)
                {


                    random = Random.Range(0, 90);
                    //アイテムドロップ判定
                    if (random < c.gameObject.GetComponent<Death>().Drop_Rand() && c.gameObject.GetComponent<Death>().Drop_Rand() < random + 10)
                    {
                        c.gameObject.GetComponent<Death>().Instant();
                    }

                    //ぶつかったの破壊
                    Destroy(c.gameObject);
                    //壊した数
                    cnt++;
                    //スコア10点加算
                    FindObjectOfType<Block_Text>().Score(0, c.gameObject.GetComponent<Death>().max_life);
                }
            }
            else {
                _collider.isTrigger = false;
                flag_mode = false;
            }
        }

    }

    
    private void OnTriggerExit(Collider c) {
        if (c.transform.tag =="top" || c.transform.tag == "right" ||c.transform.tag == "left"||(c.transform.tag == "block" && c.gameObject.GetComponent<Death>().break_ == false))
        {
            _collider.isTrigger = true;
            flag_mode = true;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.transform.tag == "block")
        {
            if (c.gameObject.GetComponent<Death>().break_ == true)
            {
                c.gameObject.GetComponent<Death>().Life_Dec();
                if (c.gameObject.GetComponent<Death>().life <= 0)
                {

                    random = Random.Range(0, 90);
                   //アイテムドロップ判定
                    if (random < c.gameObject.GetComponent<Death>().Drop_Rand() && c.gameObject.GetComponent<Death>().Drop_Rand() < random + 10)
                    {
                        c.gameObject.GetComponent<Death>().Instant();
                    }

                    //ぶつかったの破壊
                    Destroy(c.gameObject);
                    //壊した数
                    cnt++;
                    //スコア10点加算
                    FindObjectOfType<Block_Text>().Score(0, c.gameObject.GetComponent<Death>().max_life);

                }
            }
        }

        if (c.transform.tag == "down" && FindObjectOfType<GameManager>().zanki == 1)
        {
            //ライフがもうないので壊す
            FindObjectOfType<Block_Text>().No_Life();
            Destroy(this.gameObject);
            //すべて破壊
            FindObjectOfType<Player>().Death();
            FindObjectOfType<Death>().Break();
            //FindObjectOfType<Block_Item_>().Death();
            FindObjectOfType<GameManager>().Block_Death();
            prize.GetComponent<Death>().Break();
            //cnt初期化
            cnt = 0;
            //ゲームオーバー表示
            FindObjectOfType<GameManager>().GameOver();

        }
        else if(c.transform.tag == "down" && FindObjectOfType<GameManager>().zanki != 1)
        {
            game_start = false;
            speed = sp;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            this.gameObject.transform.position = initial;
            GameObject.Find("Player").GetComponent<Player>().Init();
            FindObjectOfType<GameManager>().ReGame();
            FindObjectOfType<GameManager>().Zanki_Dec();
        }
    }

}
