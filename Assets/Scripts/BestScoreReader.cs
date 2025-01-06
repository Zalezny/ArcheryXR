using TMPro;
using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za odczytywanie najlepszego wyniku z pamiêci gracza i wyœwietlanie go na obiekcie z komponentem TextMeshPro.
/// </summary>
public class BestScoreReader : MonoBehaviour
{
    /// <summary>
    /// Klucz u¿ywany do odczytywania najlepszego wyniku z PlayerPrefs.
    /// Domyœlnie ustawiony na "BestScore".
    /// </summary>
    [SerializeField]
    private string key = "BestScore";

    /// <summary>
    /// Metoda wywo³ywana na pocz¹tku, która sprawdza, czy istnieje zapisany najlepszy wynik.
    /// Jeœli wynik istnieje, jest wyœwietlany na komponencie TextMeshPro.
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
