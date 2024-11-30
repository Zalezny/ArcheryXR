using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public float moveDuration = 10f;
    private bool isMoving;
    public string playerTag = "Player";
    private Rigidbody rb;
    public float speed = 1f;
    private Vector3 direction;
    private float moveTimer;
    public void StartMoving(Vector3 normal)
    {
        GetComponent<TargetCollision>().InitialPosition(transform.position);
        isMoving = true;
        direction = normal.normalized;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveTimer = moveDuration;
    }

    void Update()
    {
        if (isMoving)
        {
            if (moveTimer > 0)
            {
                MoveInDirection();
                moveTimer -= Time.deltaTime; // Zmniejsz timer
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    private void MoveInDirection()
    {
        // Oblicz nowe po³o¿enie
        Vector3 newPosition = transform.position + speed * Time.deltaTime * direction;

        // Ustaw now¹ pozycjê obiektu
        transform.position = newPosition;

        RotateObjectToPlayer();
    }

    void RotateObjectToPlayer()
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

    public void GetHit()
    {
        rb.isKinematic = false;
        isMoving = false;
    }
}
