using TMPro;
using UnityEngine;

public class BestScoreReader : MonoBehaviour
{
    public string key = "BestScore";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(PlayerPrefs.HasKey(key))
        {
            int score = PlayerPrefs.GetInt(key);
            TextMeshPro textMeshPro = GetComponent<TextMeshPro>();
            textMeshPro.enabled = true;
            textMeshPro.text = textMeshPro.text + " " + score;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
