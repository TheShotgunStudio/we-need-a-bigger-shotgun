using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float countDown = 0.0f;
    
    public void Start(float time){
        countDown = time;
    }

    public void Tick(){
        if(countDown >= 0){
            countDown -= Time.deltaTime;
        }
    }

    public bool IsFinished(){
        return countDown <= 0;
    }
}
