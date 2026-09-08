using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoController : MonoBehaviour
{
    public float moveAmount = 0.1f;
    public float moveSpeed = 1.0f;

    private Vector3 initialPosition;

    public Transform posStop;

    void Start()
    {

    }

    void Update()
    {
        initialPosition = posStop.position;
        float y = Mathf.Sin(Time.time * moveSpeed) * moveAmount;
        transform.position = initialPosition + new Vector3(0, y, 0);
    }
}