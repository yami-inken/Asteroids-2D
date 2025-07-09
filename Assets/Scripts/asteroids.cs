using Unity.VisualScripting;
using UnityEngine;

public class asteroids : MonoBehaviour
{
    public GameObject target;

    public float speed = 4f;

    private Rigidbody2D rb;

    public float health = 1f; // Health of the asteroid

    public Sprite[] asteroidSprites; // Array of asteroid sprites

    public float trackingStopDistance = 2f;

    [SerializeField] private bool isTracking = true; // Flag to check if the asteroid is tracking the player

    SpaceSHip spaceship; // Reference to the SpaceSHip component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Sprite randomSprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];
        GetComponent<SpriteRenderer>().sprite = randomSprite;
        spaceship = target.GetComponent<SpaceSHip>(); // Get the SpaceSHip component from the player
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player"); // Find the player object by tag
            spaceship = target.GetComponent<SpaceSHip>(); // Get the SpaceSHip component from the player
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null || !isTracking) return;

        Vector2 direction = (Vector2)target.transform.position - rb.position;
        float distance = direction.magnitude;

        if (distance > trackingStopDistance)
        {
            //Debug.Log("Asteroid tracking player, moving towards them.");
            //Debug.Log("Distance to player: " + distance);
            direction.Normalize();
            Vector2 newPosition = rb.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
        else
        {
            //Debug.Log("Asteroid stopped tracking player, moving with physics now.");
            isTracking = false; // Stop AI tracking, let physics take over
            rb.linearVelocity = direction.normalized * speed;
        }
    }

    private void OnBecameInvisible()
    {
        //isTracking = true;
        Destroy(gameObject); // Destroy the asteroid when it goes off-screen
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with player

            Debug.Log("Asteroid collided with player!");
            collision.gameObject.GetComponent<SpaceSHip>().TakeDamage(10f);
            // You can add more logic here, like damaging the player or destroying the asteroid
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            // Handle collision with another asteroid
            Debug.Log("Asteroid collided with another asteroid!");
            // You can add more logic here, like bouncing off or destroying one of the asteroids
        }
        else if (collision.gameObject.CompareTag("Bullet"))
        {
            // Handle collision with bullet
            Debug.Log("Asteroid hit by bullet!");
            Destroy(collision.gameObject);
            health -= 1f; // Decrease health by 1 on bullet hit
            if (health <= 0)
            {
                Destroy(gameObject); // Destroy the asteroid on bullet hit
                int randomDrop = Random.Range(0, 6);
                if (randomDrop == 0)
                {
                    spaceship.Fuel = spaceship.Fuel + 5f;
                }
                else if (randomDrop == 1)
                {
                    spaceship.spaceShipHealth = spaceship.spaceShipHealth + 5f;
                }
                else
                {
                    // Add other drops here
                    //Debug.Log("Asteroid dropped something else!");
                    spaceship.Spacedust = spaceship.Spacedust + Random.Range(0, 5); // Increment spacedust by 1 on asteroid destruction
                }
            }
        }
        else
        {
            // Handle collision with other objects
            Debug.Log("Asteroid collided with: " + collision.gameObject.name);
        }
    }

    private void OnDestroy()
    {
        int randomDrop = Random.Range(0, 6);
        if (randomDrop == 0)
        {
            spaceship.Fuel = spaceship.Fuel + 5f;
        }
        else if (randomDrop == 1)
        {
            if (spaceship.Fuel < 0f)
            {
                spaceship.Fuel = spaceship.Fuel + 10f;
            }
            spaceship.spaceShipHealth = spaceship.spaceShipHealth + 5f;
        }
        else
        {
            // Add other drops here
            //Debug.Log("Asteroid dropped something else!");
            spaceship.Spacedust = spaceship.Spacedust + Random.Range(1, 6); // Increment spacedust by 1 on asteroid destruction
        }
    }
}