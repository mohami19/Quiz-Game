using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] float timerToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public float fillFraction;

    public bool isAnswering ;
    float timerValue;



    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer(){
        timerValue = 0;
    }

    void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if (isAnswering) {
            if (timerValue > 0) {
                fillFraction = timerValue / timerToCompleteQuestion;
            } else {
                isAnswering = false;
                timerValue = timeToShowCorrectAnswer;
            }
        } else {
            if (timerValue > 0) {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            } else {
                isAnswering = true;
                loadNextQuestion = true;
                timerValue = timerToCompleteQuestion;
            }
        }

    }
}
