using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject player;

    SpaceSHip spaceship; // Reference to the SpaceSHip component

    [SerializeField]
    public Slider healthSL; // Reference to the UI slider for health
    public Slider FuelSL; // Reference to the UI slider for mana
    public GameObject timeleftpanel; // Reference to the UI panel for time left
    public TextMeshProUGUI timelefttext; // Reference to the UI text for time left
    public TextMeshProUGUI spacedust_collectedTEXT; // Reference to the UI text for space dust collected
    public GameObject Gameoverpanel; // Reference to the game over panel
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
        spacedust_collectedTEXT.text = "Space Dust :- " + spaceship.Spacedust.ToString(); // Update space dust collected text
        if (spaceship.Fuel <= 0f)
        {
            timeleftpanel.SetActive(true); // Show time left panel if fuel is empty
            timelefttext.text = "Time Left: " + spaceship.timeleft.ToString("F2") + "s"; // Update time left text
            if(spaceship.timeleft < 0f)
            {
                timeleftpanel.SetActive(false); // Hide time left panel if time is up
            }
        }
        else
        {
            timeleftpanel.SetActive(false); // Hide time left panel if there is fuel
            spaceship.timeleft = spaceship.maxtimeleft; // Reset time left if there is fuel
        }

        if(spaceship.isalive == false)
        {
            Gameoverpanel.SetActive(true); // Show game over panel if spaceship is not alive
        }
    }

    public void onretry()
    {
        Gameoverpanel.SetActive(false); // Hide game over panel
        spaceship.isalive = true; // Reset spaceship alive status
        spaceship.spaceShipHealth = spaceship.maxSpaceShipHealth; // Reset spaceship health
        spaceship.Fuel = spaceship.maxFuel; // Reset spaceship fuel
        spaceship.Spacedust = 0f; // Reset space dust collected
        spaceship.timeleft = spaceship.maxtimeleft; // Reset time left
        player.transform.position = Vector3.zero; // Reset player position to origin
        player.SetActive(true); // Reactivate player object
    }

    public void onmenu()
    {
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }
}
