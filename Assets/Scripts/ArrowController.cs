using UnityEngine;

/// <summary>
/// Kontroler strza³y, odpowiedzialny za przygotowanie i wypuszczanie strza³.
/// </summary>
public class ArrowController : MonoBehaviour
{
    /// <summary>
    /// Punkt wizualny na œrodku ciêciwy, aktywowany podczas przygotowywania strza³y.
    /// </summary>
    [SerializeField]
    private GameObject midStringVisualPoint;

    /// <summary>
    /// Prefab strza³y, który bêdzie u¿ywany do instancjonowania nowej strza³y.
    /// </summary>
    [SerializeField]
    private GameObject arrowPrefab;

    /// <summary>
    /// Punkt, z którego strza³a zostanie wystrzelona.
    /// </summary>
    [SerializeField]
    private GameObject arrowSpawnPoint;

    /// <summary>
    /// Maksymalna prêdkoœæ strza³y, stosowana podczas jej wystrzeliwania.
    /// </summary>
    [SerializeField]
    private float arrowMaxSpeed = 0.2f;

    /// <summary>
    /// Przygotowuje strza³ê do wystrzelenia, aktywuj¹c wizualny punkt na ciêciwie.
    /// </summary>
    public void PrepareArrow()
    {
        midStringVisualPoint.SetActive(true);
    }

    /// <summary>
    /// Wypuszcza strza³ê z okreœlon¹ si³¹.
    /// </summary>
    /// <param name="strength">Si³a wystrzelenia strza³y.</param>
    public void ReleaseArrow(float strength)
    {
        midStringVisualPoint.SetActive(false);

        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = arrowSpawnPoint.transform.position;
        arrow.transform.rotation = arrowSpawnPoint.transform.rotation;
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(midStringVisualPoint.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);
    }
}