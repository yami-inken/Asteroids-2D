using System;

[Serializable]
public class PlayerSaveData
{
    public float maxHealth;
    public float collectableHealthAmount;
    public float maxFuel;
    public float fuelConsumptionRate = 10f; // Fuel consumed per second
    public float CollectableFuelAmount; // Amount of fuel collected from asteroids

    public float maxTimeWithoutFuel;
    
    public float spacedust;
    public int minpsacedustdrop;
    public int maxspacedustdrop; // Maximum spacedust drop amount
}
