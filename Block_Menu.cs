using UnityEngine;
using System.Collections;

public class Block_Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Click_Start()
    {
        Application.LoadLevel("Stage1");
    }
    public void Click_Start2() {
        Application.LoadLevel("Typing");
    }
    public void Clivk_Back()
    {
        Application.LoadLevel("Main");
    }
}
