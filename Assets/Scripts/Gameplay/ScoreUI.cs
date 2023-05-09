using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public int currentScore;
    public TextMeshProUGUI scoreText;

    private WaveManager _waveManager;

    private void Awake()
    {
        _waveManager = FindObjectOfType<WaveManager>(true);
    }

    private void OnEnable()
    {
        _waveManager.PointsScored += AddToScore;
    }

    private void AddToScore(int points)
    {
        currentScore += points;
        scoreText.text = currentScore.ToString();
    }

    private void OnDisable()
    {
        _waveManager.PointsScored -= AddToScore;
    }
}