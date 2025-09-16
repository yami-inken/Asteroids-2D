using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [Header("Orbit Settings")]
    public float orbitRadius = 5f;     // Distance from Sun
    public float orbitSpeed = 0.5f;    // Rotation speed
    public Transform sun;              // Center point

    [Header("Planet Info")]
    public string planetName = "Planet";
    [TextArea] public string questText = "Quest details here.";

    private float angle; // Current orbit angle

    void Start()
    {
        if (sun == null)
        {
            GameObject sunObj = GameObject.Find("Sun");
            if (sunObj != null) sun = sunObj.transform;
        }

        // Randomize start angle for variation each run
        angle = Random.Range(0f, 360f);
    }

    void Update()
    {
        if (sun == null) return;

        // Orbit math
        angle += orbitSpeed * Time.deltaTime;
        float x = Mathf.Cos(angle) * orbitRadius;
        float y = Mathf.Sin(angle) * orbitRadius;

        transform.position = new Vector3(x, y, 0) + sun.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Arrived at {planetName}: {questText}");
            // TODO: Open quest UI or trigger event
        }
    }
}
