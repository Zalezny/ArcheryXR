using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class EndTextReader : MonoBehaviour
{
    public string bestScoreKey = "BestScore";
    public TextMeshPro bestScoreText;
    public TextMeshPro currentScoreText;
    public int achievedScore = 0;
    void Start()
    {
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            int score = PlayerPrefs.GetInt(bestScoreKey);
            bestScoreText.enabled = true;
            bestScoreText.text = bestScoreText.text + " " + score;

        } else
        {
            bestScoreText.enabled = false;
        }
        
    }

    private void Update()
    {

        if (achievedScore > 0)
        {
            currentScoreText.enabled = true;
            currentScoreText.text = currentScoreText.text + " " + achievedScore;
        }
    }
}
