using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Player _player;
    private Animator _anim;

    [Header("Audio Settings")]
    [SerializeField] AudioClip _explosionSoundClip;
    [SerializeField] AudioSource _audioSource;

    [SerializeField] private bool _isEnemyExplosion = false;
    [Header("Laser Settings")]
    [SerializeField] private GameObject _laserPrefab;
    private Laser laser;
    [SerializeField] private float _fireRate = 2.5f;
    private float _nextFire = 0.0f; // or -1f
    void Start()
    {
        randomPos();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _explosionSoundClip;
        laser = _laserPrefab.GetComponent<Laser>();
    }

    void Update()
    {
        CalculateMovement();
        if (Time.time > _nextFire && !_isEnemyExplosion)
        {
            _fireRate = Random.Range(2f, 4f);
            _nextFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] laser = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < laser.Length; i++)
            {
                laser[i].EnemyLaserAssinger();
            }
        }
    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f){
            randomPos();
        }
    }
    public void EnemyShootsLaser()
    {
        laser.EnemyLaserAssinger();
    }
    private void OnTriggerEnter2D(Collider2D other){ // or OnTriggerEnter for 3D, Collider for 3D
        if (other.tag == "Player"){
            EnemyDeath();
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
        if (other.tag == "Laser" && !_isEnemyExplosion){
            EnemyDeath();
            _player.addScore();
            Destroy(other.gameObject);
        }
    }
    private void EnemyDeath()
    {
        _isEnemyExplosion = true;
        _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(this.gameObject, 2.8f);
        Destroy(GetComponent<Collider2D>()); // or GetComponent<Rigidbody2D>()
        _audioSource.Play();
    }
    void randomPos(){
        float randomX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomX, 7, 0);
    }
}
