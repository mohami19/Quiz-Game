
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")]
public class QuestionSO : ScriptableObject {
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";

    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer;
    public string GetQuestion(){
        return question;
    }

    public string GetAnswers(int index){
        return answers[index];
    }
    public int GetCorrectAnswer(){
        return correctAnswer;
    }
}
