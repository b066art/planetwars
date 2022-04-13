using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShip : MonoBehaviour
{
    public static StarShip starShip;

    [SerializeField]
    float shipSpeed;

    public bool player, enemy;

    GameObject target;

    void Awake()
    {
        starShip = this;
    }

    void Update()
    {
        Movement();
    }

    public void SetTarget(GameObject trgt)
    {
        target = trgt;
    }

    void Movement()
    {
        transform.Translate(Vector2.up * shipSpeed * Time.deltaTime);
        Vector3 vectorToTarget = target.transform.position - transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
    }

    private void OnCollisionEnter2D (Collision2D other) 
    {
        if (other.gameObject == target)
        {
            Destroy(gameObject);

            if (target.GetComponent<Planet>().player && player)
            {
                target.GetComponent<Planet>().population++;
            }
            if (target.GetComponent<Planet>().enemy && player)
            {
                target.GetComponent<Planet>().population--;
                if (target.GetComponent<Planet>().population <= 0)
                {
                    PlanetSelection.Instance.addPlayerPlanet(other.gameObject);
                    target.GetComponent<Planet>().player = true;
                    target.GetComponent<Planet>().enemy = false;
                }
            }
            if (target.GetComponent<Planet>().neutral && player)
            {
                target.GetComponent<Planet>().population--;
                if (target.GetComponent<Planet>().population <= 0)
                {
                    PlanetSelection.Instance.addPlayerPlanet(other.gameObject);
                    target.GetComponent<Planet>().player = true;
                    target.GetComponent<Planet>().neutral = false;
                }
            }

            if (target.GetComponent<Planet>().player && enemy)
            {
                target.GetComponent<Planet>().population--;
                if (target.GetComponent<Planet>().population <= 0)
                {
                    PlanetSelection.Instance.addEnemyPlanet(other.gameObject);
                    target.GetComponent<Planet>().enemy = true;
                    target.GetComponent<Planet>().player = false;
                }
            }
            if (target.GetComponent<Planet>().enemy && enemy)
            {
                target.GetComponent<Planet>().population++;
            }
            if (target.GetComponent<Planet>().neutral && enemy)
            {
                target.GetComponent<Planet>().population--;
                if (target.GetComponent<Planet>().population <= 0)
                {
                    PlanetSelection.Instance.addEnemyPlanet(other.gameObject);
                    target.GetComponent<Planet>().enemy = true;
                    target.GetComponent<Planet>().neutral = false;
                }
            }
        }
    }
}
