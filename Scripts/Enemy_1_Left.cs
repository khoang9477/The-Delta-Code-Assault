using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1_Left : Enemy
{
    [Header("Sine Inscribed")]
    [Tooltip("# of seconds enemy run full sine wave")]
    public float waveFrequency = 2;
    [Tooltip("Sine wave width in meters")]
    public float waveWidth = 4;
    [Tooltip("Amount to roll left and right during sine wave")]
    public float waveRotY = 45;

    private float x0; //x position
    private float birthRate;
    void Start()
    {
        x0 = pos.x;

        birthRate = Time.time;
    }

    //Override enemy movement
    public override void Move()
    {
        Vector3 tempPos = pos;
        //theta based on time
        float age = Time.time - birthRate;
        float theta = -(Mathf.PI * 2 * age / waveFrequency);
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        //Rotate the ship
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        base.Move(); //still the same as move down
    }
}
