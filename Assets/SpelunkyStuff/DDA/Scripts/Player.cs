using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    Rigidbody2D body;

    float horizontal;
    float vertical;
    public int currentHp;
    public float runSpeed = 10.0f;
    public Camera cam;
    //public Image uicross;
    public GameObject muzzleflash;
    bool pause = true;
    int layerMask;
    int attackdamage = 1;
    public Text playerhptex;
    public GameObject bomb;

    public bool pauseBomb;
    void Start()
    {   //cam= Camera.main;
        layerMask =~ LayerMask.GetMask("player");
        currentHp = PlayerPrefs.GetInt("CurrentHP");
        body = GetComponent<Rigidbody2D>();
        //Cursor.visible = false;
        cam= Camera.main;
    }

    void Update()
    {
        //uicross.transform.position = Input.mousePosition;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        playerhptex.text = currentHp.ToString();
        if(currentHp <=0){
            GameObject.Find("LevelManager").GetComponent<LevelManager>().Reset();
        }
        if (Input.GetKey(KeyCode.Space) && !pauseBomb)
        {
            Instantiate(bomb,gameObject.transform.position,Quaternion.identity);
            pauseBomb = true;
        }
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if(Input.GetKey(KeyCode.Mouse0) &&  pause)
        {
        StartCoroutine("pauseSpamShoot");
        pause = false;
        }
    }
    void Shoot(){
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        muzzleflash.SetActive(false);

        Vector3 screenPos = cam.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, screenPos,layerMask);

        if(hit)
        {
            if(hit.transform.gameObject.GetComponent<BasicEnemy>() != null){
                hit.transform.gameObject.GetComponent<BasicEnemy>().takeDamage(attackdamage);
            }
        }

    }
    IEnumerator pauseSpamShoot(){
        Shoot();
        muzzleflash.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        muzzleflash.SetActive(false);

        yield return new WaitForSecondsRealtime(0.5f);

        pause = true;
    }
}