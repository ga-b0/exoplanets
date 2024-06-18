using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public class DataFiltered : MonoBehaviour
{
    public string originalFilePath = "Data/fetch.json";
    public string filteredFilePath = "Data/fetchFiltered.json";

    void Update()
    {
        if (File.Exists(Path.Combine(Application.dataPath, originalFilePath)) && !File.Exists(Path.Combine(Application.dataPath, filteredFilePath)))
        {
            string jsonData = File.ReadAllText(Path.Combine(Application.dataPath, originalFilePath));
            List<Exoplanet> exoplanets = JsonConvert.DeserializeObject<List<Exoplanet>>(jsonData);
            List<Exoplanet> filteredExoplanets = FilterExoplanets(exoplanets);
            string filteredJson = JsonConvert.SerializeObject(filteredExoplanets, Formatting.Indented);
            File.WriteAllText(Path.Combine(Application.dataPath, filteredFilePath), filteredJson);
            Debug.Log("Archivo JSON filtrado guardado en: " + Path.Combine(Application.dataPath, filteredFilePath));
        }
    }

    private List<Exoplanet> FilterExoplanets(List<Exoplanet> exoplanets)
    {
        List<Exoplanet> filteredList = new List<Exoplanet>();

        foreach (var exoplanet in exoplanets)
        {
            if (exoplanet.koi_prad != null && exoplanet.koi_disposition == "CONFIRMED")
            {
                filteredList.Add(exoplanet);
            }
        }

        return filteredList;
    }

    public class Exoplanet
    {
        public string kepler_name { get; set; }
        public float? koi_prad { get; set; }
        public string koi_disposition { get; set; }
    }
}
