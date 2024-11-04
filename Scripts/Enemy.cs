using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoundChecks))]

public class Enemy : MonoBehaviour
{

    [Header("Inscribed")]
    public float speed = 10f;   //enemy move
    public float health = 15; //damage needed to destroy
    public int score = 0; //points for every enemy destroyed

    public int shootInterval = 0; //only applies enemy shooter
    public bool allowShootOnce = false;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    public float angleShoot = 0; //only applies unique enemy

    public float minFireRate = 0.5f;
    public float maxFireRate = 30f;

    public float homingRange = 30f; //this can be change how far enemy can detect player

    protected BoundChecks boundChecks;

    [Header("Dynamic")]
    public float randomChanceScreenPosition = 0;
    public float screenPosition = 0;

    public float degree = 0;
    [Header("GameObject")]
    public Animator _animator;

    public AudioClip _explosionSound;
    public AudioSource _audioSource;

    private Transform player;

    [Tooltip("Do not modify this. Only played once.")]
    public bool soundPlayOnce = false;
    public Vector3 pos
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Start()
    {
        //access animate gameobject
        _animator = GetComponent<Animator>();
        //access audio gameobject
        _audioSource = GetComponent<AudioSource>();
        //access audio clip
        _explosionSound = GetComponent<AudioClip>();
    }

    private void NullChecking()
    {
        if (_animator == null)
        {
            Debug.LogError("_animator is set to NULL. Animation assigned?");
        }
    }

    void Awake()
    {
        boundChecks = GetComponent<BoundChecks>();
    }

    // Update is called once per frame
    void Update()
    {

        //game progress
        shootInterval++;
        randomChanceScreenPosition = Random.Range(0, 1000f);
        screenPosition = screenPosition += Time.deltaTime;
        degree = Random.Range(-90, 90);
        if (gameObject.name == "JunkObject2(Left)(Clone)" || gameObject.name == "JunkObject2(Right)(Clone)" ||
        gameObject.name == "JunkObject3(Left)(Clone)" || gameObject.name == "JunkObject3(Right)(Clone)" ||
       gameObject.name == "JunkObject4(Clone)" || gameObject.name == "JunkObject5(Clone)" ||
       gameObject.name == "JunkObject6(Clone)")
        {
            UniqueMove();
        }
        Move();


        if (shootInterval >= 325 && gameObject.name == "Enemy_0_Shooter2(Clone)") //spread shooter
        {
            EnemySpreadShotProjectile();
            shootInterval = 0;
        }
        else if ((!allowShootOnce && shootInterval >= 5) && gameObject.name == "Enemy_0_Shooter2(Clone)") //spread shooter beginning
        {
            allowShootOnce = true;
            EnemySpreadShotProjectile();

        }
        else if ((shootInterval % 350 == 0 || shootInterval == 0) && (gameObject.name == "Enemy_0_Shooter(Beginner)(Clone)" || gameObject.name == "Enemy_1_Left_Shooter(Beginner)(Clone)" || gameObject.name == "Enemy_1_Right_Shooter(Beginner)(Clone)")) //beginner level
        {
            EnemyProjectile();
            shootInterval = 1;
        }
        else if (shootInterval >= 90 && (gameObject.name == "Enemy_0_Shooter(Clone)" || gameObject.name == "Enemy_1_Left_Shooter(Clone)" || gameObject.name == "Enemy_1_Right_Shooter(Clone)")) //enemy beginner but advanced
        {

            StartCoroutine(ShootRandomly());
            shootInterval = 0;
        }
        // else if (shootInterval >= 123 || shootInterval == 2 && (gameObject.name == "Enemy_1_Left_Shooter_Homing(Clone)" || gameObject.name == "Enemy_1_Right_Shooter_Homing(Clone)")) //enemy_1 homing
        // {
        //     EnemyHomingProjectileDownward();
        //     if (shootInterval >= 123)
        //     {
        //         shootInterval = 3;
        //     }
        // }

        //check whether the enemy is in off screen
        if (boundChecks.LocIs(BoundChecks.eScreenLocs.offDown))
        {
            Destroy(gameObject);
        }
    }

