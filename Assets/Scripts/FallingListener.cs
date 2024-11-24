using UnityEngine;

public class FallingListener : MonoBehaviour
{
    private void Update()
    {
        Vector3 currentPosition = transform.position;

        Debug.Log($"Obecna pozycja obiektu: X= {currentPosition.x}, Y={currentPosition.y},");
    }

}
