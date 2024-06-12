using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    private Player _player;
    private Animator _anim;

    [SerializeField] AudioClip _explosionSoundClip;
    [SerializeField] AudioSource _audioSource;

    [SerializeField] private bool _isEnemyExplosion = false;

    void Start()
    {
        randomPos();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _explosionSoundClip;
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f){
            randomPos();
        }
    }
    private void OnTriggerEnter2D(Collider2D other){ // or OnTriggerEnter for 3D, Collider for 3D
        if (other.tag == "Player"){
            EnemyDeath();
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
        if (other.tag == "Laser" && _isEnemyExplosion == false){
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
        Destroy(GetComponent<Rigidbody2D>());
        _audioSource.Play();
    }
    void randomPos(){
        float randomX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomX, 7, 0);
    }
}
