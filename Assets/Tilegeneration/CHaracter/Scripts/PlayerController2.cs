using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{


    Animator animator;
    Rigidbody2D body2d;

    public int speed = 1;

    float moveForce = 4800f;

    [SerializeField]
    GameObject bombPrefab;

    bool placedBomb = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SmoothFollowCam>().enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 walk = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isWalking = walk.magnitude > 0;

        if (isWalking)
        {
            body2d.AddForce(walk.normalized * (moveForce + speed * 500f), ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.Space) && !placedBomb)
        {
            StartCoroutine("PlaceBomb");

        }

        animator.speed = isWalking ? 1 : 0;

        if (isWalking)
        {
            animator.SetInteger("direction", VecDeg.VecToDegree(walk, 8));
        }
    }
    IEnumerator PlaceBomb() {
        placedBomb = true;
        GameObject b = Instantiate(bombPrefab);
        b.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(3f);
        placedBomb = false;
    }
        
   
}
