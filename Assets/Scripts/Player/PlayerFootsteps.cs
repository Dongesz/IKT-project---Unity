using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource; // Az AudioSource a hangok lejátszásához
    public AudioClip[] footstepSounds; // A lépéshangok listája
    public AudioClip[] landingSounds; // A földre érkezés hangjai

    public Rigidbody rb; // A karakter Rigidbody-ja
    private bool wasGrounded = true;
    private float stepInterval = 0.5f; // Milyen gyakran legyen lépéshang
    private float stepTimer = 0f; // Időzítő a lépéshez
    private bool isMoving = false; // A karakter mozog-e?

    private PlayerMovement playerMovement;

    [System.Obsolete]
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (playerMovement == null) return;

        // Ellenőrizzük, hogy a karakter mozog-e a Rigidbody sebessége alapján
        isMoving = rb.linearVelocity.magnitude > 0.1f;

        if (isMoving && playerMovement.IsGrounded()) // Csak akkor játssza le, ha a karakter mozog és földön van
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; // Ha nem mozgunk, az időzítő nullázódik
        }

        // Figyeljük, hogy a karakter földre ér-e ugrás után
        if (!wasGrounded && playerMovement.IsGrounded())
        {
            PlayLandingSound();
        }

        // Frissítse az előző állapotot
        wasGrounded = playerMovement.IsGrounded();
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }

    void PlayLandingSound()
    {
        if (landingSounds.Length > 0) // Válasszunk egy véletlenszerű földre érkezés hangot
        {
            int randomIndex = Random.Range(0, landingSounds.Length);
            audioSource.PlayOneShot(landingSounds[randomIndex]);
        }
    }
}
