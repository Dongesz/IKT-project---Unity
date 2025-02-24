using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    [SerializeField] private Transform cam;      // A playerCam (vagy a kamera child-objektum)
    [SerializeField] private float maxTilt = 10; // Max d�l�ssz�g fokban
    [SerializeField] private float tiltSpeed = 5; // Milyen gyorsan �rje el a c�l d�l�st

    private float currentTilt;

    void Update()
    {
        // P�lda: a horizont�lis bemenet alapj�n d�nts�k a kamer�t
        float horizontalInput = Input.GetAxis("Horizontal");

        // C�l d�l�s: ha balra megy�nk, d�ntse kicsit balra, ha jobbra, jobbra
        float targetTilt = -horizontalInput * maxTilt;

        // Folyamatos �tmenet a jelenlegi �s a c�l d�l�s k�z�tt
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        // Alkalmazzuk a Z tengely k�r�li elforgat�st
        cam.localRotation = Quaternion.Euler(cam.localRotation.eulerAngles.x,
                                             cam.localRotation.eulerAngles.y,
                                             currentTilt);
    }
}