using TMPro;
using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za odczytywanie najlepszego wyniku z pami�ci gracza i wy�wietlanie go na obiekcie z komponentem TextMeshPro.
/// </summary>
public class BestScoreReader : MonoBehaviour
{
    /// <summary>
    /// Klucz u�ywany do odczytywania najlepszego wyniku z PlayerPrefs.
    /// Domy�lnie ustawiony na "BestScore".
    /// </summary>
    [SerializeField]
    private string key = "BestScore";

    /// <summary>
    /// Metoda wywo�ywana na pocz�tku, kt�ra sprawdza, czy istnieje zapisany najlepszy wynik.
    /// Je�li wynik istnieje, jest wy�wietlany na komponencie TextMeshPro.
    /// </summary>
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
}
