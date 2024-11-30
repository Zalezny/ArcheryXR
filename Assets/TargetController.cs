using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float moveDuration = 10f;
    private Vector3 mirroredPosition;
    private bool isMoving;
    private Vector3 wallNormal = new Vector3(1,0,0);
    public string playerTag = "Player";
    public void StartMoving()
    {
        isMoving = true;
    }

    void Start()
    {
        CalculateMirroredPosition();
        RotateObject();
    }

    void Update()
    {
        if (isMoving)
        {
            // Ruch w kierunku odbitej pozycji
            float step = Time.deltaTime / moveDuration; // Oblicz krok na podstawie czasu
            transform.position = Vector3.MoveTowards(transform.position, mirroredPosition, step);

            // Sprawdzenie, czy osi¹gniêto odbit¹ pozycjê
            if (Vector3.Distance(transform.position, mirroredPosition) < 0.001f)
            {
                isMoving = false; // Zatrzymaj ruch po osi¹gniêciu celu
            }
        }
    }


    void CalculateMirroredPosition()
    {
        // Obliczanie wektora od pozycji obiektu do pozycji œciany
        Vector3 directionToWall = transform.position;

        // Odbicie lustrzane
        mirroredPosition = Vector3.Reflect(directionToWall.normalized, wallNormal) * directionToWall.magnitude;

        Debug.Log("Odbita pozycja: " + mirroredPosition);
    }

    void RotateObject()
    {
        Transform player = GameObject.FindGameObjectWithTag(playerTag).transform;
        // Oblicz kierunek do gracza
        Vector3 directionToPlayer = player.position - transform.position;

        // Ustaw rotacjê obiektu w kierunku gracza
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

        // Dodaj obrót o 90 stopni wokó³ osi Y
        targetRotation *= Quaternion.Euler(0, -90, 0); // Zmiana k¹ta o 90 stopni

        // Ustaw now¹ rotacjê
        transform.rotation = targetRotation;
    }
}
