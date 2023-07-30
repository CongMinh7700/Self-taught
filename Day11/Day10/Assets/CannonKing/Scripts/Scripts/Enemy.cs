using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private GameObject _player;
    private GameManager _gameManager;
    public float speed = 30.0f;
    public int pointValue;
    public ParticleSystem exploisionParticle;
    //Health Bar
    public Slider healthBar;
    public int amountToDeath;
    private int _curentHealAmount = 0;
 
    // Start is called before the first frame update
    void Start()
    {      
        _player = GameObject.Find("Player");
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();        
        healthBar.maxValue = amountToDeath;
        healthBar.value = 0;
        healthBar.fillRect.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();

    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
           
            Debug.Log("Destroy Enemy");
            gameObject.GetComponent<Enemy>().DamageEnemy(1);
            Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Debug.Log("Destroy Enemy");
            _gameManager.UpdateLive(-1);
            Instantiate(exploisionParticle, transform.position, exploisionParticle.transform.rotation);
        }
    }

    public void EnemyMove()
    {
        Vector3 lookAt = _player.transform.position;
        transform.LookAt(lookAt);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    public void DamageEnemy(int amount)
    {
        _curentHealAmount += amount;
        healthBar.fillRect.gameObject.SetActive(true);
        healthBar.value = _curentHealAmount;
        if (_curentHealAmount >= amountToDeath)
        {
            _gameManager.UpdateScore(pointValue);
            Destroy(gameObject, 0.1f);
            Instantiate(exploisionParticle, transform.position, exploisionParticle.transform.rotation);
        }

    }

}
