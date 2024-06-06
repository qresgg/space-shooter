using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private int powerUpID;
    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -6) Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            switch (powerUpID)
            {
                case 0:
                    Destroy(this.gameObject);
                    _player.TripleShotActive();
                    break;
                case 1:
                    Destroy(this.gameObject);
                    _player.SpeedActive();
                    break;
                case 2:
                    Destroy(this.gameObject);
                    _player.ShieldActive();
                    break;
                default:
                    Debug.Log("Default values");
                    break;
            }
        }
    }
}
