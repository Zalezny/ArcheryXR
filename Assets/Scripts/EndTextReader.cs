using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class EndTextReader : MonoBehaviour
{
    public string bestScoreKey = "BestScore";
    public GameObject bestScoreGameObject;
    public GameObject currentScoreGameObject;

    private TextMeshProUGUI bestScoreText;
    private TextMeshProUGUI currentScoreText;
    public int achievedScore = 0;
    void Start()
    {
        bestScoreText = bestScoreGameObject.GetComponent<TextMeshProUGUI>();
        currentScoreText = currentScoreGameObject.GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey(bestScoreKey))
        {
            int score = PlayerPrefs.GetInt(bestScoreKey);
            bestScoreText.enabled = true;
            bestScoreText.text = bestScoreText.text + " " + score;

        } else
        {
            bestScoreText.enabled = false;
        }
        currentScoreText.enabled = true;
        currentScoreText.text = currentScoreText.text + " " + achievedScore;
    }


}
