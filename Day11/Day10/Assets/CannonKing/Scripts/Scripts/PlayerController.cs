using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject Cannon;
    [SerializeField] float _speedRotation = 30.0f;
    [SerializeField] float _speedBullet = 30;
    public Transform _spawnPos;
    public GameObject _bullet;
    private Rigidbody bulletRb;

    private float _horinzontalInput;
    private float Delay = 0;
    private float timeDelay = 1.0f; 
    public bool isShoot = false;
    //CoolDown
    public Slider cooldownBar;
    public int amountCooldown;
    private float _currentCoolDown = 0;
    public bool isCoolDown;
    //Capacitors
    public Slider capBar;
    public int amountCap;
    private float _currentCap = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletRb = GameObject.Find("Bullet").GetComponent<Rigidbody>();
        isShoot = true;
        capBar.fillRect.gameObject.SetActive(false);
        

    }
    // Update is called once per frame
    void Update()
    {
        PlayControl();
        Shoot();
        CoolDown(Time.deltaTime * (amountCooldown / timeDelay ));
    }
    public void PlayControl()
    {
        _horinzontalInput = Input.GetAxis("Horizontal");
        Cannon.transform.Rotate(Vector3.up, Time.deltaTime * _horinzontalInput * _speedRotation);

    }

    public void Shoot()
    {
    
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (Time.time > Delay)
            {
                isShoot = false;
            }
        }
        if (Input.GetKey(KeyCode.Space) && isShoot == false)
        {
            _speedBullet += 1;
            Capacity(Time.deltaTime*( amountCap/_speedBullet)*10);
            
            if (_speedBullet >= 80)
            {
                _speedBullet = 80;
               
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && isShoot == false)
        {

            SpawnBullet();
            ReCoolDown();
            ReFill();
            Delay = Time.time + timeDelay;


            
        }
    }
    public void ReCoolDown()
    {
        _speedBullet = 30;
        isShoot = true;
        capBar.fillRect.gameObject.SetActive(false);
        _currentCap = 0;
    }
    public void SpawnBullet()
    {

        GameObject cannonBall = Instantiate(_bullet, _spawnPos.position, _bullet.transform.rotation);
        bulletRb = cannonBall.GetComponent<Rigidbody>();
        bulletRb.velocity = -_speedBullet * _spawnPos.right;

    }
    public void CoolDown(float amount)
    {
        if (isShoot == true)
        {

            isCoolDown = true;

        }
        if (isCoolDown == true)
        {
            _currentCoolDown += amount;
            cooldownBar.fillRect.gameObject.SetActive(true);
            cooldownBar.value = _currentCoolDown;
        }

        


    }
    public void ReFill()
    {
        if (cooldownBar.value == cooldownBar.maxValue)
        {
            isCoolDown = false;
            cooldownBar.fillRect.gameObject.SetActive(false);
            _currentCoolDown = 0;
        }
    }
    public void Capacity(float amount)
    {

        _currentCap += amount;
        capBar.fillRect.gameObject.SetActive(true);
        capBar.value = _currentCap;



    }
   

}
