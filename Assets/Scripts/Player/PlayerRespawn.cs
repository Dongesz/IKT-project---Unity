using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Csak a j�t�kos aktiv�lja
        {
            other.transform.position = respawnPoint.position; // Visszarakja a spawn pontra
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Null�zza a sebess�get, hogy ne essen tov�bb
            }
        }
    }
}
