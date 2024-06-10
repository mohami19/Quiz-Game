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
    bool hasAnsweredEarly = true ;
    
    [Header("Buttons")]
    [SerializeField] Sprite defaultAnswerSprit;
    [SerializeField] Sprite correctAnswerSprit;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar; 

    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update(){
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion){
            if(progressBar.value == progressBar.maxValue){
                isComplete = true;
                return;
            }
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
        scoreText.text = "Score : " + scoreKeeper.CalculatedScore() + "%";
    
        
    }

    void DisplayAnswer(int index){
        correctAnswer = currentQuestion.GetCorrectAnswer();
        Image ButtonImage = answerButtons[correctAnswer].GetComponent<Image>();
        if (index == correctAnswer){
            questionText.text = "correct";
            ButtonImage.sprite = correctAnswerSprit;
            scoreKeeper.IncrementCorrectAnswer();
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
            progressBar.value++;
            scoreKeeper.IncrementQuestionSeen();
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
