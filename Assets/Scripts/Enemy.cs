using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Player _player;
    private Animator _anim;

    void Start()
    {
        randomPos();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
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
            _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(this.gameObject, 2.8f);
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
        if (other.tag == "Laser"){
            _anim.SetTrigger("OnEnemyDeath");
            _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(this.gameObject, 2.8f);
            _player.addScore();
            Destroy(other.gameObject);
        }
    }
    private void EnemyDeath()
    {
        _anim.SetTrigger("OnEnemyDeath");
        _speed = 0;
        Destroy(this.gameObject, 2.8f);
    }
    void randomPos(){
        float randomX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomX, 7, 0);
    }
}
