using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Exoplanet : MonoBehaviour
{
    [Header("Radio del Planeta")]
    public float radius = 1.0f;

    [Header("Objeto para Spawnear el Planeta")]
    public Transform spawPoint;
    private ManageExo manageExo;
    [Header("Numero de Planetas a Mostrar")]
    [Range(0, 100)]
    public int numMaxPlanets = 10;

    [Header("Campos UI")]
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI radioText;

    [Header("Materiales de los Planetas")]
    public Material[] materialsPlanets;
    List<ManageExo.Planet> planetsSelected = new List<ManageExo.Planet>();
    private int numPlanet = 0;
    private GameObject sphere;
    private float planetRadius;
    private void Start()
    {

        manageExo = FindObjectOfType<ManageExo>();

        for (int i = 0; i < numMaxPlanets; i++) {

            planetsSelected.Add(manageExo.planets[i]);
            
        }

        if (manageExo != null && manageExo.planets.Count > 0)
        {
            textMeshPro.text = manageExo.planets[numPlanet].kepler_name;
            Debug.Log("Count of planets selected: " + planetsSelected.Count);
            planetRadius = manageExo.planets[numPlanet].koi_prad ?? radius;
            radioText.text = "Radio: " + planetRadius.ToString() + "\nRadio respecto al de la Tierra";
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (materialsPlanets.Length > 0)
            {
                int randomIndex = Random.Range(0, materialsPlanets.Length);
                sphere.GetComponent<MeshRenderer>().material = materialsPlanets[randomIndex];
            }
            sphere.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
            sphere.transform.position = spawPoint.position;
        }
        else
        {
            Debug.LogError("No se encontró ManageExo o la lista de planetas está vacía.");
        }
    }

    public void NextPlanet() {

        if (numPlanet == planetsSelected.Count) {
            return;
        }

        numPlanet++;
        textMeshPro.text = manageExo.planets[numPlanet].kepler_name;
        planetRadius = manageExo.planets[numPlanet].koi_prad ?? radius;
        radioText.text = "Radio: " + planetRadius.ToString() + "\nRadio respecto al de la Tierra";
        if (materialsPlanets.Length > 0)
        {
            int randomIndex = Random.Range(0, materialsPlanets.Length);
            sphere.GetComponent<MeshRenderer>().material = materialsPlanets[randomIndex];
        }
        sphere.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
        sphere.transform.position = spawPoint.position;
    }


    public void PreviousPlanet() {

        if (numPlanet <= 0)
        {
            return;
        }

        numPlanet--;
        textMeshPro.text = manageExo.planets[numPlanet].kepler_name;
        planetRadius = manageExo.planets[numPlanet].koi_prad ?? radius;
        radioText.text = "Radio: " + planetRadius.ToString() + "\nRadio respecto al de la Tierra";
        if (materialsPlanets.Length > 0)
        {
            int randomIndex = Random.Range(0, materialsPlanets.Length);
            sphere.GetComponent<MeshRenderer>().material = materialsPlanets[randomIndex];
        }
        sphere.transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
        sphere.transform.position = spawPoint.position;
    }


}
