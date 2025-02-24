using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    [SerializeField] private Transform cam;      // A playerCam (vagy a kamera child-objektum)
    [SerializeField] private float maxTilt = 10; // Max dõlésszög fokban
    [SerializeField] private float tiltSpeed = 5; // Milyen gyorsan érje el a cél dõlést

    private float currentTilt;

    void Update()
    {
        // Példa: a horizontális bemenet alapján döntsük a kamerát
        float horizontalInput = Input.GetAxis("Horizontal");

        // Cél dõlés: ha balra megyünk, döntse kicsit balra, ha jobbra, jobbra
        float targetTilt = -horizontalInput * maxTilt;

        // Folyamatos átmenet a jelenlegi és a cél dõlés között
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        // Alkalmazzuk a Z tengely körüli elforgatást
        cam.localRotation = Quaternion.Euler(cam.localRotation.eulerAngles.x,
                                             cam.localRotation.eulerAngles.y,
                                             currentTilt);
    }
}