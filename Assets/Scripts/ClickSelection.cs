using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSelection : MonoBehaviour
{
    [SerializeField]
    float coolDownTime;

    private bool canSpawn = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (PlanetSelection.Instance.planetsSelected.Count == 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && PlanetSelection.Instance.playerPlanetList.Contains(hit.collider.gameObject))
                {
                    PlanetSelection.Instance.MouseClickSelect(hit.collider.gameObject);
                }
                else
                {
                    PlanetSelection.Instance.DeselectAll();
                }
            }
            else if (PlanetSelection.Instance.target != null)
            {
                if (canSpawn)
                {
                    foreach (GameObject planet in PlanetSelection.Instance.planetsSelected)
                    {
                        SpawnShip.spSh.Spawn(planet.GetComponent<Planet>().population / 2, planet, PlanetSelection.Instance.target);
                        planet.GetComponent<Planet>().population = planet.GetComponent<Planet>().population / 2;
                        StartCoroutine(Cooldown());
                    }
                }
            }
            if (PlanetSelection.Instance.planetsSelected.Count != 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && PlanetSelection.Instance.playerPlanetList.Contains(hit.collider.gameObject))
                {
                    PlanetSelection.Instance.DeselectAll();
                    PlanetSelection.Instance.MouseClickSelect(hit.collider.gameObject);
                }
                 if (hit.collider == null)
                {
                    PlanetSelection.Instance.DeselectAll();
                }
            }
        }
    }

    private IEnumerator Cooldown()
    {
        canSpawn = false;
        yield return new WaitForSeconds(coolDownTime);
        canSpawn = true;
    }
}
