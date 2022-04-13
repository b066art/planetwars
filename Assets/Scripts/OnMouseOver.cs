using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOver : MonoBehaviour
{
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && PlanetSelection.Instance.planetsSelected.Count != 0 && !PlanetSelection.Instance.playerPlanetList.Contains(hit.collider.gameObject))
        {
            PlanetSelection.Instance.MouseOver(hit.collider.gameObject);
        }
        else
        {
            PlanetSelection.Instance.DeselectTarget();
        }
    }
}
