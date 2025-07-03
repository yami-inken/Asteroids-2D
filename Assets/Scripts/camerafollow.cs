using UnityEngine;

public class CameraFollowWithMargins : MonoBehaviour
{
    public Transform target;             // Your spaceship
    public Vector2 margin = new Vector2(9f, 4f); // Distance from screen edge before camera moves
    public float smoothSpeed = 5f;       // How fast the camera catches up

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 camPos = transform.position;
        Vector3 targetPos = target.position;

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        Vector3 delta = targetPos - camPos;

        Vector3 newCamPos = camPos;

        // Horizontal movement
        if (Mathf.Abs(delta.x) > margin.x)
        {
            newCamPos.x = Mathf.Lerp(camPos.x, targetPos.x - Mathf.Sign(delta.x) * margin.x, Time.deltaTime * smoothSpeed);
        }

        // Vertical movement
        if (Mathf.Abs(delta.y) > margin.y)
        {
            newCamPos.y = Mathf.Lerp(camPos.y, targetPos.y - Mathf.Sign(delta.y) * margin.y, Time.deltaTime * smoothSpeed);
        }

        // Keep camera z-position unchanged (usually -10)
        newCamPos.z = camPos.z;

        transform.position = newCamPos;
    }

    private void OnDrawGizmos()
    {
        if (cam == null)
            cam = Camera.main; // Ensure we have the camera

        if (target == null) return;

        // Camera center position
        Vector3 camPos = transform.position;

        // Get camera extents
        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        // Set color for the margin box
        Gizmos.color = Color.yellow;

        // Draw a rectangle representing the margin area
        Gizmos.DrawWireCube(
            camPos,
            new Vector3(margin.x * 2, margin.y * 2, 0)
        );
    }
}
