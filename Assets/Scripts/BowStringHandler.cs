using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Obs�uguje logik� naci�gania ci�ciwy �uku, w tym wizualizacj� naci�gu,
/// wykrywanie interakcji u�ytkownika oraz obs�ug� zdarze� zwi�zanych z naci�ganiem i puszczeniem ci�ciwy.
/// </summary>
public class BowStringHandler : MonoBehaviour
{
    /// <summary>
    /// Komponent odpowiedzialny za renderowanie ci�ciwy.
    /// </summary>
    [SerializeField]
    private BowChord stringRenderer;

    /// <summary>
    /// Obiekt, kt�ry u�ytkownik chwyta, aby naci�gn�� ci�ciw�.
    /// </summary>
    [SerializeField]
    private Transform grabHandle;

    /// <summary>
    /// Wizualny �rodek ci�ciwy, przesuwany w miar� naci�gania �uku.
    /// </summary>
    [SerializeField]
    private Transform visualMidpoint;

    /// <summary>
    /// Rodzic �rodkowego punktu ci�ciwy, u�ywany do oblicze� pozycji lokalnej.
    /// </summary>
    [SerializeField]
    private Transform midpointParent;

    /// <summary>
    /// Maksymalna odleg�o�� naci�gu ci�ciwy, po kt�rej naci�g jest ograniczony.
    /// </summary>
    [SerializeField]
    private float maxDrawDistance = 0.25f;

    /// <summary>
    /// Komponent obs�uguj�cy chwytanie ci�ciwy.
    /// </summary>
    private Grabbable grabInteractable;

    /// <summary>
    /// Aktualny interaktor, kt�ry trzyma ci�ciw�.
    /// </summary>
    private Transform currentInteractor;

    /// <summary>
    /// Aktualna si�a naci�gu ci�ciwy (0 do 1).
    /// </summary>
    private float currentDrawStrength;

    /// <summary>
    /// Wydarzenie wywo�ywane, gdy u�ytkownik zaczyna naci�ga� ci�ciw�.
    /// </summary>
    public UnityEvent OnBowDrawStarted;

    /// <summary>
    /// Wydarzenie wywo�ywane, gdy u�ytkownik puszcza ci�ciw�.
    /// Przekazuje warto�� si�y naci�gu (od 0 do 1).
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
    /// Usuwa rejestracj� zdarze� interakcji w momencie zniszczenia obiektu.
    /// </summary>
    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.WhenPointerEventRaised -= OnPointerEvent;
        }
    }

    /// <summary>
    /// Obs�uguje zdarzenia wska�nika interakcji (np. rozpocz�cie lub zako�czenie naci�gania ci�ciwy).
    /// </summary>
    /// <param name="pointerEvent">Dane dotycz�ce zdarzenia wska�nika.</param>
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
    /// Rozpoczyna proces naci�gania ci�ciwy.
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
    /// Resetuje ci�ciw� do stanu pocz�tkowego po puszczeniu.
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
    /// Aktualizuje logik� ci�ciwy w ka�dej klatce.
    /// Obs�uguje naci�g ci�ciwy oraz wywo�uje odpowiednie zdarzenia wizualne.
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
    /// Obs�uguje rysowanie ci�ciwy podczas naci�gu w normalnym zakresie.
    /// </summary>
    /// <param name="absZ">Bezwzgl�dna warto�� wsp�rz�dnej Z ci�ciwy.</param>
    /// <param name="localPosition">Pozycja lokalna ci�ciwy.</param>
    private void HandleStringDrawing(float absZ, Vector3 localPosition)
    {
        if (localPosition.z < 0f && absZ < maxDrawDistance)
        {
            currentDrawStrength = Remap(absZ, 0f, maxDrawDistance, 0f, 1f);
            visualMidpoint.localPosition = new Vector3(0, 0, localPosition.z);
        }
    }

    /// <summary>
    /// Przekszta�ca warto�� z jednego zakresu na inny.
    /// </summary>
    /// <param name="value">Warto�� do przekszta�cenia.</param>
    /// <param name="fromMin">Dolny zakres wej�ciowy.</param>
    /// <param name="fromMax">G�rny zakres wej�ciowy.</param>
    /// <param name="toMin">Dolny zakres wyj�ciowy.</param>
    /// <param name="toMax">G�rny zakres wyj�ciowy.</param>
    /// <returns>Przekszta�cona warto�� w nowym zakresie.</returns>
    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    /// <summary>
    /// Ogranicza naci�g ci�ciwy do maksymalnego dystansu.
    /// </summary>
    /// <param name="absZ">Bezwzgl�dna warto�� wsp�rz�dnej Z ci�ciwy.</param>
    /// <param name="localPosition">Pozycja lokalna ci�ciwy.</param>
    private void HandleStringAtMaxDraw(float absZ, Vector3 localPosition)
    {
        if (localPosition.z < 0f && absZ >= maxDrawDistance)
        {
            currentDrawStrength = 1f;
            visualMidpoint.localPosition = new Vector3(0, 0, -maxDrawDistance);
        }
    }

    /// <summary>
    /// Ustawia ci�ciw� w pozycji pocz�tkowej, gdy naci�g nie zosta� rozpocz�ty.
    /// </summary>
    /// <param name="localPosition">Pozycja lokalna ci�ciwy.</param>
    private void HandleStringAtStartPosition(Vector3 localPosition)
    {
        if (localPosition.z >= 0f)
        {
            currentDrawStrength = 0f;
            visualMidpoint.localPosition = Vector3.zero;
        }
    }
}