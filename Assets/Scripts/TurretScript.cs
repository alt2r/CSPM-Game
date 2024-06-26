using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    // Start is called before the first frame update
    float momentum = 0;
    [SerializeField]
    GameObject bulletGO;


    float fireRate = 3;
    float shootCountdown = 0; //counts down to 0
    void Start()
    {
        //StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if(shootCountdown > 0)   //turret shoot cooldown
        shootCountdown -= Time.deltaTime;


        //movement
        if(Input.GetKey(KeyCode.DownArrow))
        {
            if(momentum > -15)
            momentum -= 5f * Time.deltaTime;
            else
            momentum = -10;
            if(momentum > 3.5f * Time.deltaTime)
            momentum -= 3.5f * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            if(momentum < 15)
            momentum += 5f * Time.deltaTime;
            else
            momentum = 10;
            if(momentum < -3.5f * Time.deltaTime) //friction + movement
            momentum += 3.5f * Time.deltaTime;
            
        }
        else
        {
            if(momentum > 3.5f * Time.deltaTime)
            momentum -= 3.5f * Time.deltaTime; //friciton
            else if(momentum < -3.5f * Time.deltaTime)
            momentum += 3.5f * Time.deltaTime;
            else
            momentum = 0;
        }

        if(!(momentum > 0 && transform.position.y > 4.5f) && !(momentum < 0 && transform.position.y < -4.5f))
        {
            transform.Translate(new Vector2(0, momentum * Time.deltaTime));
        }
        else
        {
            if(transform.position.y > 4.5f)
            {
                transform.position = new Vector2(transform.position.x, 4.5f);
            }
            else if(transform.position.y < -4.5f)
            {
                transform.position = new Vector2(transform.position.x, -4.5f);
            }
            
            momentum = 0;
        }


        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        return;
        
    }


    //IEnumerator Shoot()
    void Shoot()
    {
        Player player = Player.instance;
        
        if(shootCountdown <= 0)
        {
            shootCountdown = 1 / fireRate;
            GameObject thisBullet = Instantiate(bulletGO);
            thisBullet.transform.position = transform.position;
        }
        return;

    }
}
