using UnityEngine;

/// <summary>
/// Zarz¹dza wibracjami kontrolera w celu zapewnienia haptycznego sprzê¿enia zwrotnego.
/// </summary>
public class HapticFeedback : MonoBehaviour
{
    /// <summary>
    /// Uruchamia wibracje na prawym kontrolerze przez okreœlony czas.
    /// </summary>
    /// <param name="time">Czas trwania wibracji w sekundach.</param>
    public void TriggerVibration(float time)
    {
        OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        Invoke(nameof(StopVibration), time);
    }

    /// <summary>
    /// Zatrzymuje wibracje na prawym kontrolerze.
    /// </summary>
    private void StopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
