using UnityEngine;
using TMPro;

public class ScoresController : MonoBehaviour
{
    public static ScoresController Instance;

    [SerializeField] private TextMeshProUGUI scoresText;

    private int scores;

    private void Start()
    {
        Instance = this;
        scores = 0;
    }

    public void AddScores(int amount)
    {
        scores += amount;
        UpdateScore();
        if (scores > GameplayController.Instance.scoresToLevelUp)
            GameplayController.Instance.LevelUp();
    }

    public void UpdateScore()
    {
        scoresText.text = "Scores: " + scores;
        if(scores > PlayerPrefs.GetInt("Best Score", 0))
            PlayerPrefs.SetInt("Best Score", scores);
    }
}
