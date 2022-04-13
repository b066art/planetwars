using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlanets : MonoBehaviour
{
    [SerializeField]
    GameObject[] planetPrefab = new GameObject[3]; 
    [SerializeField]
    GameObject textBoxPrefab;
    [SerializeField]
    private float minScale, maxScale;

    private int arrayLength;
    private float xMin, xMax, yMin, yMax;
    
    void Start()
    {
        SetBorders();
        arrayLength = planetPrefab.Length;
    }

    private void SetBorders()
    {
        xMin = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0)).x;
        xMax = Camera.main.ViewportToWorldPoint (new Vector2 (1, 0)).x;
        yMin = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0)).y;
        yMax = Camera.main.ViewportToWorldPoint (new Vector2 (0, 1)).y;
    }

    public void SpawnPlanet(int amountOfPlanets)
    {
        for (int i = 0; i < amountOfPlanets; i++)
            NewPos();
    }

    private void NewPos()
    {
        int rnd = Random.Range(0, arrayLength);
        float scale = Random.Range(minScale, maxScale);
        float localRad = planetPrefab[rnd].GetComponent<CircleCollider2D>().radius;
        float worldRad = localRad * scale;
        float x = Random.Range(xMin + worldRad + 1, xMax - worldRad - 1);
        float y = Random.Range(yMin + worldRad + 1, yMax - worldRad - 1);
        Vector3 spawnPos = new Vector3(x, y, 0);

        if (CheckPos(spawnPos, worldRad))
        {
            GameObject Planet = Instantiate(planetPrefab[Random.Range(0, arrayLength)], spawnPos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Planet.transform.localScale = new Vector3(scale, scale, 1);
            GameObject TextBox = Instantiate(textBoxPrefab, spawnPos, Quaternion.identity);
            TextBox.transform.SetParent(Planet.transform);
        }
        else
        {
            NewPos();
        }
    }

    bool CheckPos(Vector3 spawnPos, float worldRad)
    {
        bool canSpawn = true;
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject planet in planets)
        {
            float radSum = worldRad + (planet.GetComponent<CircleCollider2D>().radius * planet.transform.localScale.x);
            float distance = (spawnPos - planet.transform.position).magnitude - radSum;
            if (distance < radSum)
            {
                canSpawn = false;
                break;
            }
        }
        return canSpawn;
    }
}
