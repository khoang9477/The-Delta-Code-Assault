using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Inscribed")]
    public Transform player;
    public Transform[] panels;
    [Tooltip("Speed at the panels scrolling")]
    public float scrollSpeed = -30f; //if negative scroll up, positive scroll down.
    //0 is static, useful for wave and boss.
    [Tooltip("Controls the background react to player")]
    public float motionMulti = 0.25f;

    private float panelHeight;
    private float depth; //background Z
    // Start is called before the first frame update
    void Start()
    {
        panelHeight = panels[0].localScale.y;
        depth = panels[0].position.z;

        //initial position of panels
        panels[0].position = new Vector3(0, 0, depth);
        panels[1].position = new Vector3(0, panelHeight, depth);
    }

    // Update is called once per frame
    void Update()
    {
        //moving background based on player movement or wave
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHeight + (panelHeight * 0.5f);
        if (player != null)
        {
            tX = -player.transform.position.x * motionMulti;
            tY += player.transform.position.y * motionMulti;
        }
        panels[0].position = new Vector3(tX, tY, depth);

        //continuous background moving depends on wave or regular
        if (tY >= 0)
        {
            panels[1].position = new Vector3(tX, tY - panelHeight, depth);
        }
        else
        {
            panels[1].position = new Vector3(tX, tY + panelHeight, depth);
        }
    }
}
