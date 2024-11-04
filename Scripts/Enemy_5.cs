using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5 : Enemy
{
    public float waveRotY = 45;
    public float circleTimer = 0f;
    public float circleSpeed = 0f;
    public float circleRadius = 0f;

    private float x0; //x position
    private float birthRate;
    void Start()
    {
        x0 = pos.x;

        birthRate = Time.time;
    }
    public override void Move()
    {
        // Circular movement
        circleTimer += Time.deltaTime;
        float angle = circleTimer * circleSpeed;
        float x = Mathf.Cos(angle) * circleRadius;
        float y = Mathf.Sin(angle) * circleRadius;

        // Combine circular and downward movement
        transform.position += new Vector3(x, y - speed * Time.deltaTime, 0);

        // Rotate the ship
        Vector3 rot = new Vector3(0, waveRotY, 0);
        transform.rotation = Quaternion.Euler(rot);
    }
}
