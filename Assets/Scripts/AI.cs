using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    float coolDownTimeMin, coolDownTimeMax;

    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> targetList = new List<GameObject>();

    GameObject target;

    private bool canSpawn;

    void Start()
    {
        StartCoroutine(Cooldown());
    }

    void Update()
    {
        if (PlanetSelection.Instance.enemyPlanetList.Count != 0)
        {
            foreach (GameObject planet in PlanetSelection.Instance.enemyPlanetList)
            {
                enemyList.Add(planet);
            }
            foreach (GameObject planet in PlanetSelection.Instance.playerPlanetList)
            {
                targetList.Add(planet);
            }
            foreach (GameObject planet in PlanetSelection.Instance.neutralPlanetList)
            {
                targetList.Add(planet);
            }

            target = targetList[Random.Range(0, targetList.Count)];

            int enemyAmount = Random.Range(0, enemyList.Count + 1);

            if (canSpawn)
            {
                GameObject enemy = enemyList[Random.Range(0, enemyList.Count )];
                SpawnShip.spSh.Spawn(enemy.GetComponent<Planet>().population / 2, enemy, target);
                enemy.GetComponent<Planet>().population = enemy.GetComponent<Planet>().population / 2;
                enemyList.Remove(enemy);
                StartCoroutine(Cooldown());
            }

            enemyList.Clear();
            targetList.Clear();
        }
    }

    private IEnumerator Cooldown()
    {
        float coolDownTime = Random.Range(coolDownTimeMin, coolDownTimeMax);
        canSpawn = false;
        yield return new WaitForSeconds(coolDownTime);
        canSpawn = true;
    }
}
