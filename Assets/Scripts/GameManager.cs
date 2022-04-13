using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameMng;

    [SerializeField]
    private GameObject spawner;

    public int amountOfPlanets;

    void Awake()
    {
        gameMng = this;
    }

    void Start()
    {
        SpawnObjects();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    private void SpawnObjects()
    {
        spawner.GetComponent<SpawnPlanets>().SpawnPlanet(amountOfPlanets);
    }
}
