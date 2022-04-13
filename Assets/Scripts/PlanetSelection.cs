using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelection : MonoBehaviour
{
    [SerializeField]
    private int playerStartPopulation;
    [SerializeField]
    private int enemyStartPopulation;
    [SerializeField]
    private int enemyAmount;

    public List<GameObject> planetList = new List<GameObject>();
    public List<GameObject> playerPlanetList = new List<GameObject>();
    public List<GameObject> enemyPlanetList = new List<GameObject>();
    public List<GameObject> neutralPlanetList = new List<GameObject>();
    public List<GameObject> planetsSelected = new List<GameObject>();
    public GameObject target;

    private static PlanetSelection _instance;

    public static PlanetSelection Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetPlanets()
    {
        rndPlayerPlanet();
        rndEnemyPlanet(enemyAmount);

        foreach (GameObject planet in planetList)
        {
            addNeutralPlanet(planet);
        }

        planetList.Clear();
    }

    public void rndPlayerPlanet()
    {
        int rnd = Random.Range(0, planetList.Count);
        planetList[rnd].GetComponent<SpriteRenderer>().color = Color.blue;
        planetList[rnd].GetComponent<Planet>().SetPopulation(playerStartPopulation);
        planetList[rnd].GetComponent<Planet>().player = true;
        playerPlanetList.Add(planetList[rnd]);
        planetList.RemoveAt(rnd);            
    }

    public void rndEnemyPlanet(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int rnd = Random.Range(0, planetList.Count);
            planetList[rnd].GetComponent<SpriteRenderer>().color = Color.red;
            planetList[rnd].GetComponent<Planet>().SetPopulation(enemyStartPopulation);
            planetList[rnd].GetComponent<Planet>().enemy = true;
            enemyPlanetList.Add(planetList[rnd]);
            planetList.RemoveAt(rnd);
        }
    }

    public void addNeutralPlanet(GameObject planetToAdd)
    {
        int pop = Mathf.RoundToInt(planetToAdd.transform.localScale.x * 100);
        planetToAdd.GetComponent<SpriteRenderer>().color = Color.grey;   
        planetToAdd.GetComponent<Planet>().SetPopulation(pop);
        planetToAdd.GetComponent<Planet>().neutral = true;
        neutralPlanetList.Add(planetToAdd);
    }

    public void addPlayerPlanet(GameObject planetToAdd)
    {
        int pop = Mathf.RoundToInt(planetToAdd.transform.localScale.x * 100);
        planetToAdd.GetComponent<SpriteRenderer>().color = Color.blue;   
        playerPlanetList.Add(planetToAdd);
        if (enemyPlanetList.Contains(planetToAdd))
        {
            enemyPlanetList.Remove(planetToAdd);
        }
        else
        {
            neutralPlanetList.Remove(planetToAdd);
        }
    }

    public void addEnemyPlanet(GameObject planetToAdd)
    {
        int pop = Mathf.RoundToInt(planetToAdd.transform.localScale.x * 100);
        planetToAdd.GetComponent<SpriteRenderer>().color = Color.red;   
        enemyPlanetList.Add(planetToAdd);
        if (playerPlanetList.Contains(planetToAdd))
        {
            playerPlanetList.Remove(planetToAdd);
        }
        else
        {
            neutralPlanetList.Remove(planetToAdd);
        }
    }

    public void MouseClickSelect(GameObject planetToSelect)
    {
        DeselectAll();
        planetsSelected.Add(planetToSelect);
        planetToSelect.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void MouseOver(GameObject planetToTarget)
    {
        target = planetToTarget;
        planetToTarget.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void MouseDragSelect(GameObject planetToSelect)
    {
        if (!planetsSelected.Contains(planetToSelect))
        {
            planetsSelected.Add(planetToSelect);
            planetToSelect.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Deselect(GameObject planetToDeselect)
    {

    }

    public void DeselectAll()
    {
        foreach (GameObject planet in planetsSelected)
        {
            planet.transform.GetChild(0).gameObject.SetActive(false);
        }
        planetsSelected.Clear();
    }

    public void DeselectTarget()
    {
        if (target != null)
        {
            target.transform.GetChild(0).gameObject.SetActive(false);
            target = null;
        }
    }
}
