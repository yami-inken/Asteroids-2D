using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    //health stats
    public float maxHealth = 100f;
    public float currentHealth;
    public float collectableHealthAmount = 5f; // Amount of health collected from asteroids
    public float HealthupgradeAmount = 10f; // Amount of health increased per upgrade
    public float HealthupgradeCost = 100f; // Cost of health upgrade in spacedust

    public float maxFuel = 100f;
    public float currentFuel;
    public float fuelConsumptionRate = 10f; // Fuel consumed per second
    public float CollectableFuelAmount = 5f; // Amount of fuel collected from asteroids
    public float FuelupgradeAmount = 10f; // Amount of fuel increased per upgrade
    public float FuelupgradeCost = 100f; // Cost of fuel upgrade in spacedust

    public float maxTimeWithoutFuel = 30f;
    public float currentTimeLeft;

    public float spacedust = 0f;
    public int minpsacedustdrop = 1;
    public int maxspacedustdrop = 5; // Maximum spacedust drop amount

    public bool isAlive = true;

    private string saveFilePath => Path.Combine(Application.persistentDataPath, "playerdata.json");
    private string defaultSaveFilePath => Path.Combine(Application.dataPath, "Defaultplayerdata.json");

    private void Awake()
    {
        LoadData();
        ResetStats();
    }

    public void ResetStats()
    {
        currentHealth = maxHealth;
        currentFuel = maxFuel;
        currentTimeLeft = maxTimeWithoutFuel;
        isAlive = true;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }

    public void ConsumeFuel(float amount)
    {
        currentFuel -= amount;
        if (currentFuel <= 0)
        {
            currentTimeLeft -= Time.deltaTime;
            if (currentTimeLeft <= 0)
            {
                isAlive = false;
            }
        }
    }

    public void AddSpacedust(int amount)
    {
        spacedust += amount;
    }

    public void Drops()
    {
        int randomDrop = Random.Range(0, 6);
        if (randomDrop == 0)
        {
            currentFuel = currentFuel + CollectableFuelAmount;
        }
        else if (randomDrop == 1)
        {
            if (currentFuel < 0f)
            {
                currentFuel = currentFuel + CollectableFuelAmount + 5f;
            }
            currentHealth = currentHealth + collectableHealthAmount;
        }
        else
        {
            // Add other drops here
            //Debug.Log("Asteroid dropped something else!");
            AddSpacedust(Random.Range(minpsacedustdrop, maxspacedustdrop)); // Increment spacedust by 1 on asteroid destruction
        }
    }

    //json save/load methods
    public void SaveData()
    {
        PlayerSaveData saveData = new PlayerSaveData()
        {
            maxHealth = this.maxHealth,
            maxFuel = this.maxFuel,
            spacedust = this.spacedust,
            collectableHealthAmount = this.collectableHealthAmount,
            fuelConsumptionRate = this.fuelConsumptionRate,
            CollectableFuelAmount = this.CollectableFuelAmount,
            minpsacedustdrop = this.minpsacedustdrop,
            maxspacedustdrop = this.maxspacedustdrop,
            maxTimeWithoutFuel = this.maxTimeWithoutFuel,

        };

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Player data saved to: " + saveFilePath);
    }

    public void LoadData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerSaveData saveData = JsonUtility.FromJson<PlayerSaveData>(json);

            this.maxHealth = saveData.maxHealth;
            this.maxFuel = saveData.maxFuel;
            this.spacedust = saveData.spacedust;
            this.collectableHealthAmount = saveData.collectableHealthAmount;
            this.fuelConsumptionRate = saveData.fuelConsumptionRate;
            this.CollectableFuelAmount = saveData.CollectableFuelAmount;
            this.minpsacedustdrop = saveData.minpsacedustdrop;
            this.maxspacedustdrop = saveData.maxspacedustdrop;
            this.maxTimeWithoutFuel = saveData.maxTimeWithoutFuel;

            Debug.Log("Player data loaded from: " + saveFilePath);
        }
        else
        {
            Debug.Log("No save file found. Using default values.");
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted.");
        }
    }

    public void ResetPlayerDataToDefault()
    {
        if (File.Exists(defaultSaveFilePath))
        {
            string json = File.ReadAllText(defaultSaveFilePath);
            PlayerSaveData defaultData = JsonUtility.FromJson<PlayerSaveData>(json);
            this.maxHealth = defaultData.maxHealth;
            this.maxFuel = defaultData.maxFuel;
            this.spacedust = defaultData.spacedust;
            this.collectableHealthAmount = defaultData.collectableHealthAmount;
            this.fuelConsumptionRate = defaultData.fuelConsumptionRate;
            this.CollectableFuelAmount = defaultData.CollectableFuelAmount;
            this.minpsacedustdrop = defaultData.minpsacedustdrop;
            this.maxspacedustdrop = defaultData.maxspacedustdrop;
            this.maxTimeWithoutFuel = defaultData.maxTimeWithoutFuel;
            SaveData(); // Save the reset data to the save file
            Debug.Log("Player data reset to default values.");
        }
        else
        {
            Debug.LogError("Default player data file not found at: " + defaultSaveFilePath);
        }
    }

    //player upgrades
    public void healthupgrade() {
        if(spacedust>=HealthupgradeCost)
        {
            spacedust = spacedust - HealthupgradeCost;
            maxHealth = maxHealth + HealthupgradeAmount;
            HealthupgradeCost = HealthupgradeCost + 100f; // Increase cost for next upgrade
        }
        else
        {
            Debug.LogWarning("Not enough Space Dust to upgrade health!"); // Log warning if not enough space dust
        }
    }
}
