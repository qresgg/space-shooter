using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Player _player;

    void Start()
    {
        randomPos();
        _player = GameObject.Find("Player").GetComponent<Player>();
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
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
        if (other.tag == "Laser"){
            Destroy(this.gameObject);
            _player.addScore();
            Destroy(other.gameObject);
        }
    }
    
    void randomPos(){
        float randomX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomX, 7, 0);
    }
}
