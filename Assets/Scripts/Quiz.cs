using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
     QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer;
    bool hasAnsweredEarly;
    
    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprit;
    [SerializeField] Sprite correctAnswerSprit;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion){
            hasAnsweredEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        } else if(!hasAnsweredEarly && !timer.isAnswering){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void onAnswerSelected(int index){
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    void DisplayAnswer(int index){
        correctAnswer = currentQuestion.GetCorrectAnswer();
        Image ButtonImage = answerButtons[correctAnswer].GetComponent<Image>();
        if (index == correctAnswer){
            questionText.text = "correct";
            ButtonImage.sprite = correctAnswerSprit;
        } else {
            ButtonImage.sprite = correctAnswerSprit;
            string correctAnswerText = currentQuestion.GetAnswers(correctAnswer);
            questionText.text = "The Correct Answer is : \n" + correctAnswerText;
        }
    }
    void getNextQuestion(){
        if (questions.Count > 0) {
            SetButtonState(true);
            SetDefaultButtonSprit();
            GetRandomQuestion();
            DisplayQuestion();
        }
    }

    void GetRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }

    }

    void DisplayQuestion(){
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswers(i);
        }
    }

    void SetButtonState(bool state){
        for (int i = 0; i < answerButtons.Length; i++){
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprit(){
        for (int i = 0; i < answerButtons.Length; i++){
            Image ButtonImage = answerButtons[i].GetComponent<Image>();
            ButtonImage.sprite = defaultAnswerSprit;
        }

    }

}
