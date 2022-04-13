using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }
}
