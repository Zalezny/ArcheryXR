using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

/// <summary>
/// Klasa odpowiedzialna za wy�wietlanie najlepszego wyniku oraz aktualnego wyniku
/// na ekranie ko�cowym gry.
/// </summary>
public class EndTextReader : MonoBehaviour
{
    /// <summary>
    /// Klucz u�ywany do odczytywania najlepszego wyniku z PlayerPrefs.
    /// </summary>
    [SerializeField]
    private string bestScoreKey = "BestScore";

    /// <summary>
    /// Obiekt UI zawieraj�cy komponent TextMeshProUGUI dla wy�wietlania najlepszego wyniku.
    /// </summary>
    [SerializeField]
    private GameObject bestScoreGameObject;

    /// <summary>
    /// Obiekt UI zawieraj�cy komponent TextMeshProUGUI dla wy�wietlania aktualnego wyniku.
    /// </summary>
    [SerializeField]
    private GameObject currentScoreGameObject;

    /// <summary>
    /// Komponent TextMeshProUGUI odpowiedzialny za wy�wietlanie najlepszego wyniku.
    /// </summary>
    private TextMeshProUGUI bestScoreText;

    /// <summary>
    /// Komponent TextMeshProUGUI odpowiedzialny za wy�wietlanie aktualnego wyniku.
    /// </summary>
    private TextMeshProUGUI currentScoreText;

    /// <summary>
    /// Wynik osi�gni�ty przez gracza w bie��cej rozgrywce.
    /// </summary>
    public int achievedScore = 0;

    /// <summary>
    /// Inicjalizuje komponenty i wy�wietla najlepszy oraz aktualny wynik na ekranie ko�cowym.
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

