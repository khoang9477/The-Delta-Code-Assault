using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    [Header("Polarization Inscribed")]
    public float lifeTime = 10;
    [Tooltip("How much sine wave will ease in interpolation")]
    public float sinEccentricity = 2f;
    public AnimationCurve rotCurve;

    [Header("Private Fields")]
    [SerializeField] private float birthTime;
    [SerializeField] private Vector3 p0, p1;

    [SerializeField] private Quaternion baseRotate;
    // Start is called before the first frame update
    void Start()
    {
        //Left side of screen
        p0 = Vector3.zero;

        p0.x = -boundChecks.camWidth - boundChecks.radius;
        p0.y = Random.Range(-boundChecks.camHeight, boundChecks.camHeight);

        //Right side of screen
        p1 = Vector3.zero;

        p1.x = -boundChecks.camWidth + boundChecks.radius;
        p1.y = Random.Range(-boundChecks.camHeight, boundChecks.camHeight);

        if (Random.value > 0.5f)
        {
            //If negative each point, move to the screen
            p0.x *= -1;
            p1.x *= -1;
        }

        birthTime = Time.time;

        //ship rotation
        transform.position = p0;
        transform.LookAt(p1, Vector3.back);
        baseRotate = transform.rotation;
    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime; //linear interpolations

        if (u > 1) //out of range
        {
            Destroy(this.gameObject);
            return;
        }

        float shipRot = rotCurve.Evaluate(u) * 360;

        transform.rotation = baseRotate * Quaternion.Euler(-shipRot, 0, 0);

        //moving based on cubic graph
        u = u + sinEccentricity * Mathf.Sin(u * Mathf.PI * 2);

        //interpolate two x points
        pos = (1 - u) * p0 + u * p1;
    }
}
