using UnityEngine;
using System.Collections;

public class block_text_Mesh : MonoBehaviour {
    public TextMesh stage;
    public int number;
    // Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        number = FindObjectOfType<GameManager>().Stage_Number()+1;
        stage.text = "Stage" + number;
    }
}
