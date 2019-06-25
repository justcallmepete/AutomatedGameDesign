using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player"){
			other.gameObject.GetComponent<Player>().runSpeed = 2;
		}
 }
 private void OnCollisionExit2D(Collision2D other) {
	  
		if(other.gameObject.tag == "Player"){
			StartCoroutine(backToNormalSpeed(other));
		}
 }
 IEnumerator backToNormalSpeed(Collision2D o){
	 yield return new WaitForSecondsRealtime(2);
	 o.gameObject.GetComponent<Player>().runSpeed = 4;
 }
}

