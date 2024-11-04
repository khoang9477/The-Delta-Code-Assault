using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [Header("Inscribed")]
    public float rotationShieldPerSecond = 0.1f;

    [Header("Dynamic")]
    public int levelShown = 0;

    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        //read hero shield
        int currentLevel = Mathf.FloorToInt(Hero.S.shieldLevel);

        //if different shield
        if (levelShown != currentLevel)
        {
            levelShown = currentLevel;

            //adjust texture
            mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        }

        //rotate the shield
        float rotateZ = -(rotationShieldPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rotateZ);
    }
}
