using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public float maxFuel = 100f;
    public float currentFuel;

    public float maxTimeWithoutFuel = 30f;
    public float currentTimeLeft;

    public float spacedust = 0f;
    public int minpsacedustdrop = 1;
    public int maxspacedustdrop = 5; // Maximum spacedust drop amount

    public bool isAlive = true;

    private string saveFilePath => Path.Combine(Application.persistentDataPath, "playerdata.json");

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
            currentFuel = currentFuel + 5f;
        }
        else if (randomDrop == 1)
        {
            if (currentFuel < 0f)
            {
                currentFuel = currentFuel + 10f;
            }
            currentHealth = currentHealth + 5f;
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
            spacedust = this.spacedust
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
}
