using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayerBomb : MonoBehaviour {

   public Sprite[] bomb;

    // Update is called once per frame
    private void Start()
    {
        
        gameObject.GetComponent<SpriteRenderer>().sprite = bomb[0];
        StartCoroutine("explode");
    }
    IEnumerator explode()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = bomb[1];
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = bomb[2];
        Destroy(gameObject);
    }
}
