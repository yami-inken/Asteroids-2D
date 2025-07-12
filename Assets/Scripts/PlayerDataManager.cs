using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    public PlayerData playerData;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (playerData == null)
        {
            playerData = gameObject.AddComponent<PlayerData>();
        }

        playerData.LoadData(); // Load from JSON on start
    }

    private void OnApplicationQuit()
    {
        playerData.SaveData(); // Save when app closes
    }
}