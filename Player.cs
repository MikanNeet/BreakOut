using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private Vector3 mousepos;
    private Vector3 W_mousepos;
    public float speed = 10;
    public float move = 0.3f;
    public bool direction_flag;//方向フラッグtrue:右false:左
    private bool cl_flag;
    private Vector3 initial_p;
	// Use this for initialization
	void Start () {
        this.gameObject.name = "Player";
        initial_p = this.gameObject.transform.position;
	}
	public void Init()
    {
        this.gameObject.transform.position = initial_p;
    }
	// Update is called once per frame
	void Update () {
        mousepos = Input.mousePosition;
        W_mousepos = Camera.main.ScreenToWorldPoint(mousepos);
        float p_x = gameObject.transform.position.x;

        GameObject wr = GameObject.Find("right");
        GameObject wl = GameObject.Find("left");


        if (Input.GetMouseButtonDown(0)) {
            Debug.Log(this.transform.position.x);
        }
        if (cl_flag)
        {
            if (wl.transform.position.x+8f < W_mousepos.x && W_mousepos.x < wr.transform.position.x-7.5f)
            {
                cl_flag = false;
            }
        }
        else
        {

            if (p_x != W_mousepos.x)
            {
                if (p_x > W_mousepos.x)
                {
                    direction_flag = false;
                    p_x -= move;
                }
                else if (p_x < W_mousepos.x)
                {
                    direction_flag = true;
                    p_x += move;
                }
                if (W_mousepos.x - move < p_x && p_x < W_mousepos.x + move)
                {
                    p_x += W_mousepos.x - p_x;
                }
            }
            gameObject.transform.position = new Vector3(p_x, transform.position.y, transform.position.z);

        }


    }

    void OnCollisionEnter(Collision c) {
        if (c.transform.tag=="right" || c.transform.tag=="left") {
            cl_flag = true;
            
        }
    }
    public void Death() {
        Destroy(gameObject);
    }

}
