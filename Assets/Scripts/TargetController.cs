using UnityEngine;

/// <summary>
/// Zarz�dza logik� ruchu celu, jego interakcjami i obs�ug� zdarze� po trafieniu.
/// </summary>
public class TargetController : MonoBehaviour
{
    /// <summary>
    /// Czas trwania ruchu celu w sekundach.
    /// </summary>
    public float moveDuration = 10f;

    /// <summary>
    /// Wskazuje, czy cel aktualnie si� porusza.
    /// </summary>
    private bool isMoving;

    /// <summary>
    /// Tag kamery g��wnej u�ywany do ustalenia pozycji gracza.
    /// </summary>
    public string mainCameraTag = "MainCamera";

    /// <summary>
    /// Tag kontrolera gry u�ywany do obs�ugi zdarze� ko�ca poziomu.
    /// </summary>
    public string gameControllerTag = "GameController";

    /// <summary>
    /// Komponent Rigidbody celu, u�ywany do kontroli fizyki.
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Pr�dko�� poruszania si� celu.
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
    /// Odpowiada za haptyczne sprz�enie zwrotne po trafieniu celu.
    /// </summary>
    public HapticFeedback hapticFeedback;

    /// <summary>
    /// Inicjalizuje ruch celu w okre�lonym kierunku.
    /// </summary>
    /// <param name="normal">Wektor normalny okre�laj�cy kierunek ruchu.</param>
    public void StartMoving(Vector3 normal)
    {
        var targetCollision = GetComponent<TargetCollision>();
        targetCollision.InitialPosition(transform.position);
        targetCollision.isLastTarget = isLastTarget;
        isMoving = true;
        direction = normal.normalized;
    }

    /// <summary>
    /// Wywo�ywana, gdy cel zostaje trafiony.
    /// Wy��cza ruch celu i fizyk�.
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
    /// Aktualizuje ruch celu i sprawdza, czy czas ruchu min��.
    /// </summary>
    void Update()
    {
        UnityEngine.Debug.Log("Czas tarczy " + moveTimer + " " + isLastTarget);
        if (moveTimer > moveDuration )
        {
            RemoveTarget();
            UnityEngine.Debug.Log("Usuni�to tarcze " + isLastTarget);
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
    /// Usuwa cel z poziomu, a je�li jest to ostatni cel, uruchamia logik� ko�ca poziomu.
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
    /// Wywo�ywana, gdy ostatni cel zostaje usuni�ty.
    /// Informuje kontroler gry o zako�czeniu poziomu.
    /// </summary>
    private void OnLastTarget()
    {
        UnityEngine.Debug.Log("Test OnLastTarget");

        var gameController = GameObject.FindGameObjectWithTag(gameControllerTag);
        gameController.GetComponent<GameStateManager>().CheckResultOfLevel();
    }

    /// <summary>
    /// Porusza cel w zadanym kierunku i obraca go w stron� gracza.
    /// </summary>
    private void MoveInDirection()
    {
        Vector3 newPosition = transform.position + speed * Time.deltaTime * direction;
        transform.position = newPosition;
        RotateObjectToPlayer();
    }

    /// <summary>
    /// Obraca cel w stron� gracza, ustawiaj�c rotacj� obiektu w jego kierunku.
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
    /// Wy��cza ruch celu i uruchamia haptyczne sprz�enie zwrotne.
    /// </summary>
    public void DisableMovingAndHapticFeedback()
    {
        isMoving = false;
        hapticFeedback.TriggerVibration(0.5f);
    }
}
