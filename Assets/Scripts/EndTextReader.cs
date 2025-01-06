using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Klasa odpowiedzialna za wyœwietlanie najlepszego wyniku oraz aktualnego wyniku
/// na ekranie koñcowym gry.
/// </summary>
public class EndTextReader : MonoBehaviour
{
    /// <summary>
    /// Klucz u¿ywany do odczytywania najlepszego wyniku z PlayerPrefs.
    /// </summary>
    [SerializeField]
    private string bestScoreKey = "BestScore";

    /// <summary>
    /// Obiekt UI zawieraj¹cy komponent TextMeshProUGUI dla wyœwietlania najlepszego wyniku.
    /// </summary>
    [SerializeField]
    private GameObject bestScoreGameObject;

    /// <summary>
    /// Obiekt UI zawieraj¹cy komponent TextMeshProUGUI dla wyœwietlania aktualnego wyniku.
    /// </summary>
    [SerializeField]
    private GameObject currentScoreGameObject;

    /// <summary>
    /// Komponent TextMeshProUGUI odpowiedzialny za wyœwietlanie najlepszego wyniku.
    /// </summary>
    private TextMeshProUGUI bestScoreText;

    /// <summary>
    /// Komponent TextMeshProUGUI odpowiedzialny za wyœwietlanie aktualnego wyniku.
    /// </summary>
    private TextMeshProUGUI currentScoreText;

    /// <summary>
    /// Wynik osi¹gniêty przez gracza w bie¿¹cej rozgrywce.
    /// </summary>
    public int achievedScore = 0;

    /// <summary>
    /// Inicjalizuje komponenty i wyœwietla najlepszy oraz aktualny wynik na ekranie koñcowym.
    /// </summary>
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

