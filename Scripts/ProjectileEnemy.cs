using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    private BoundChecks boundChecks;

    // public int damage = 0;

    // Update is called once per frame
    void Update()
    {
        if (boundChecks.LocIs(BoundChecks.eScreenLocs.offUp) || boundChecks.LocIs(BoundChecks.eScreenLocs.offDown)
        || boundChecks.LocIs(BoundChecks.eScreenLocs.offLeft) || boundChecks.LocIs(BoundChecks.eScreenLocs.offRight))
        {
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        boundChecks = GetComponent<BoundChecks>();
    }

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Hero"))
        {
            // coll.gameObject.GetComponent<Hero>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
