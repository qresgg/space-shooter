using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triple_Shot_PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -4) Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            Destroy(this.gameObject);
            _player.TripleShotActive();
        }
    }
}
