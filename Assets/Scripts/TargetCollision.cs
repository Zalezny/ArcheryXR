using UnityEngine;

/// <summary>
/// Zarz�dza zachowaniem celu w przypadku kolizji z innymi obiektami.
/// </summary>
public class TargetCollision : MonoBehaviour
{

    /// <summary>
    /// Prefab wizualizuj�cy czarn� dziur� generowan� przy kolizji.
    /// </summary>
    public GameObject blackHole;

    /// <summary>
    /// Wizualna reprezentacja celu.
    /// </summary>
    public GameObject visualTarget;

    /// <summary>
    /// Komponent Rigidbody celu, u�ywany do kontroli fizyki.
    /// </summary>
    public Rigidbody rb;

    /// <summary>
    /// Kontroler zarz�dzaj�cy logik� celu.
    /// </summary>
    public TargetController controller;

    /// <summary>
    /// Wskazuje, czy cel jest ostatnim celem w grze.
    /// </summary>
    public bool isLastTarget;

    /// <summary>
    /// Licznik kolizji z powierzchni� �ciany.
    /// </summary>
    private int wallCollisionCount = 0;

    /// <summary>
    /// Pocz�tkowa pozycja celu w przestrzeni.
    /// </summary>
    private Vector3 startPos;

    /// <summary>
    /// Ustawia pocz�tkow� pozycj� celu.
    /// </summary>
    /// <param name="startPos">Wektor pozycji pocz�tkowej.</param>
    public void InitialPosition(Vector3 startPos)
    {
        this.startPos = startPos;
    }

    /// <summary>
    /// Wywo�ywana, gdy cel zostaje trafiony.
    /// Wy��cza ruch celu i haptyczne sprz�enie zwrotne.
    /// </summary>
    public void GetHit()
    {
        rb.isKinematic = false;
        
        controller.DisableMovingAndHapticFeedback();
    }

    /// <summary>
    /// Obs�uguje zdarzenie kolizji z innymi obiektami, takie jak �ciana.
    /// Tworzy wizualizacj� kolizji (czarn� dziur�) i obs�uguje usuni�cie celu po wielokrotnej kolizji.
    /// </summary>
    /// <param name="other">Kolider, z kt�rym cel wszed� w interakcj�.</param>
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
