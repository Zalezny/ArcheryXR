using UnityEngine;

public class HapticFeedback : MonoBehaviour
{
    public void TriggerVibration(float time)
    {
        // Ustawienie drga� na maksymaln� cz�stotliwo�� i amplitud�
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch); // Dla prawego kontrolera
        Invoke(nameof(StopVibration), time); // Zatrzymaj drganie po 0.5 sekundy
    }

    private void StopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch); // Zatrzymaj drganie
    }
}