    //this is enemy direction moving
    public virtual void Move()
    {
        //normal enemy ship and regular asteroid, no shoot
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime; //enemy move
        pos = tempPos;  //swap each time enemy move

        //asteroid #1 - default
        //asteroid #2 - moving 30 degree
        //asteroid #3 - moving 45 degree and then other 45 degree direction when reaches random number (1/4 up to 3/4)
        //asteroid #4 - moving straight and then up around 3/4 bottom all the way to top screen and then straight down
        //asteroid #5 - moving slowly and then very fast
        //asteroid #6 - moving random degree from -90 to 90
    }

    public virtual void UniqueMove() //applies only to asteroid
    {

        if (gameObject.name == "JunkObject2(Left)(Clone)")
        {
            Vector3 direction = Quaternion.Euler(0, -30, -30) * Vector3.forward;
            // transform.Translate(direction * speed * Time.deltaTime);
            Vector3 tempPos = pos;
            tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
            pos = tempPos;  //swap each time enemy move

        }
        if (gameObject.name == "JunkObject2(Right)(Clone)")
        {
            Vector3 direction = Quaternion.Euler(0, 30, 30) * Vector3.forward;
            // transform.Translate(direction * speed * Time.deltaTime);
            Vector3 tempPos = pos;
            tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
            pos = tempPos;  //swap each time enemy move
        }
        if (gameObject.name == "JunkObject3(Left)(Clone)")
        {
            if (randomChanceScreenPosition >= 999f)
            {
                Vector3 direction = Quaternion.Euler(0, 45, 45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
                randomChanceScreenPosition = 1000;
            }
            else if (randomChanceScreenPosition >= 666f)
            {
                Vector3 direction = Quaternion.Euler(0, 45, 45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
                randomChanceScreenPosition = 1000;
            }
            else if (randomChanceScreenPosition >= 333f)
            {
                Vector3 direction = Quaternion.Euler(0, 45, 45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
                randomChanceScreenPosition = 1000;
            }
            else
            {
                Vector3 direction = Quaternion.Euler(0, -45, -45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
            }
        }
        if (gameObject.name == "JunkObject3(Right)(Clone)")
        {
            if (randomChanceScreenPosition >= 999f)
            {
                Vector3 direction = Quaternion.Euler(0, -45, -45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
                randomChanceScreenPosition = 1000;
            }
            else if (randomChanceScreenPosition >= 666f)
            {
                Vector3 direction = Quaternion.Euler(0, -45, -45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
                randomChanceScreenPosition = 1000;
            }
            else if (randomChanceScreenPosition >= 333f)
            {
                Vector3 direction = Quaternion.Euler(0, -45, -45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
                randomChanceScreenPosition = 1000;
            }
            else
            {
                Vector3 direction = Quaternion.Euler(0, 45, 45) * Vector3.forward;
                // transform.Translate(direction * speed * Time.deltaTime);
                Vector3 tempPos = pos;
                tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
                pos = tempPos;  //swap each time enemy move
            }
        }
        if (gameObject.name == "JunkObject4(Clone)")
        {
            if (screenPosition >= 6f)
            {
                speed = -5f;
            }
            if (screenPosition >= 10f)
            {
                speed = 10f;
            }
        }
        if (gameObject.name == "JunkObject5(Clone)")
        {
            speed += speed * Time.deltaTime;
        }
        if (gameObject.name == "JunkObject6(Clone)")
        {
            Vector3 direction = Quaternion.Euler(0, degree, degree) * Vector3.forward;
            // transform.Translate(direction * speed * Time.deltaTime);
            Vector3 tempPos = pos;
            tempPos.y -= direction.y * speed * Time.deltaTime; //enemy move
            pos = tempPos;  //swap each time enemy move
            if (speed >= 30f)
            {
                speed -= speed * Time.deltaTime;

            }
            else if (speed <= 10f)
            {
                speed += speed * Time.deltaTime;
            }
            else
            {
                speed += speed * Time.deltaTime;
            }
        }
    }

    //when hero projectile hit enemy
    void OnCollisionEnter(Collision coll)
    {
        GameObject playerColl = coll.gameObject;
        if (playerColl.GetComponent<ProjectileHero>() != null)
        {
            health--;
            Destroy(playerColl);

            if (health <= 0)
            {
                Destroy(playerColl); //remove player projectile
                speed = 0;
                _animator.SetTrigger("OnEnemyDeath");
                if (!soundPlayOnce)
                {
                    _audioSource.Play();
                    soundPlayOnce = true;
                }
                Destroy(gameObject, 0.8f); //remove enemy

            }
        }
        else
        {
            Debug.Log("ENemy hit by non-projectile: " + playerColl.name);
        }
    }

    public void EnemyProjectile() //enemy_0
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is missing!");
            return;
        }

        if (gameObject.name == "Enemy_0_Shooter(Clone)" || gameObject.name == "Enemy_0_Shooter(Beginner)(Clone)" ||
        gameObject.name == "Enemy_1_Left_Shooter(Beginner)(Clone)" || gameObject.name == "Enemy_1_Right_Shooter(Beginner)(Clone)"
        || gameObject.name == "Enemy_1_Left_Shooter(Clone)" || gameObject.name == "Enemy_1_Right_Shooter(Clone)")
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody rigidB = projectile.GetComponent<Rigidbody>();
            if (rigidB != null)
            {
                rigidB.velocity = Vector3.down * projectileSpeed;
            }
        }

    }

    public void EnemySpreadShotProjectile() //enemy_0_spread
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab is missing!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody rigidB = projectile.GetComponent<Rigidbody>();
        if (rigidB != null)
        {
            rigidB.velocity = Vector3.down * projectileSpeed;
        }

        // For downward diagonal to the left:
        angleShoot = 250f;
        float angleRadians = angleShoot * Mathf.Deg2Rad;
        float xSpeed = Mathf.Cos(angleRadians) * projectileSpeed;
        float ySpeed = Mathf.Sin(angleRadians) * projectileSpeed;

        Vector3 leftVel = new Vector3(xSpeed, ySpeed, 0);

        GameObject leftProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody leftRb = leftProjectile.GetComponent<Rigidbody>();
        leftRb.velocity = leftVel;

        // For downward diagonal to the right:
        angleShoot = 290f; //right
        float angleRadians2 = angleShoot * Mathf.Deg2Rad;
        float xSpeed2 = Mathf.Cos(angleRadians2) * projectileSpeed;
        float ySpeed2 = Mathf.Sin(angleRadians2) * projectileSpeed;

        Vector3 rightVel = new Vector3(xSpeed2, ySpeed2, 0);

        GameObject rightProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody rightRb = rightProjectile.GetComponent<Rigidbody>();
        rightRb.velocity = rightVel;
    }

    public void EnemySpiralProjectile()
    {

    }
    // public void EnemyHomingProjectileDownward()
    // {
    //     GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    //     Rigidbody rigidB = projectile.GetComponent<Rigidbody>();

    //     rigidB.velocity = Vector2.down * projectileSpeed;

    //     // Find the player
    //     player = GameObject.FindGameObjectWithTag("Hero").transform;

    //     if (player != null && player.gameObject.activeInHierarchy && player.position.y < transform.position.y)
    //     {
    //         Vector2 direction = (player.position - transform.position).normalized;
    //         rigidB.velocity = Vector3.Lerp(rigidB.velocity, direction * speed, Time.deltaTime * 150);
    //     }
    // }

    IEnumerator ShootRandomly()  //random interval for fire rate
    {
        while (true)
        {
            float waitTime = Random.Range(minFireRate, maxFireRate);
            yield return new WaitForSeconds(waitTime);

            EnemyProjectile();
        }
    }
}
