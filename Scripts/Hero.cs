using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S { get; private set; }

    // control player
    [Header("Inscribed")]
    public float speed = 20;
    public float rollMultiply = -35;
    public float pitchMultiply = 20;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    public Animator _animator;
    public AudioSource _audioSource;
    public AudioClip _explosionSound;

    public bool soundPlayOnce = false;
    //control player shield
    [Header("Dynamic")]
    [Range(0, 4)]
    [SerializeField]
    public float _shieldLevel = 1;
    [Tooltip("This field holds a reference triggering GameObject")]
    // private GameObject lastTrigger = null;
    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - cannot assign");
        }
    }

    void Start()
    {
        //access animate gameobject
        _animator = GetComponent<Animator>();
        //access audio gameobject
        _audioSource = GetComponent<AudioSource>();
    }

    private void NullChecking()
    {
        if (_animator == null)
        {
            Debug.LogError("_animator is set to NULL. Animation assigned?");
        }
    }
    // When collision
    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        ProjectileEnemy projectile = other.GetComponent<ProjectileEnemy>();
        if (projectile != null) //if hit shield or yourself
        {
            shieldLevel--;
            Destroy(go);
        }
        else
        {
            Enemy enemy = go.GetComponent<Enemy>();
            if (enemy != null)
            {
                shieldLevel--;
                Destroy(go);
            }
            else
            {
                Debug.LogWarning("Shield not trigger by non-enemy: " + go.name);
            }
        }
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        private set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value <= 0)
            {
                // speed = 0;
                // _animator.SetTrigger("OnEnemyDeath");
                Destroy(this.gameObject, 0.8f);

                Main.HERO_DIED();
            }
        }
    }

    // public void TakeDamage(int damage)
    // {
    //     shieldLevel = damage;
    // }

    // Update is called once per frame
    void Update()
    {
        //pull information on axis
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        //change transformation based on axes
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;

        transform.position = pos;

        //rotate the ship feel a bit dynamic
        transform.rotation =
    Quaternion.Euler(vAxis * pitchMultiply, hAxis * rollMultiply, 0);

        //allow player to shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void TempFire()
    {
        GameObject projectile = Instantiate<GameObject>(projectilePrefab);
        projectile.transform.position = transform.position;
        Rigidbody rigidB = projectile.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.up * projectileSpeed;
    }
}
