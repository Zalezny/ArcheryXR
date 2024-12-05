using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class EndTextReader : MonoBehaviour
{
    public string bestScoreKey = "BestScore";
    public GameObject bestScoreGameObject;
    public GameObject currentScoreGameObject;

    private TextMeshPro bestScoreText;
    private TextMeshPro currentScoreText;
    public int achievedScore = 0;
    void Start()
    {
        bestScoreText = bestScoreGameObject.GetComponent<TextMeshPro>();
        currentScoreText = currentScoreGameObject.GetComponent<TextMeshPro>();
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
