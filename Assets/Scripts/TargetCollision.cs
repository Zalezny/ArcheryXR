using UnityEngine;

/// <summary>
/// Zarz¹dza zachowaniem celu w przypadku kolizji z innymi obiektami.
/// </summary>
public class TargetCollision : MonoBehaviour
{

    /// <summary>
    /// Prefab wizualizuj¹cy czarn¹ dziurê generowan¹ przy kolizji.
    /// </summary>
    public GameObject blackHole;

    /// <summary>
    /// Wizualna reprezentacja celu.
    /// </summary>
    public GameObject visualTarget;

    /// <summary>
    /// Komponent Rigidbody celu, u¿ywany do kontroli fizyki.
    /// </summary>
    public Rigidbody rb;

    /// <summary>
    /// Kontroler zarz¹dzaj¹cy logik¹ celu.
    /// </summary>
    public TargetController controller;

    /// <summary>
    /// Wskazuje, czy cel jest ostatnim celem w grze.
    /// </summary>
    public bool isLastTarget;

    /// <summary>
    /// Licznik kolizji z powierzchni¹ œciany.
    /// </summary>
    private int wallCollisionCount = 0;

    /// <summary>
    /// Pocz¹tkowa pozycja celu w przestrzeni.
    /// </summary>
    private Vector3 startPos;

    /// <summary>
    /// Ustawia pocz¹tkow¹ pozycjê celu.
    /// </summary>
    /// <param name="startPos">Wektor pozycji pocz¹tkowej.</param>
    public void InitialPosition(Vector3 startPos)
    {
        this.startPos = startPos;
    }

    /// <summary>
    /// Wywo³ywana, gdy cel zostaje trafiony.
    /// Wy³¹cza ruch celu i haptyczne sprzê¿enie zwrotne.
    /// </summary>
    public void GetHit()
    {
        rb.isKinematic = false;
        
        controller.DisableMovingAndHapticFeedback();
    }

    /// <summary>
    /// Obs³uguje zdarzenie kolizji z innymi obiektami, takie jak œciana.
    /// Tworzy wizualizacjê kolizji (czarn¹ dziurê) i obs³uguje usuniêcie celu po wielokrotnej kolizji.
    /// </summary>
    /// <param name="other">Kolider, z którym cel wszed³ w interakcjê.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "WALL_FACE_EffectMesh")
        {
            GameObject generatedHole = Instantiate(blackHole);
            generatedHole.transform.position = transform.position;
            Vector3 collisionNormal = transform.position - startPos;

            collisionNormal.Normalize();
            generatedHole.transform.rotation = Quaternion.Euler(collisionNormal.z * 90, 0, collisionNormal.x * 90);
            visualTarget.GetComponent<MeshRenderer>().enabled = true;

            wallCollisionCount++;
            if (wallCollisionCount > 1)
            {
                controller.RemoveTarget();
            }
        }
    }
}
