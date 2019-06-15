using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    
    public float timer;
    public LayerMask m_LayerMask;
    public Collider2D[] colls;
    public float i2;
    bool tempswitch = false;
    public GameObject expl;

    private void Start()
    {
        colls = Physics2D.OverlapCircleAll(transform.position, 1, m_LayerMask);
        StartCoroutine("explode");

    }
    private void FixedUpdate()
    {
        startBlinking();
    }

    IEnumerator explode()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        GameObject o = Instantiate(expl);
        o.transform.position = gameObject.transform.position;

        foreach (Collider2D coll in colls)
        {
            if (coll)
            {
                Destroy(coll.transform.gameObject);
            }

        }
    }

    void startBlinking()
    {
        for (int i = 0; i < 3000; i++)
        {
            if(i2 > 1)
            {
                tempswitch = true;
            }
            if (i2 < 0)
            {
                tempswitch = false;
            }

            if (!tempswitch)
            {
                i2 += 0.0001f;
            }
            else if (tempswitch)
            {
                i2 -= 0.0001f;
            }
            gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_FlashAmount", i2);
        }
       



    }
}