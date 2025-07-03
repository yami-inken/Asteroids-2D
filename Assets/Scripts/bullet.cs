using UnityEngine;

public class bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceMagnitude = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.up * forceMagnitude;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
