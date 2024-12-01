using System;
using Oculus.Interaction;
using Oculus.Interaction.DistanceReticles;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using UnityEngine;

public class ReticleDataMeshImprover : MonoBehaviour
{
    public ReticleDataMesh secondHandReticleDataMesh;
    public HandGrabInteractable handGrabInteractable;

    // Update is called once per frame

    private void Update()
    {
        if (handGrabInteractable.Interactors.Count > 0)
        {
            secondHandReticleDataMesh.enabled = false;
            //foreach (IInteractorView interactor in handGrabInteractable.Interactors)
            //{
            //    // Sprawd�, czy interaktor jest typu HandGrabInteractor
            //    if (interactor is HandGrabInteractor handGrabInteractor)
            //    {
            //        // Pobierz informacj� o r�ce
            //        Handedness hand = handGrabInteractor.HandGrabApi.Hand.Handedness;
            //        Debug.Log("Obiekt jest trzymany przez: " + hand.ToString());
            //    }
            //    // Dla interakcji na odleg�o��
            //    else if (interactor is DistanceHandGrabInteractor distanceHandGrabInteractor)
            //    {
            //        Handedness hand = distanceHandGrabInteractor.HandGrabApi.Hand.Handedness;
            //        Debug.Log("Obiekt jest trzymany na odleg�o�� przez: " + hand.ToString());
            //    }
            //}
        } else
        {
            secondHandReticleDataMesh.enabled = true;
        }
    }

    private void OnGrabEvent(PointerEvent pointerEvent)
    {
        

        
        if (pointerEvent.Type == PointerEventType.Select)
        {
            
            secondHandReticleDataMesh.enabled = false;
        }
        else if (pointerEvent.Type == PointerEventType.Unselect)
        {
            secondHandReticleDataMesh.enabled = true;

        }
    }
}
