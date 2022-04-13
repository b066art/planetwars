using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer circleRenderer;

    private CircleCollider2D planetCollider;
    private Transform planetTransform;
    private int numOfSteps = 100;

    void Awake()
    {
        StartDraw();
    }

    public void StartDraw()
    {
        planetCollider = gameObject.GetComponentInParent<CircleCollider2D>();
        planetTransform = gameObject.GetComponentInParent<Transform>();

        float radius = planetCollider.radius * transform.parent.localScale.x;
        DrawCircle (numOfSteps, radius + 0.15f);
    }

    void DrawCircle (int steps, float radius)
    {
        circleRenderer.positionCount = steps + 1;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius + planetTransform.position.x;
            float y = yScaled * radius + planetTransform.position.y;

            Vector3 currentPosition = new Vector3(x, y, 0);

            circleRenderer.SetPosition(currentStep, currentPosition);
        }

        circleRenderer.SetPosition(numOfSteps, circleRenderer.GetPosition(0));
    }
}
