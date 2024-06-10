using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
public class EndScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper ScoreKeeper;
    void Awake() {
        ScoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore(){
        finalScoreText.text = "Congratulations!\n Your Score : " + 
                                    ScoreKeeper.CalculatedScore() + "%";
    }
}
