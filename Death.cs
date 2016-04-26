using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
    public int life;
    public int max_life;
    public bool break_=true;
    public int child_Max;
    public static int flag_false;
    public int item_drop;
    public GameObject item;
    void Awake() {
        max_life = life;
        if (this.gameObject.transform.tag=="block") {
           item_drop= Random.Range(0, 100);
            if (break_==false) {//壊れないブロック
                flag_false++;
            }
        }
        if (this.gameObject.transform.tag == "Block") {
            gameObject.name = "Block";
            child_Max = this.gameObject.transform.childCount;
        }
        if (this.gameObject.transform.tag == "prize") {
            this.gameObject.transform.name = "Prize";
        }
    }
    public int F() {
        return flag_false;
    }
	public void Break() {
        Destroy(gameObject);
    }
    public void Life_Dec() {
        life--;
    }
    public void One_Kill() {
        life = 0;
    }

    public int Drop_Rand() {
        return item_drop;
    }
    public void Instant() {
        Instantiate(item, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }
    void Update() {

        if (this.gameObject.transform.tag=="Block") {
            if (this.gameObject.transform.childCount-flag_false == 0)
            {
                flag_false = 0;
                Destroy(gameObject);
            }
        }

    }
}
