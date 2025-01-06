using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Obs³uguje logikê naci¹gania ciêciwy ³uku, w tym wizualizacjê naci¹gu,
/// wykrywanie interakcji u¿ytkownika oraz obs³ugê zdarzeñ zwi¹zanych z naci¹ganiem i puszczeniem ciêciwy.
/// </summary>
public class BowStringHandler : MonoBehaviour
{
    /// <summary>
    /// Komponent odpowiedzialny za renderowanie ciêciwy.
    /// </summary>
    [SerializeField]
    private BowChord stringRenderer;

    /// <summary>
    /// Obiekt, który u¿ytkownik chwyta, aby naci¹gn¹æ ciêciwê.
    /// </summary>
    [SerializeField]
    private Transform grabHandle;

    /// <summary>
    /// Wizualny œrodek ciêciwy, przesuwany w miarê naci¹gania ³uku.
    /// </summary>
    [SerializeField]
    private Transform visualMidpoint;

    /// <summary>
    /// Rodzic œrodkowego punktu ciêciwy, u¿ywany do obliczeñ pozycji lokalnej.
    /// </summary>
    [SerializeField]
    private Transform midpointParent;

    /// <summary>
    /// Maksymalna odleg³oœæ naci¹gu ciêciwy, po której naci¹g jest ograniczony.
    /// </summary>
    [SerializeField]
    private float maxDrawDistance = 0.25f;

    /// <summary>
    /// Komponent obs³uguj¹cy chwytanie ciêciwy.
    /// </summary>
    private Grabbable grabInteractable;

    /// <summary>
    /// Aktualny interaktor, który trzyma ciêciwê.
    /// </summary>
    private Transform currentInteractor;

    /// <summary>
    /// Aktualna si³a naci¹gu ciêciwy (0 do 1).
    /// </summary>
    private float currentDrawStrength;

    /// <summary>
    /// Wydarzenie wywo³ywane, gdy u¿ytkownik zaczyna naci¹gaæ ciêciwê.
    /// </summary>
    public UnityEvent OnBowDrawStarted;

    /// <summary>
    /// Wydarzenie wywo³ywane, gdy u¿ytkownik puszcza ciêciwê.
    /// Przekazuje wartoœæ si³y naci¹gu (od 0 do 1).
    /// </summary>
    public UnityEvent<float> OnBowReleased;

    /// <summary>
    /// Inicjalizuje komponent Grabbable.
    /// </summary>
    private void Awake()
    {
        if(grabHandle != null)
            if(grabHandle.TryGetComponent<Grabbable>(out Grabbable grab))
            {
                grabInteractable = grab;
            }
    }

    /// <summary>
    /// Rejestruje zdarzenia interakcji w momencie uruchomienia obiektu.
    /// </summary>
    private void Start()
    {
        if (grabInteractable != null)
        {
            grabInteractable.WhenPointerEventRaised += OnPointerEvent;
        }
    }

    /// <summary>
    /// Usuwa rejestracjê zdarzeñ interakcji w momencie zniszczenia obiektu.
    /// </summary>
    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.WhenPointerEventRaised -= OnPointerEvent;
        }
    }

    /// <summary>
    /// Obs³uguje zdarzenia wskaŸnika interakcji (np. rozpoczêcie lub zakoñczenie naci¹gania ciêciwy).
    /// </summary>
    /// <param name="pointerEvent">Dane dotycz¹ce zdarzenia wskaŸnika.</param>
    private void OnPointerEvent(PointerEvent pointerEvent)
    {
        if (pointerEvent.Type == PointerEventType.Select)
        {
            StartDrawing();
        }
        else if (pointerEvent.Type == PointerEventType.Unselect)
        {
            ResetString();
        }
    }

    /// <summary>
    /// Rozpoczyna proces naci¹gania ciêciwy.
    /// </summary>
    private void StartDrawing()
    {
        if (grabInteractable != null)
        {
            currentInteractor = grabInteractable.transform;
        }
        OnBowDrawStarted?.Invoke();
    }

    /// <summary>
    /// Resetuje ciêciwê do stanu pocz¹tkowego po puszczeniu.
    /// </summary>
    private void ResetString()
    {
        OnBowReleased?.Invoke(currentDrawStrength);
        currentDrawStrength = 0f;

        currentInteractor = null;
        grabHandle.localPosition = Vector3.zero;
        visualMidpoint.localPosition = Vector3.zero;
        stringRenderer.GenerateString(null);
    }

    /// <summary>
    /// Aktualizuje logikê ciêciwy w ka¿dej klatce.
    /// Obs³uguje naci¹g ciêciwy oraz wywo³uje odpowiednie zdarzenia wizualne.
    /// </summary>
    private void Update()
    {
        if (currentInteractor != null)
        {
            Vector3 localPosition = midpointParent.InverseTransformPoint(grabHandle.position);
            float absZ = Mathf.Abs(localPosition.z);

            HandleStringAtStartPosition(localPosition);
            HandleStringAtMaxDraw(absZ, localPosition);
            HandleStringDrawing(absZ, localPosition);

            stringRenderer.GenerateString(visualMidpoint.position);
        }
    }

    /// <summary>
    /// Obs³uguje rysowanie ciêciwy podczas naci¹gu w normalnym zakresie.
    /// </summary>
    /// <param name="absZ">Bezwzglêdna wartoœæ wspó³rzêdnej Z ciêciwy.</param>
    /// <param name="localPosition">Pozycja lokalna ciêciwy.</param>
    private void HandleStringDrawing(float absZ, Vector3 localPosition)
    {
        if (localPosition.z < 0f && absZ < maxDrawDistance)
        {
            currentDrawStrength = Remap(absZ, 0f, maxDrawDistance, 0f, 1f);
            visualMidpoint.localPosition = new Vector3(0, 0, localPosition.z);
        }
    }

    /// <summary>
    /// Przekszta³ca wartoœæ z jednego zakresu na inny.
    /// </summary>
    /// <param name="value">Wartoœæ do przekszta³cenia.</param>
    /// <param name="fromMin">Dolny zakres wejœciowy.</param>
    /// <param name="fromMax">Górny zakres wejœciowy.</param>
    /// <param name="toMin">Dolny zakres wyjœciowy.</param>
    /// <param name="toMax">Górny zakres wyjœciowy.</param>
    /// <returns>Przekszta³cona wartoœæ w nowym zakresie.</returns>
    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    /// <summary>
    /// Ogranicza naci¹g ciêciwy do maksymalnego dystansu.
    /// </summary>
    /// <param name="absZ">Bezwzglêdna wartoœæ wspó³rzêdnej Z ciêciwy.</param>
    /// <param name="localPosition">Pozycja lokalna ciêciwy.</param>
    private void HandleStringAtMaxDraw(float absZ, Vector3 localPosition)
    {
        if (localPosition.z < 0f && absZ >= maxDrawDistance)
        {
            currentDrawStrength = 1f;
            visualMidpoint.localPosition = new Vector3(0, 0, -maxDrawDistance);
        }
    }

    /// <summary>
    /// Ustawia ciêciwê w pozycji pocz¹tkowej, gdy naci¹g nie zosta³ rozpoczêty.
    /// </summary>
    /// <param name="localPosition">Pozycja lokalna ciêciwy.</param>
    private void HandleStringAtStartPosition(Vector3 localPosition)
    {
        if (localPosition.z >= 0f)
        {
            currentDrawStrength = 0f;
            visualMidpoint.localPosition = Vector3.zero;
        }
    }
}