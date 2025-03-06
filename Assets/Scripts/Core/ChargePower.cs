using UnityEngine;

public class ChargeThrow : MonoBehaviour
{
    private bool isCharging = false;
    private float chargePower = 0f;
    private Rigidbody heldObject;

    [Header("Throw Settings")]
    public float minThrowForce = 5f; // Minimális dobási erő
    public float maxThrowForce = 20f; // Maximális dobási erő
    public float chargeSpeed = 10f; // Milyen gyorsan töltődik a dobás ereje

    [Header("References")]
    public Transform playerTransform; // A játékos Transform-ja
    public DragDropItem dragDropItem; // A meglévő DragDropItem script referenciája

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (dragDropItem == null)
        {
            dragDropItem = GetComponent<DragDropItem>(); // Meglévő DragDropItem lekérése
        }
    }

    void Update()
    {
        if (dragDropItem != null && dragDropItem.IsDragging()) // Ha éppen húz valamit
        {
            heldObject = dragDropItem.GetCurrentlyDraggedObject(); // Az éppen tartott tárgy referenciája

            // ✅ Dobás töltése (jobb egérgombbal)
            if (Input.GetMouseButtonDown(1)) // Jobb egérgomb
            {
                isCharging = true;
                chargePower = minThrowForce;
            }

            if (isCharging)
            {
                chargePower += chargeSpeed * Time.deltaTime;
                chargePower = Mathf.Clamp(chargePower, minThrowForce, maxThrowForce);
            }

            // ✅ Labda eldobása, ha elengedi a gombot
            if (Input.GetMouseButtonUp(1))
            {
                ThrowObject();
            }
        }
    }

    // 🔹 Labda eldobása
    void ThrowObject()
    {
        if (heldObject != null)
        {
            isCharging = false;

            heldObject.isKinematic = false; // Fizika visszaállítása

            // ✅ Dobás iránya a játékos előre mutató iránya
            Vector3 throwDirection = playerTransform.forward + Vector3.up * 0.2f; // Kicsit felfelé dobás

            // ✅ Erő alkalmazása a labdára
            heldObject.AddForce(throwDirection * chargePower, ForceMode.Impulse);

            // Elengedés után nullázás
            dragDropItem.ReleaseObject();
            heldObject = null;
            chargePower = 0f;
        }
    }
}
