using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ManageExo : MonoBehaviour
{
    private string filteredFilePath = "Data/fetchFiltered.json";
    public List<Planet> planets;
    private void Awake()
    {
        string jsonData = File.ReadAllText(Path.Combine(Application.dataPath, filteredFilePath));
        planets = JsonConvert.DeserializeObject<List<Planet>>(jsonData);
        Debug.Log("Count: " + planets.Count);
    }

    void Update()
    {
        /*foreach (var planet in planets)
        {
            Debug.Log($"Kepler Name: {planet.kepler_name}, Koi Prad: {planet.koi_prad}");
        }*/
    }


    public class Planet {

        public string kepler_name { get; set; }
        public float? koi_prad { get; set; }

    }
}
