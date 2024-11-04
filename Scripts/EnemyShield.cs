using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
[RequireComponent(typeof(BlinkColorOnHit))]
public class EnemyShield : MonoBehaviour
{
    [Header("Inscribed")]
    public float health = 20;
    private List<EnemyShield> protectors = new List<EnemyShield>();
    private BlinkColorOnHit blinker;
    // Start is called before the first frame update
    void Start()
    {


        blinker = GetComponent<BlinkColorOnHit>();

        blinker.ignoreOnCollisionEnter = true;

        if (transform.parent == null) return;

        EnemyShield shieldParent = transform.parent.GetComponent<EnemyShield>();
        if (shieldParent != null)
        {
            shieldParent.AddProtector(this);
        }
    }

    public void AddProtector(EnemyShield shieldChild)
    {
        protectors.Add(shieldChild);
    }

    public bool isActive
    {
        get
        {
            return gameObject.activeInHierarchy;
        }
        private set
        {
            gameObject.SetActive(value);
        }
    }

    public float TakeDamage(float dmg)
    {
        //Shield damage by bullet
        foreach (EnemyShield es in protectors)
        {
            if (es.isActive)
            {
                dmg = es.TakeDamage(dmg);
                if (dmg == 0) return 0;
            }
        }

        //blinker.SetColors();

        health -= dmg;
        //Destroy enemy shield
        if (health <= 0)
        {
            isActive = false;
            return -health;
        }

        return 0;
    }
}
