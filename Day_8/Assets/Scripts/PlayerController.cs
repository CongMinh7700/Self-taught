using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerUp = false;
    public float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    //
    public GameObject bulletPrefab;
    public PowerUpType currentPowerup = PowerUpType.None;
    private GameObject tmpBullet;
    private Coroutine powerupCountDown;
    //Smash
    private float hangTime = 1 ;
    private float smashSpeed =10;
    private float explosionForce =50;
    private float explosionRadius =50;
    bool smashing = false;
    float floorY;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
       
     
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        if(currentPowerup == PowerUpType.Bullet && Input.GetKeyDown(KeyCode.F))
        {
            LaunchBullet();
        }
        if (currentPowerup == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && smashing== false)
        {
            smashing = true;
            StartCoroutine(Smash());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power Up"))
        {
            hasPowerUp = true;
            currentPowerup = other.gameObject.GetComponent<PowerUp>().powerUpType;              
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            if(powerupCountDown!= null)
            {
                StopCoroutine(powerupCountDown);
            }
            powerupCountDown = StartCoroutine(PowerupCountdownRoutine());
        }
        
    }
    IEnumerator PowerupCountdownRoutine()
    {
      
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        currentPowerup = PowerUpType.None;
        powerupIndicator.gameObject.SetActive(false);
    }
    IEnumerator Smash()
    {
        var enemy = FindObjectsOfType<EnemyController>();
        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        while(transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x,-smashSpeed*2);
            yield return null;
        }
        for (int i = 0; i < enemy.Length; i++)
        {
            if (enemy[i] != null)
            {
                enemy[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
            smashing = false;
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && currentPowerup == PowerUpType.Pushback)
        {
            Rigidbody enemyRigidB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
          
            enemyRigidB.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + "with powerup set " + currentPowerup.ToString());
        }
    }
    void LaunchBullet()
    {
        foreach(var enemy in FindObjectsOfType<EnemyController>())
        {
            tmpBullet = Instantiate(bulletPrefab,transform.position+Vector3.up,Quaternion.identity);
            tmpBullet.GetComponent<Shoot>().Fire(enemy.transform);
        }
    }
  
  
}
