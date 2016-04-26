using UnityEngine;
using System.Collections;

public class B_Ball_rotate : MonoBehaviour {
    public float speed = 4f;
    GameObject obj;
    Vector3 axis;

    Rigidbody rb;
    // Use this for initialization
    void Start () {
        obj = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
