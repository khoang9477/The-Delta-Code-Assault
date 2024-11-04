using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static private Main S;

    [Header("Inscribed")]
    public GameObject[] prefabEnemies; //allow to select multiple enemies/objects
    public float enemySpawnPerSecond = 0.2f; //time for delay
    public float enemyInsetDefault = 1.5f; //inset from the sides

    public float gameRestartDelay = 2; //seconds to delay from starting or restarting
    private BoundChecks boundChecks;

    [Header("Difficulty")]
    public float timerProgress = 0; //timer difficulty
    public int difficultyProgress = 0; //scaling difficulty

    void Awake()
    {
        S = this; //reference to boundcheck
        //gameObject
        boundChecks = GetComponent<BoundChecks>();
        //spawn enemy

        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    void Update()
    {
        timerProgress = timerProgress += Time.deltaTime;
    }

    public void SpawnEnemy()
    {
        // all enemies, objects, and boss has no interact to them except player
        int index = Random.Range(0, prefabEnemies.Length);

        if (difficultyProgress == 0)
        {
            //starter difficulty (only enemy_0, and asteroid #1)
            index = Random.Range(0, 2);
            if (timerProgress >= 15f)
            {
                difficultyProgress++;
                timerProgress = 0;
            }
        }
        if (difficultyProgress == 1)
        {
            index = Random.Range(0, 5);
            if (timerProgress >= 15f)
            {
                difficultyProgress++;
                timerProgress = 0;
            }
        }
        if (difficultyProgress == 2)
        {
            //starting here each difficulty increase faster spawn
            //increase more difficulty (add enemy_0 shooter, asteroid #2)
            index = Random.Range(0, 8);
            enemySpawnPerSecond = 0.4f;
            if (timerProgress >= 15f)
            {
                difficultyProgress++;
                timerProgress = 0;
            }
        }
        if (difficultyProgress == 3)
        {
            //1/4 of the game (add enemy_1, unique enemy #1 -> shoot spread shot, asteroid #3)
            index = Random.Range(0, 10);
            if (timerProgress >= 15f)
            {
                difficultyProgress++;
                timerProgress = 0;
            }
        }
        if (difficultyProgress == 4)
        {
            index = Random.Range(0, 13);
            enemySpawnPerSecond = 0.5f;
            if (timerProgress >= 15f)
            {
                difficultyProgress++;
                timerProgress = 0;
            }
        }
        if (difficultyProgress == 5)
        {
            index = Random.Range(0, 16);
            if (timerProgress >= 15f)
            {
                difficultyProgress++;
                timerProgress = 0;
            }
        }
        if (difficultyProgress == 6)
        {
            enemySpawnPerSecond = 0.6f;
            index = Random.Range(0, 18);
            if (timerProgress >= 15f)
            {
                difficultyProgress++;
                timerProgress = 0;
            }
        }
        if (difficultyProgress == 7)
        {
            //half way through (add enemy_2, unique enemy #2 -> same as regular but unique sine wave, all asteroid)
            index = Random.Range(0, prefabEnemies.Length);
            if (timerProgress >= 15f)
            {
                // difficultyProgress++;
                // timerProgress = 0;
                //supposed to do more unique but not able to work circular and boss complex pattern
            }
        }


        GameObject go = Instantiate<GameObject>(prefabEnemies[index]);

        //These are plan more but not able to complete the circular stuff



        //2/3 of the game (add unique enemy #3 -> shoot in circle in clockwise or counterclockwise in faster bullet, asteroid #5)

        //4/5 of the game (add enemy_3, unique #4 -> shoot in homing but only downward, asteroid #6)

        //boss (first, disabled all and then enabled back after finish interact) (add enemy_4 first then use all 4/5)
        //boss used shoot all variants, only move within on screen

        //boss defeated, win and restart the game

        //Position the enemy
        float enemyInset = enemyInsetDefault;
        if (go.GetComponent<BoundChecks>() != null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundChecks>().radius);
        }

        //set initial position to spawn
        Vector3 pos = Vector3.zero;
        float xMin = -boundChecks.camWidth + enemyInset;
        float xMax = boundChecks.camWidth - enemyInset;
        pos.x = Random.Range(xMin, xMax);
        pos.y = boundChecks.camHeight + enemyInset;
        go.transform.position = pos;

        //invoke to spawn enemy again
        Invoke(nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
    }

    void DelayedRestart()
    {
        //Invoke the Restart() //function for game over
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart()
    {
        //Reload the scene
        SceneManager.LoadScene("SampleScene");
    }

    static public void HERO_DIED()
    {
        //Restart game when hero died (called game over screen)
        S.DelayedRestart();
    }
}
