using UnityEngine;

public class DragDropItem : MonoBehaviour
{
    public bool isDragging = false;
    public Rigidbody currentlyDraggedRigidbody;
    private Vector3 offset;
    private int originalLayer;

    [Header("Smooth Movement")]
    public float smoothSpeed = 5f;
    public Transform playerTransform; // A játékos referenciája

    [Header("Pickup Settings")]
    public float pickupDistance = 2f; // Milyen messze legyen a tárgy a játékostól

    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        PlayerReach playerReach = GetComponent<PlayerReach>();

        if (playerReach != null && playerReach.IsRaycastHit())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Rigidbody hitRigidbody = hit.collider.GetComponent<Rigidbody>();
                    if (hitRigidbody != null)
                    {
                        isDragging = true;
                        currentlyDraggedRigidbody = hitRigidbody;

                        originalLayer = currentlyDraggedRigidbody.gameObject.layer;
                        int temporaryLayer = LayerMask.NameToLayer("TemporaryLayer");
                        currentlyDraggedRigidbody.gameObject.layer = temporaryLayer;

                        offset = currentlyDraggedRigidbody.transform.position - hit.point;
                        currentlyDraggedRigidbody.isKinematic = true;
                    }
                }
            }
        }

        if (isDragging && currentlyDraggedRigidbody != null)
        {
            Vector3 targetPosition = playerTransform.position + playerTransform.forward * pickupDistance;
            MoveWithCollisions(targetPosition);

            if (playerTransform != null)
            {
                currentlyDraggedRigidbody.transform.LookAt(new Vector3(
                    playerTransform.position.x,
                    currentlyDraggedRigidbody.transform.position.y,
                    playerTransform.position.z
                ));
            }

            if (Input.GetMouseButtonUp(0))
            {
                currentlyDraggedRigidbody.gameObject.layer = originalLayer;
                isDragging = false;
                currentlyDraggedRigidbody.isKinematic = false;
                currentlyDraggedRigidbody = null;
            }
        }
    }

    private void MoveWithCollisions(Vector3 targetPosition)
    {
        currentlyDraggedRigidbody.MovePosition(Vector3.Lerp(
            currentlyDraggedRigidbody.transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        ));
    }

    // ✅ Getter metódusok, hogy a ChargeThrow script tudjon hivatkozni rá!
    public bool IsDragging()
    {
        return isDragging;
    }

    public Rigidbody GetCurrentlyDraggedObject()
    {
        return currentlyDraggedRigidbody;
    }

    public void ReleaseObject()
    {
        isDragging = false;
        if (currentlyDraggedRigidbody != null)
        {
            currentlyDraggedRigidbody.isKinematic = false;
            currentlyDraggedRigidbody = null;
        }
    }
}
