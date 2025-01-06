using UnityEngine;

/// <summary>
/// Kontroler strza�y, odpowiedzialny za przygotowanie i wypuszczanie strza�.
/// </summary>
public class ArrowController : MonoBehaviour
{
    /// <summary>
    /// Punkt wizualny na �rodku ci�ciwy, aktywowany podczas przygotowywania strza�y.
    /// </summary>
    [SerializeField]
    private GameObject midStringVisualPoint;

    /// <summary>
    /// Prefab strza�y, kt�ry b�dzie u�ywany do instancjonowania nowej strza�y.
    /// </summary>
    [SerializeField]
    private GameObject arrowPrefab;

    /// <summary>
    /// Punkt, z kt�rego strza�a zostanie wystrzelona.
    /// </summary>
    [SerializeField]
    private GameObject arrowSpawnPoint;

    /// <summary>
    /// Maksymalna pr�dko�� strza�y, stosowana podczas jej wystrzeliwania.
    /// </summary>
    [SerializeField]
    private float arrowMaxSpeed = 0.2f;

    /// <summary>
    /// Przygotowuje strza�� do wystrzelenia, aktywuj�c wizualny punkt na ci�ciwie.
    /// </summary>
    public void PrepareArrow()
    {
        midStringVisualPoint.SetActive(true);
    }

    /// <summary>
    /// Wypuszcza strza�� z okre�lon� si��.
    /// </summary>
    /// <param name="strength">Si�a wystrzelenia strza�y.</param>
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