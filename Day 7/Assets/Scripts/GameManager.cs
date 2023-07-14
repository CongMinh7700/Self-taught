using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public  Transform startingPoint;
    public float lerpSpeed;
    private PlayerController playerControllerScripts;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScripts = GameObject.Find("Player").GetComponent<PlayerController>();
        playerControllerScripts.isGameOver = true;
        StartCoroutine(PlayIntro());
    }

    public IEnumerator PlayIntro()
    {
        //Lấy vị trí đầu, cuối
        Vector3 startPos = playerControllerScripts.transform.position;
        Vector3 endPos = startingPoint.position;
        //lấy khoảng cách
        float journeyLength = Vector3.Distance(startPos, endPos);
        //tg bắt đầu
        float startTime = Time.time;
        //khoảng thời gian để thực hiện animation tght-tgbd
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;
        playerControllerScripts.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);
        while(fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            //dịch chuyển từ từ tăng dần theo delta time
            playerControllerScripts.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            //tạm dừng 
            yield return null;

        }
        playerControllerScripts.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
        playerControllerScripts.isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        AddScore();
    }
    public void AddScore()
    {
        if (!playerControllerScripts.isGameOver)
        {
            if (playerControllerScripts.isSpeedRun == true)
            {
                score += 2;
            
            }
            else if(playerControllerScripts.isSpeedRun == false)
            {
                score ++;
               
            }
            Debug.Log("Score :" + score /100);
        }
        
        
      
    }
}
