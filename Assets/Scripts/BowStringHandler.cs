using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class BowStringHandler : MonoBehaviour
{
    [SerializeField]
    private BowChord stringRenderer;

    [SerializeField]
    private Transform grabHandle; // dawniej midPointGrabObject
    [SerializeField]
    private Transform visualMidpoint; // dawniej midPointVisualObject
    [SerializeField]
    private Transform midpointParent; // dawniej midPointParent

    [SerializeField]
    private float maxDrawDistance = 0.25f; // dawniej bowStringStretchLimit

    private Grabbable grabInteractable;
    private Transform currentInteractor; // dawniej interactor
    private float currentDrawStrength; // dawniej strength

    public UnityEvent OnBowDrawStarted; // dawniej onBowPulled
    public UnityEvent<float> OnBowReleased; // dawniej onBowReleased

    private void Awake()
    {
        grabInteractable = grabHandle.GetComponent<Grabbable>();
    }

    private void Start()
    {
        grabInteractable.WhenPointerEventRaised += OnPointerEvent;
    }

    private void OnDestroy()
    {
        grabInteractable.WhenPointerEventRaised -= OnPointerEvent;
    }

    private void OnPointerEvent(PointerEvent pointerEvent)
    {
        if (pointerEvent.Type == PointerEventType.Select)
        {
            StartDrawing(pointerEvent);
        }
        else if (pointerEvent.Type == PointerEventType.Unselect)
        {
            ResetString();
        }
    }

    private void StartDrawing(PointerEvent pointerEvent)
    {
        currentInteractor = grabInteractable.transform;
        OnBowDrawStarted?.Invoke();
    }

    private void ResetString()
    {
        OnBowReleased?.Invoke(currentDrawStrength);
        currentDrawStrength = 0f;

        currentInteractor = null;
        grabHandle.localPosition = Vector3.zero;
        visualMidpoint.localPosition = Vector3.zero;
        stringRenderer.GenerateString(null);
    }

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

    private void HandleStringDrawing(float absZ, Vector3 localPosition)
    {
        if (localPosition.z < 0f && absZ < maxDrawDistance)
        {
            currentDrawStrength = Remap(absZ, 0f, maxDrawDistance, 0f, 1f);
            visualMidpoint.localPosition = new Vector3(0, 0, localPosition.z);
        }
    }

    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    private void HandleStringAtMaxDraw(float absZ, Vector3 localPosition)
    {
        if (localPosition.z < 0f && absZ >= maxDrawDistance)
        {
            currentDrawStrength = 1f;
            visualMidpoint.localPosition = new Vector3(0, 0, -maxDrawDistance);
        }
    }

    private void HandleStringAtStartPosition(Vector3 localPosition)
    {
        if (localPosition.z >= 0f)
        {
            currentDrawStrength = 0f;
            visualMidpoint.localPosition = Vector3.zero;
        }
    }
}