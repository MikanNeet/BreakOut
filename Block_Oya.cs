using UnityEngine;
using System.Collections;

public class Block_Oya : MonoBehaviour {   

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.transform.childCount==0) {
            Destroy(gameObject);
        }
	}

    
}
