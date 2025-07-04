using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject player;

    SpaceSHip spaceship; // Reference to the SpaceSHip component

    [SerializeField]
    public Slider healthSL; // Reference to the UI slider for health
    public Slider FuelSL; // Reference to the UI slider for mana

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            spaceship = player.GetComponent<SpaceSHip>();
            if (spaceship != null)
            {
                healthSL.value = spaceship.spaceShipHealth; // Initialize health slider
                FuelSL.value = spaceship.movementSpeed; // 
            }
            else
            {
                Debug.LogError("SpaceSHip component not found on player object.");
            }
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthSL.value = spaceship.spaceShipHealth; // Initialize health slider
        FuelSL.value = spaceship.Fuel;
    }
}
