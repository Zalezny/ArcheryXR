using UnityEngine;

/// <summary>
/// Zarz¹dza logik¹ ruchu celu, jego interakcjami i obs³ug¹ zdarzeñ po trafieniu.
/// </summary>
public class TargetController : MonoBehaviour
{
    /// <summary>
    /// Czas trwania ruchu celu w sekundach.
    /// </summary>
    public float moveDuration = 10f;

    /// <summary>
    /// Wskazuje, czy cel aktualnie siê porusza.
    /// </summary>
    private bool isMoving;

    /// <summary>
    /// Tag kamery g³ównej u¿ywany do ustalenia pozycji gracza.
    /// </summary>
    public string mainCameraTag = "MainCamera";

    /// <summary>
    /// Tag kontrolera gry u¿ywany do obs³ugi zdarzeñ koñca poziomu.
    /// </summary>
    public string gameControllerTag = "GameController";

    /// <summary>
    /// Komponent Rigidbody celu, u¿ywany do kontroli fizyki.
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Prêdkoœæ poruszania siê celu.
    /// </summary>
    public float speed = 1f;

    /// <summary>
    /// Wskazuje, czy cel jest ostatnim celem na poziomie.
    /// </summary>
    public bool isLastTarget = false;

    /// <summary>
    /// Kierunek ruchu celu.
    /// </summary>
    private Vector3 direction;

    /// <summary>
    /// Licznik czasu ruchu celu.
    /// </summary>
    private float moveTimer = 0f;

    /// <summary>
    /// Odpowiada za haptyczne sprzê¿enie zwrotne po trafieniu celu.
    /// </summary>
    public HapticFeedback hapticFeedback;

    /// <summary>
    /// Inicjalizuje ruch celu w okreœlonym kierunku.
    /// </summary>
    /// <param name="normal">Wektor normalny okreœlaj¹cy kierunek ruchu.</param>
    public void StartMoving(Vector3 normal)
    {
        var targetCollision = GetComponent<TargetCollision>();
        targetCollision.InitialPosition(transform.position);
        targetCollision.isLastTarget = isLastTarget;
        isMoving = true;
        direction = normal.normalized;
    }

    /// <summary>
    /// Wywo³ywana, gdy cel zostaje trafiony.
    /// Wy³¹cza ruch celu i fizykê.
    /// </summary>
    public void GetHit()
    {
        rb.isKinematic = false;
    }

    /// <summary>
    /// Inicjalizuje komponenty celu podczas uruchamiania obiektu.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveTimer = 0f;
    }

    /// <summary>
    /// Aktualizuje ruch celu i sprawdza, czy czas ruchu min¹³.
    /// </summary>
    void Update()
    {
        UnityEngine.Debug.Log("Czas tarczy " + moveTimer + " " + isLastTarget);
        if (moveTimer > moveDuration )
        {
            RemoveTarget();
            UnityEngine.Debug.Log("Usuniêto tarcze " + isLastTarget);
        }
        else
        {
            if(isMoving)
            {
                MoveInDirection();
            }
            moveTimer += Time.deltaTime;
                
        }
    }

    /// <summary>
    /// Usuwa cel z poziomu, a jeœli jest to ostatni cel, uruchamia logikê koñca poziomu.
    /// </summary>
    public void RemoveTarget()
    {
        if (isLastTarget)
        {
            OnLastTarget();
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Wywo³ywana, gdy ostatni cel zostaje usuniêty.
    /// Informuje kontroler gry o zakoñczeniu poziomu.
    /// </summary>
    private void OnLastTarget()
    {
        UnityEngine.Debug.Log("Test OnLastTarget");

        var gameController = GameObject.FindGameObjectWithTag(gameControllerTag);
        gameController.GetComponent<GameStateManager>().CheckResultOfLevel();
    }

    /// <summary>
    /// Porusza cel w zadanym kierunku i obraca go w stronê gracza.
    /// </summary>
    private void MoveInDirection()
    {
        Vector3 newPosition = transform.position + speed * Time.deltaTime * direction;
        transform.position = newPosition;
        RotateObjectToPlayer();
    }

    /// <summary>
    /// Obraca cel w stronê gracza, ustawiaj¹c rotacjê obiektu w jego kierunku.
    /// </summary>
    void RotateObjectToPlayer()
    {
        
        Transform player = GameObject.FindGameObjectWithTag(mainCameraTag).transform;
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        targetRotation *= Quaternion.Euler(0, -90, 0);
        transform.rotation = targetRotation;
    }

    /// <summary>
    /// Wy³¹cza ruch celu i uruchamia haptyczne sprzê¿enie zwrotne.
    /// </summary>
    public void DisableMovingAndHapticFeedback()
    {
        isMoving = false;
        hapticFeedback.TriggerVibration(0.5f);
    }
}
