using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Networking;

public class ManageExo : MonoBehaviour
{
    private string jsonFileName = "fetchFiltered.json";
    public List<Planet> planets;

    private void Awake()
    {
        StartCoroutine(LoadJson());
    }

    private IEnumerator LoadJson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFileName);

#if UNITY_WEBGL
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error JSON: " + www.error);
        }
        else
        {
            string jsonData = www.downloadHandler.text;
            planets = JsonConvert.DeserializeObject<List<Planet>>(jsonData);
            Debug.Log("Count: " + planets.Count);
        }
#else

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            planets = JsonConvert.DeserializeObject<List<Planet>>(jsonData);
            Debug.Log("Count: " + planets.Count);
        }
        else
        {
            Debug.LogError("Error JSON: " + filePath);
        }
#endif
    }

    public class Planet
    {
        public string kepler_name { get; set; }
        public float? koi_prad { get; set; }
    }
}
