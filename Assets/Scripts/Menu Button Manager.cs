using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    public TextMeshProUGUI fuelReserves;
    public TextMeshProUGUI health;
    public TextMeshProUGUI spaceDustCollected;
    public TextMeshProUGUI fuelefficiency;
    public TextMeshProUGUI fuelcollector;

    public TextMeshProUGUI fuelReservesPrice;
    public TextMeshProUGUI healthPrice;
    public TextMeshProUGUI fuelefficiencyPrice;
    public TextMeshProUGUI fuelcollectorPrice;

    public GameObject mainscreenpanel;
    public GameObject upgradescreenpanel;

    PlayerData PlayerData; // Reference to the PlayerData component
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerData = PlayerDataManager.Instance.playerData; // Get the player data from the PlayerDataManager
        fuelReserves.text = "Fuel Reserves: " + PlayerData.maxFuel.ToString("F2") + " L"; // Display max fuel reserves
        health.text = "Health: " + PlayerData.maxHealth.ToString("F2") + " HP"; // Display max health
        spaceDustCollected.text = "Space Dust Collected: " + PlayerData.spacedust.ToString(); // Display collected space dust
        fuelefficiency.text = "Fuel Efficiency: " + PlayerData.fuelConsumptionRate.ToString("F2") + " L/s"; // Display fuel efficiency
        fuelcollector.text = "Fuel Collector: " + PlayerData.CollectableFuelAmount.ToString("F2") + " L"; // Display fuel collector capacity

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onstartbutton()
    {
        SceneManager.LoadScene("SpaceScene"); // Load the main game scene when the start button is pressed
    }

    public void onupgradebutton()
    {
        mainscreenpanel.SetActive(false); // Hide the main screen panel
        upgradescreenpanel.SetActive(true); // Show the upgrade screen panel
    }

    public void onbackbutton()
    {
        mainscreenpanel.SetActive(true); // Show the main screen panel
        upgradescreenpanel.SetActive(false); // Hide the upgrade screen panel
    }

    public void onexitbutton()
    {
        Application.Quit(); // Exit the application when the exit button is pressed
        Debug.Log("Exit button pressed, application will close."); // Log message for debugging
    }

    public void onfuelreservesbutton()
    {
        PlayerData.maxFuel += 10f; // Increase max fuel reserves by 10 when the button is pressed
        fuelReserves.text = "Fuel Reserves: " + PlayerData.maxFuel.ToString("F2") + " L"; // Update the displayed fuel reserves
    }

    public void onhealthbutton()
    {
        PlayerData.maxHealth += 10f; // Increase max health by 10 when the button is pressed
        health.text = "Health: " + PlayerData.maxHealth.ToString("F2") + " HP"; // Update the displayed health
    }

    public void onfuelefficiancybutton()
    {
        if(PlayerData.fuelConsumptionRate > 2f)
        {
            PlayerData.fuelConsumptionRate -= 0.5f; // Decrease fuel consumption rate by 1 when the button is pressed
            fuelefficiency.text = "Fuel Efficiency: " + PlayerData.fuelConsumptionRate.ToString("F2") + " L/s"; // Update the displayed fuel efficiency
        }
        else
        {
            Debug.LogWarning("Fuel efficiency cannot be reduced further."); // Log warning if fuel efficiency is already at minimum
        }
    }

    public void onfuelcollectorbutton()
    {
        PlayerData.CollectableFuelAmount += 5f; // Increase fuel collector capacity by 5 when the button is pressed
        fuelcollector.text = "Fuel Collector: " + PlayerData.CollectableFuelAmount.ToString("F2") + " L"; // Update the displayed fuel collector capacity
    }
}
