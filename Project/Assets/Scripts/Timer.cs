using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float CountDown = 0.0f;
    
    public void Start(float time){
        CountDown = time;
    }

    public void Tick(){
        if(CountDown >= 0){
            CountDown -= Time.deltaTime;
        }
    }

    public bool IsFinished(){
        return CountDown <= 0;
    }
}
