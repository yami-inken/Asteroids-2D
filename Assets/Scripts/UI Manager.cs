using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject player;

    PlayerData PlayerData; // Reference to the SpaceSHip component

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
            
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }

        PlayerData = PlayerDataManager.Instance.playerData;
        if (PlayerData != null)
        {
            healthSL.value = PlayerData.currentHealth; // Initialize health slider
            FuelSL.value = PlayerData.currentFuel; // 
        }
        else
        {
            Debug.LogError("Player data not accessed.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthSL.value = PlayerData.currentHealth; // Initialize health slider
        FuelSL.value = PlayerData.currentFuel;
        spacedust_collectedTEXT.text = "Space Dust :- " + PlayerData.spacedust.ToString(); // Update space dust collected text
        if (PlayerData.currentFuel <= 0f)
        {
            timeleftpanel.SetActive(true); // Show time left panel if fuel is empty
            timelefttext.text = "Time Left: " + PlayerData.currentTimeLeft.ToString("F2") + "s"; // Update time left text
            if(PlayerData.currentTimeLeft < 0f)
            {
                timeleftpanel.SetActive(false); // Hide time left panel if time is up
            }
        }
        else
        {
            timeleftpanel.SetActive(false); // Hide time left panel if there is fuel
            PlayerData.currentTimeLeft = PlayerData.maxTimeWithoutFuel; // Reset time left if there is fuel
        }

        if(PlayerData.isAlive == false)
        {
            Gameoverpanel.SetActive(true); // Show game over panel if spaceship is not alive
        }
    }

    public void onretry()
    {
        PlayerData.SaveData(); // Save player data before retrying
        Gameoverpanel.SetActive(false); // Hide game over panel
        PlayerData.isAlive = true; // Reset spaceship alive status
        PlayerData.currentHealth = PlayerData.maxHealth; // Reset health to max
        PlayerData.currentFuel = PlayerData.maxFuel; // Reset fuel to max
        PlayerData.currentTimeLeft = PlayerData.maxTimeWithoutFuel; // Reset time left to max
        player.transform.position = Vector3.zero; // Reset player position to origin
        player.SetActive(true); // Reactivate player object
    }

    public void onmenu()
    {
        PlayerData.SaveData(); // Save player data before going to menu
        SceneManager.LoadScene("Main Menu"); // Load the main menu scene
    }
}
