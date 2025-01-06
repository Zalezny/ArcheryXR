using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za generowanie wizualnej reprezentacji ci�ciwy �uku
/// za pomoc� komponentu LineRenderer.
/// </summary>
[RequireComponent (typeof(LineRenderer))]
public class BowChord : MonoBehaviour
{
    /// <summary>
    /// Punkt pocz�tkowy ci�ciwy na �uku.
    /// </summary>
    [SerializeField]
    private Transform startAnchor;

    /// <summary>
    /// Punkt ko�cowy ci�ciwy na �uku.
    /// </summary>
    [SerializeField]
    private Transform endAnchor;

    /// <summary>
    /// Komponent LineRenderer u�ywany do rysowania ci�ciwy.
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
    /// Generuje ci�ciw� z opcjonalnym punktem �rodkowym, kt�ry symuluje naci�gni�cie �uku.
    /// </summary>
    /// <param name="midPoint">
    /// Opcjonalny punkt �rodkowy, definiuj�cy naci�g ci�ciwy.
    /// Je�li warto�� nie jest ustawiona, ci�ciwa jest prost� lini� mi�dzy punktami pocz�tkowym i ko�cowym.
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
    /// Generuje domy�ln� ci�ciw� (prost� lini�) po uruchomieniu obiektu.
    /// </summary>
    private void Start()
    {
        GenerateString(null);
    }

}
