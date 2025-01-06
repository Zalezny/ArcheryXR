using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za generowanie wizualnej reprezentacji ciêciwy ³uku
/// za pomoc¹ komponentu LineRenderer.
/// </summary>
[RequireComponent (typeof(LineRenderer))]
public class BowChord : MonoBehaviour
{
    /// <summary>
    /// Punkt pocz¹tkowy ciêciwy na ³uku.
    /// </summary>
    [SerializeField]
    private Transform startAnchor;

    /// <summary>
    /// Punkt koñcowy ciêciwy na ³uku.
    /// </summary>
    [SerializeField]
    private Transform endAnchor;

    /// <summary>
    /// Komponent LineRenderer u¿ywany do rysowania ciêciwy.
    /// </summary>
    private LineRenderer lineRenderer;

    /// <summary>
    /// Inicjalizuje komponent LineRenderer podczas budzenia obiektu.
    /// </summary>
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Generuje ciêciwê z opcjonalnym punktem œrodkowym, który symuluje naci¹gniêcie ³uku.
    /// </summary>
    /// <param name="midPoint">
    /// Opcjonalny punkt œrodkowy, definiuj¹cy naci¹g ciêciwy.
    /// Jeœli wartoœæ nie jest ustawiona, ciêciwa jest prost¹ lini¹ miêdzy punktami pocz¹tkowym i koñcowym.
    /// </param>
    public void GenerateString(Vector3? midPoint)
    {
        Vector3[] pointInLine = new Vector3[midPoint.HasValue ? 3 : 2];
        pointInLine[0] = startAnchor.localPosition;

        if (midPoint.HasValue)
        {
            pointInLine[1] = transform.InverseTransformPoint(midPoint.Value);
        }

        pointInLine[^1] = endAnchor.localPosition;

        lineRenderer.positionCount = pointInLine.Length;
        lineRenderer.SetPositions(pointInLine);
    }

    /// <summary>
    /// Generuje domyœln¹ ciêciwê (prost¹ liniê) po uruchomieniu obiektu.
    /// </summary>
    private void Start()
    {
        GenerateString(null);
    }

}
