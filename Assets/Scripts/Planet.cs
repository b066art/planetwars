using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planet : MonoBehaviour
{
    [SerializeField]
    public int population, maxPopulation, playerPopInSec, enemyPopInSec;

    TMP_Text textBox;
    float TimeInterval;

    public bool neutral, player, enemy;

    void Start()
    {
        textBox = this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();

        PlanetSelection.Instance.planetList.Add(this.gameObject);

        if (PlanetSelection.Instance.planetList.Count == GameManager.gameMng.amountOfPlanets)
        {
            PlanetSelection.Instance.SetPlanets();
        }

        InvokeRepeating("IncreasePlayerPopulation", 1f / playerPopInSec, 1f / playerPopInSec);
        InvokeRepeating("IncreaseEnemyPopulation", 1f / enemyPopInSec, 1f / enemyPopInSec);
    }

    void Update()
    {
        if (population >= maxPopulation)
        {
            textBox.text = maxPopulation.ToString();
            population = maxPopulation;
        }
        
        textBox.text = population.ToString();
    }

    void IncreasePlayerPopulation()
    {
        if (PlanetSelection.Instance.playerPlanetList.Contains(this.gameObject))
        {
            population++;
        }
    }

    void IncreaseEnemyPopulation()
    {
        if (PlanetSelection.Instance.enemyPlanetList.Contains(this.gameObject))
        {
            population++;
        }
    }

    public void SetPopulation(int pop)
    {
        population = pop;
    }
}
