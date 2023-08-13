using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _direction;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;
    private int spriteIndex;
    [SerializeField] private float _gravity = 9.8f;
    [SerializeField] private float _strength = 5f;
    private GameManger gameManager;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManger>();
    }
    private void Start()
    {
        InvokeRepeating("AnimateSprite", 0.15f, 0.15f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            _direction = Vector3.up * _strength;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _direction = Vector3.up * _strength;
            }
        }
        _direction.y += Time.deltaTime * -_gravity;
        transform.position += _direction * Time.deltaTime;
    }
    public void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;

        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.GameOver();
        }
        else if (collision.gameObject.CompareTag("Scoring"))
        {
            gameManager.UpdateScore();
        }
    }
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        _direction = Vector3.zero;
    }
}
