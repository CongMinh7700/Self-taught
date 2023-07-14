using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    [SerializeField]
    private float gravityModifier;
    public bool isOnGround = true;
    public bool isGameOver = false;
    private float jumpForce = 600;
    private bool doubleJump = false;
    public bool isSpeedRun = false;
 
  
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
        
    }
  
    // Update is called once per frame
    void Update()
    {    
        JumpPlayer();
        isSpeed();

    }
    public void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
        else if (doubleJump == false && !isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                doubleJump = true;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
        }
    }
    public void isSpeed()
    {
        if (Input.GetKeyDown(KeyCode.S) && isSpeedRun == false)
        {
            isSpeedRun = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && isSpeedRun == true)
        {
            isSpeedRun = false;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground") && !isGameOver)
        {
            isOnGround = true;
            dirtParticle.Play();
            doubleJump = false;

        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            isGameOver = true;
            Debug.Log("Game Over !!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
    
}
