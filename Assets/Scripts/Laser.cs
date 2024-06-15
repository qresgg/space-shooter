using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 12f;
    [SerializeField]
    private bool _isEnemyLaser = false;
    private Player player;

    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();   
        }
    }
    public void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        
       if (transform.position.y > 8f) 
       {
            DestroyObjectInKillZone();
       }
    }
    public void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
       if (transform.position.y < -4.5f) 
       {
            DestroyObjectInKillZone();
       }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && this.tag == "LaserEnemy"){
            Destroy(this.gameObject);
            Player player = other.transform.GetComponent<Player>();
            if (player != null) player.Damage();
        }
    }
    void DestroyObjectInKillZone()
    {
        if(transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(this.gameObject);
    }
    public void EnemyLaserAssinger()
    {
        _isEnemyLaser = true;
        _speed = 6f;
    }
    public void PlayerLaserAssigner()
    {
        _isEnemyLaser = false;
        _speed = 12f;
    }
}
