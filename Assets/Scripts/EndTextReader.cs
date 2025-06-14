using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Klasa odpowiedzialna za wyświetlanie najlepszego wyniku oraz aktualnego wyniku
/// na ekranie końcowym gry.
/// </summary>
public class EndTextReader : MonoBehaviour
{
    /// <summary>
    /// Klucz używany do odczytywania najlepszego wyniku z PlayerPrefs.
    /// </summary>
    [SerializeField]
    private string bestScoreKey = "BestScore";

    /// <summary>
    /// Obiekt UI zawierający komponent TextMeshProUGUI dla wyświetlania najlepszego wyniku.
    /// </summary>
    [SerializeField]
    private GameObject bestScoreGameObject;

    /// <summary>
    /// Obiekt UI zawierający komponent TextMeshProUGUI dla wyświetlania aktualnego wyniku.
    /// </summary>
    [SerializeField]
    private GameObject currentScoreGameObject;

    /// <summary>
    /// Komponent TextMeshProUGUI odpowiedzialny za wyświetlanie najlepszego wyniku.
    /// </summary>
    private TextMeshProUGUI bestScoreText;

    /// <summary>
    /// Komponent TextMeshProUGUI odpowiedzialny za wyświetlanie aktualnego wyniku.
    /// </summary>
    private TextMeshProUGUI currentScoreText;

    /// <summary>
    /// Wynik osiągnięty przez gracza w bieżącej rozgrywce.
    /// </summary>
    public int achievedScore = 0;

    /// <summary>
    /// Inicjalizuje komponenty i wyświetla najlepszy oraz aktualny wynik na ekranie końcowym.
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

