using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHero : MonoBehaviour
{
    private BoundChecks boundChecks;

    // Update is called once per frame
    void Update()
    {
        if (boundChecks.LocIs(BoundChecks.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        boundChecks = GetComponent<BoundChecks>();
    }
}
