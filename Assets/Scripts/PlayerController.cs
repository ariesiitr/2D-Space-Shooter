using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public GameObject GameManagerGO;
    public float speed;
    public AudioSource audio;
    public GameObject PlayerBulletGO;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject ExplosionGO;
    public Text LivesUIText;
    const int MaxLives = 3;
    int lives;

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text= lives.ToString();
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
    }
    void Update()

    {
        if (Input.GetKeyDown("space"))
        {
            audio.Play();
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition01.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition02.transform.position;
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // Adjusting max values
        max.x -= 0.225f;
        max.y -= 0.225f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
   void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "EnemyShipTag")|| (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            if(lives==0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
              gameObject.SetActive(false);
            }

          
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}