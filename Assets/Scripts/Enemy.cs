using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    void Start()
    {
        transform.position = new Vector3(0, 6, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6f){
            randomPos();
        }
    }
    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();
            if (player != null ? true : false) player.Damage();
        }
        if (other.tag == "Laser"){
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
    
    void randomPos(){
        float randomX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomX, 6, 0);
    }
}
