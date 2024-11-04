using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : Enemy
{
    [Header("Beizer Fields")]
    public float lifeTime = 5;
    public Vector2 midpointYRange = new Vector2(1.5f, 3);
    [Tooltip("If true, path and points are drawn for debug")]
    public bool drawDebug = true;
    [Header("Private Fields")]
    [SerializeField] private Vector3[] points;
    [SerializeField] private float birthTime;
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[3];

        points[0] = pos;

        //set min and max same as spawn
        float xMin = -boundChecks.camWidth + boundChecks.radius;
        float xMax = boundChecks.camWidth - boundChecks.radius;

        //random middle position
        points[1] = Vector3.zero;
        points[1].x = Random.Range(xMin, xMax);
        float midYMulti = Random.Range(midpointYRange[0], midpointYRange[1]);
        points[1].y = -boundChecks.camHeight * midYMulti;

        //random top position screen point
        points[2] = Vector3.zero;
        points[2].y = pos.y;
        points[2].x = Random.Range(xMin, xMax);

        birthTime = Time.time;

        if (drawDebug) DrawDebug();
    }

    public override void Move()
    {
        //same as linear interpolation values
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        u = u - 0.1f * Mathf.Sin(u * Mathf.PI * 2);
        transform.rotation = Quaternion.Euler(u * 180, 0, 0);

        pos = Utils.Bezier(u, points);
    }

    void DrawDebug()
    {
        //Draw three points
        Debug.DrawLine(points[0], points[1], Color.red, lifeTime);
        Debug.DrawLine(points[1], points[2], Color.green, lifeTime);

        //Draw Line
        float numSections = 20;
        Vector3 prevPoint = points[0];
        Color col;
        Vector3 pt;
        for (int i = 1; i < numSections; i++)
        {
            float u = i / numSections;
            pt = Utils.Bezier(u, points);
            col = Color.Lerp(Color.red, Color.green, u);
            Debug.DrawLine(prevPoint, pt, col, lifeTime);
            prevPoint = pt;
        }
    }
}
