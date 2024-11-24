using System;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class BowController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;
   
    private Grabbable interactable;
    
    [SerializeField]
    private Transform midPointGrabObject, midPointVisualObject, midPointParent;

    [SerializeField]
    private float bowStringStretchLimit = 0.35f;

    private Transform interactor;

    private float strength;

    public UnityEvent onBowPulled;
    public UnityEvent<float> onBowReleased;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<Grabbable>();
    }

    private void OnGrabEvent(PointerEvent pointerEvent)
    {
        if(pointerEvent.Type == PointerEventType.Select)
        {
            PrepareBowString(pointerEvent);

        }
        else if(pointerEvent.Type == PointerEventType.Unselect) {
           ResetBowString();
        }
    }

    private void Start()
    {
        interactable.WhenPointerEventRaised += OnGrabEvent;
    }

    private void OnDestroy()
    {
        interactable.WhenPointerEventRaised -= OnGrabEvent;
    }

    private void PrepareBowString(PointerEvent pointerEvent)
    {
        interactor = interactable.transform;

        onBowPulled?.Invoke();
        
    }

    private void ResetBowString()
    {
        onBowReleased?.Invoke(strength);
        strength = 0.0f;

        interactor = null;
        midPointGrabObject.localPosition = Vector3.zero;
        midPointVisualObject.localPosition = Vector3.zero;
        bowStringRenderer.CreateString(null);
    }

    private void Update()
    {
        if (interactor != null)
        {
            Vector3 midPointLocalSpace = midPointParent.InverseTransformPoint(midPointGrabObject.position);
            float midPointLocalZAbs = Mathf.Abs(midPointLocalSpace.z);


            HandleStringPushedBackToStart(midPointLocalSpace);

            HandleStringPulledBackToLimit(midPointLocalZAbs, midPointLocalSpace);

            HandlePullingString(midPointLocalZAbs, midPointLocalSpace);


            bowStringRenderer.CreateString(midPointVisualObject.position);
        }
    }

    private void HandlePullingString(float midPointLocalZAbs, Vector3 midPointLocalSpace) { 
        if (midPointLocalSpace.z < 0f && midPointLocalZAbs < bowStringStretchLimit)
        {
            strength = Remap(midPointLocalZAbs, 0, bowStringStretchLimit, 0, 1);
            midPointVisualObject.localPosition = new Vector3(0, 0, midPointLocalSpace.z);
        }
    }

    private float Remap(float value, int fromMin, float fromMax, int toMin, int toMax)
    {
        return (value - fromMin) / (fromMax - fromMin)* (toMax - toMin) + toMin;
    }

    private void HandleStringPulledBackToLimit(float midPointLocalZAbs, Vector3 midPointLocalSpace) {
        if (midPointLocalSpace.z < 0f && midPointLocalZAbs >= bowStringStretchLimit)
        {
            strength = 1;
            midPointVisualObject.localPosition = new Vector3(0, 0, -bowStringStretchLimit);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace) {
        if(midPointLocalSpace.z >= 0f)
        {
            strength = 0;
            midPointVisualObject.localPosition = Vector3.zero;
        }
    }
}
