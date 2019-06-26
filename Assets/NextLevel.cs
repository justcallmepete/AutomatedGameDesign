using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour {
	GameObject go;
	// Use this for initialization
	void Start () {
		go = GameObject.Find("LevelManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 private void OnCollisionEnter2D(Collision2D other) {
		 
	 Debug.Log("oha");
		if(other.gameObject.tag == "Player"){
			go.GetComponent<LevelManager>().GenerateNewLevel();
		}
	}
}
