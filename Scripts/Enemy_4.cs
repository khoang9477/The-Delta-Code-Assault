using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyShield))]
public class Enemy_4 : Enemy
{
    private EnemyShield[] allShields;
    private EnemyShield thisShield;
    // Start is called before the first frame update
    // void Start()
    // {
    //     allShields = GetComponentsInChildren<EnemyShield>();
    //     thisShield = GetComponent<EnemyShield>();
    // }

    public override void Move()
    {

    }

    // void OnCollisionEnter(Collision coll)
    // {
    //     GameObject otherGO = coll.gameObject;
    //     ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
    //     if (p != null)
    //     {
    //         Destroy(otherGO);
    //         if (boundChecks.isOnScreen)
    //         {
    //             GameObject hitGO = coll.contacts[0].thisCollider.gameObject;
    //             if (hitGO == otherGO)
    //             {
    //                 hitGO = coll.contacts[0].otherCollider.gameObject;
    //             }

    //             health--; //temporarily reduce enemy HP

    //             bool shieldFound = false;
    //             foreach (EnemyShield es in allShields)
    //             {
    //                 if (es.gameObject == hitGO)
    //                 {
    //                     // es.TakeDamage(dmg);
    //                     shieldFound = true;
    //                 }
    //             }

    //             // if (!shieldFound) thisShield.TakeDamage(dmg);

    //             if (thisShield.isActive) return;
    //         }
    //     }
    // }

    // Update is called once per frame
    void Update()
    {

    }
}
