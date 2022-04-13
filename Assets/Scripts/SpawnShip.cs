using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShip : MonoBehaviour
{
    public static SpawnShip spSh;

    [SerializeField]
    GameObject shipPrefab;

    private float xMin, xMax, yMin, yMax;

    void Awake()
    {
        spSh = this;
    }

    private void SetBorders(GameObject planet)
    {
        float localRad = planet.GetComponent<CircleCollider2D>().radius;
        float worldRad = localRad * planet.transform.localScale.x;

        xMin = planet.transform.position.x - worldRad;
        xMax = planet.transform.position.x + worldRad;
        yMin = planet.transform.position.y - worldRad;
        yMax = planet.transform.position.y + worldRad;
    }

    public void Spawn(int amount, GameObject playerPlanet, GameObject target)
    {
        SetBorders(playerPlanet);

        for (int i = 0; i < amount; i++)
        {
            float x = Random.Range(xMin, xMax);
            float y = Random.Range(yMin, yMax);
  
            Vector3 spawnPos = new Vector3(x, y, 0);
            Vector3 vectorToTarget = target.transform.position - spawnPos;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;
            GameObject Ship = Instantiate(shipPrefab, spawnPos, Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget));
            StarShip.starShip.SetTarget(target);
        
            if (PlanetSelection.Instance.playerPlanetList.Contains(playerPlanet))
            {
                StarShip.starShip.player = true;
            }
            if (PlanetSelection.Instance.enemyPlanetList.Contains(playerPlanet))
            {
                StarShip.starShip.enemy = true;
            }
        }
    }
}
