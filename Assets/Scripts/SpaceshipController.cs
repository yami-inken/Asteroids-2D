using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceSHip : MonoBehaviour
{
    Rigidbody2D myRb;

    public GameObject bulletpf;

    public Transform barrel;

    public Vector2 movementDirection;

    public float movementSpeed;

    InputSystem_Actions playerInputs;

    bool isShooting = false;

    bool isAccelerating = false;

    public float spaceShipHealth = 100f; // Health of the spaceship

    public float fireRate = 0.25f;

    private float nextFireTime = 0f;

    public Slider healthSlider; // Reference to the UI slider for health

    public bool isinteractable = false; // Flag to check if the spaceship is interactable
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        healthSlider.value = spaceShipHealth;
        Debug.Log("Interact enabled: " + playerInputs.Player.Interact.enabled);
        Debug.Log("Bindings: " + playerInputs.Player.Interact.bindings.Count);
    }

    private void OnEnable()
    {
        if (playerInputs == null)
        {
            playerInputs = new InputSystem_Actions();
        }

        playerInputs.Enable();
        playerInputs.Player.Enable(); // ✅ Force Player map to be active

        // Bindings
        playerInputs.Player.shoot.performed += ctx => isShooting = true;
        playerInputs.Player.shoot.canceled += ctx => isShooting = false;

        playerInputs.Player.Interact.performed += interact;

        playerInputs.Player.Look.performed += Lookperformed;
        playerInputs.Player.Look.canceled += Lookperformed;

        playerInputs.Player.Accelerate.performed += ctx => isAccelerating = true;
        playerInputs.Player.Accelerate.canceled += ctx => isAccelerating = false;
    }

    private void interact(InputAction.CallbackContext context)
    {
        Debug.Log("interact pressed");
        if (isinteractable)
        {
            Debug.Log("Interacting with planet");
            SceneManager.LoadScene("planets"); // Load the planet scene when interact is pressed
        }
        else
        {
            Debug.Log("Not interactable with any planet");
        }
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void Lookperformed(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>().normalized;
    }

// Update is called once per frame
void Update()
    {
        //rotation
        transform.up = Vector2.Lerp(transform.up, movementDirection, Time.deltaTime * 10f);

        //fire rate for the gun
        if (isShooting && Time.time >= nextFireTime)
        {
            Debug.Log("Shooting");
            shooting();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void FixedUpdate()
    {
        if (isAccelerating)
        {
            moving();
        }
    }

    void shooting()
    {
        var bullets = Instantiate(bulletpf, barrel.position, barrel.rotation);
    }

    void moving()
    {
        myRb.linearVelocity = movementSpeed * transform.up.normalized * 5f;
    }

    public void TakeDamage(float damage)
    {
        spaceShipHealth -= damage;

        healthSlider.value = spaceShipHealth; // Update the health slider UI

        if (spaceShipHealth <= 0)
        {
          
            Destroy(gameObject); // Destroy the spaceship when health reaches zero
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("planets"))
        {
            isinteractable = true; // Set the spaceship as interactable when colliding with a planet
        }
    }
}
