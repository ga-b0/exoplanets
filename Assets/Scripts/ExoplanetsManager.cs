using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class ExoplanetsManager : MonoBehaviour
{
    public Material[] materialsPlanets;
    private string urlAPI = "https://exoplanetarchive.ipac.caltech.edu/cgi-bin/nstedAPI/nph-nstedAPI?table=cumulative&format=json";

    void Awake()
    {
        Debug.Log("Iniciando");
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(urlAPI))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string jsonData = request.downloadHandler.text;
                Debug.Log(jsonData);

                string filePath = Path.Combine(Application.dataPath, "Data/fetch.json");
                File.WriteAllText(filePath, jsonData);
                Debug.Log("Archivo JSON guardado en: " + filePath);
            }
        }
    }

    public class Exoplanet
    {
        public string kepler_name { get; set; }
        public float koi_prad { get; set; }
    }
}
