using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public Transform player;
    public float detectRange = 5; 
    public bool inRange = false;
    public float moveSpeed = 2f; 
    Rigidbody2D rb; 
    int attackDelay = 1;
    bool atcCD;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        detectRange *= detectRange;
        BalanceSpawnRate(chanceOfSpawning);
    }

    void Update()
    {
        if(HP == 0){
            Destroy(gameObject);
        }
        transform.right = player.position - transform.position;
        float distsqr = (player.position - transform.position).sqrMagnitude;

        if (distsqr <= detectRange)
        {
            inRange = true;
            Vector2 velocity = (player.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = velocity;
        }
        else rb.velocity = Vector2.zero;
    }
    private void OnCollisionStay2D(Collision2D other) {

        if(other.gameObject.tag == "Player" && !atcCD){
        StartCoroutine(attacc(other));
        atcCD = true;
        }
    }

IEnumerator attacc(Collision2D c)
{
c.gameObject.GetComponent<Player>().currentHp-= (int)damage;
yield return new WaitForSecondsRealtime(attackDelay);
atcCD = false;
}

public void takeDamage(int playerAttackPower){
    HP -=playerAttackPower;
    StartCoroutine(verycoolattackfeedback());
  
}
IEnumerator verycoolattackfeedback(){
    GetComponent<SpriteRenderer>().color = Color.red;
      yield return new WaitForSecondsRealtime(0.1f);
    GetComponent<SpriteRenderer>().color = Color.white;

}
}
    