using UnityEngine;
using System.Collections;

public class Block_Item_ : MonoBehaviour {

    private void OnTriggerEnter(Collider c) {
        if (c.transform.tag == "Player")
        {
            Debug.Log("Get");
            if (GameObject.Find("Ball").GetComponent<Ball2>().flag_mode==false) {
                GameObject.Find("Ball").GetComponent<Ball2>().mode = 1;
            }
            Destroy(this.gameObject);
        }
        else if(c.transform.tag == "down")
        {
            Destroy(this.gameObject);
        }
    }

    public void Death() {
        Destroy(this.gameObject);
    }
}
