using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipController : MonoBehaviour
{
    public PlayerData playerData;
    public float movementSpeed = 5f;

    public GameObject bulletPrefab;
    public Transform barrel;

    private Rigidbody2D myRb;
    private InputSystem_Actions playerInputs;

    private bool isShooting = false;
    private bool isAccelerating = false;

    private float fireRate = 0.25f;
    private float nextFireTime = 0f;

    public TrailRenderer burnerTrail;

    private Vector2 movementDirection = Vector2.zero;

    private void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        playerData = PlayerDataManager.Instance.playerData;
        playerData.ResetStats();
    }

    private void OnEnable()
    {
        if (playerInputs == null)
            playerInputs = new InputSystem_Actions();

        playerInputs.Enable();
        playerInputs.Player.Enable();

        playerInputs.Player.shoot.performed += ctx => isShooting = true;
        playerInputs.Player.shoot.canceled += ctx => isShooting = false;

        playerInputs.Player.Accelerate.performed += ctx => isAccelerating = true;
        playerInputs.Player.Accelerate.canceled += ctx => isAccelerating = false;

        playerInputs.Player.Look.performed += Lookperformed;
        playerInputs.Player.Look.canceled += Lookperformed;
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }

    private void Lookperformed(InputAction.CallbackContext context)
    {
        movementDirection = context.ReadValue<Vector2>().normalized;
    }

    private void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (movementDirection.sqrMagnitude > 0.01f)
        {
            transform.up = Vector2.Lerp(transform.up, movementDirection, Time.deltaTime * 10f);
        }
    }

    private void FixedUpdate()
    {
        if (!playerData.isAlive)
        {
            this.gameObject.SetActive(false);
            return;
        }

        if (isAccelerating && playerData.currentFuel > 0)
        {
            myRb.linearVelocity = transform.up * movementSpeed;
            playerData.ConsumeFuel(Time.fixedDeltaTime * 10f);
            if (burnerTrail != null) burnerTrail.emitting = true;
        }
        else
        {
            if (burnerTrail != null) burnerTrail.emitting = false;
        }

        if (playerData.currentFuel <= 0)
        {
            playerData.ConsumeFuel(0f); // continue burning timeLeft
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, barrel.position, barrel.rotation);
    }

    public void TakeDamage(float damage)
    {
        playerData.TakeDamage(damage);
        if (!playerData.isAlive)
        {
            playerData.SaveData();
            this.gameObject.SetActive(false);
        }
    }
}
