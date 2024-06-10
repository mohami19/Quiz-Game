using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswer;
    [SerializeField] Sprite defaultAnswerSprit;
    [SerializeField] Sprite correctAnswerSprit;
    void Start()
    {
        DisplayQuestion();
    }

    public void onAnswerSelected(int index){
        correctAnswer = question.GetCorrectAnswer();
        Image ButtonImage = answerButtons[correctAnswer].GetComponent<Image>();
        if (index == correctAnswer){
            questionText.text = "correct";
            ButtonImage.sprite = correctAnswerSprit;
        } else {
            ButtonImage.sprite = correctAnswerSprit;
            // string correctAnswerText = question.GetAnswers(correctAnswer);
            questionText.text = "The Correct Answer Has Been Highlighted";
        }
    }

    void DisplayQuestion(){
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswers(i);
        }
    }

}
