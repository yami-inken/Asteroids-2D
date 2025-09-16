using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [System.Serializable]
    public class PlanetData
    {
        public string name;
        public float orbitRadius;
        public float orbitSpeed;
        [TextArea] public string questText;
        public Sprite planetSprite;
    }

    public PlanetData[] planets;
    public GameObject planetPrefab;
    public Transform sun;

    void Start()
    {
        foreach (var data in planets)
        {
            GameObject planet = Instantiate(planetPrefab, sun.position, Quaternion.identity);
            planet.name = data.name;

            // Set sprite
            SpriteRenderer sr = planet.GetComponent<SpriteRenderer>();
            if (sr != null && data.planetSprite != null)
                sr.sprite = data.planetSprite;

            // Apply planet settings
            PlanetController pc = planet.GetComponent<PlanetController>();
            pc.sun = sun;
            pc.planetName = data.name;
            pc.orbitRadius = data.orbitRadius;
            pc.orbitSpeed = data.orbitSpeed;
            pc.questText = data.questText;
        }
    }
}
