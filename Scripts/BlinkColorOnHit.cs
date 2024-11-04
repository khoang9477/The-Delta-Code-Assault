using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class BlinkColorOnHit : MonoBehaviour
{
    private static float blinkDuration = 0.1f; //show damage blink
    private static Color blinkColor = Color.red;

    [Header("Dynamic")]
    public bool showingColor = false;
    public float blinkCompleteTime;

    public bool ignoreOnCollisionEnter = false;
    private Material[] materials;
    private Color[] originalColors;
    private BoundChecks boundChecks;

    void Awake()
    {
        boundChecks = GetComponentInParent<BoundChecks>();
        //get materials
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showingColor && Time.time > blinkCompleteTime)
        {
            RevertColors();
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (ignoreOnCollisionEnter) return;
        //Check for collisions with all projectile
        ProjectileHero p = coll.gameObject.GetComponent<ProjectileHero>();
        if (p != null)
        {
            if (boundChecks != null && !boundChecks.isOnScreen)
            {
                return; //no damage and flies off
            }
            SetColors();
        }
    }

    void SetColors()
    {
        foreach (Material m in materials)
        {
            m.color = blinkColor;
        }
        showingColor = true;
        blinkCompleteTime = Time.time + blinkDuration;
    }

    void RevertColors()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingColor = false;
    }
}
