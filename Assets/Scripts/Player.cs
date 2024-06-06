using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.5f;
    private float _nextFire = 0.0f; // or -1f

    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    private bool _isTripleShotActive = false;
    private bool _isSpeedActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _shield;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire){
            ShootLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        // transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        // transform.position = (transform.position.y >= 0) ? new Vector3(transform.position.x, 0, 0) : transform.position;
        // transform.position = (transform.position.y <= -3.8f) ? new Vector3(transform.position.x, -3.8f, 0) : transform.position;

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        transform.position = (transform.position.x > 11.3f) ? new Vector3(-11.3f, transform.position.y, 0) : transform.position;
        transform.position = (transform.position.x < -11.3f) ? new Vector3(11.3f, transform.position.y, 0) : transform.position;
    }
    
    void ShootLaser()
    {
        _nextFire = Time.time + _fireRate;
        Vector3 direction = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
        // transform.position + new Vector3(0, 0.8f, 0)
        
        if (_isTripleShotActive)
        {
            Vector3 TripleShotActive = new Vector3(transform.position.x, transform.position.y + 0.2f, 0);
            Instantiate(_tripleShotPrefab, TripleShotActive, Quaternion.identity);   
        }
        else
        {
            Vector3 TripleShotActiveDeactivate = new Vector3(transform.position.x, transform.position.y + 0.8f, 0);
            Instantiate(_laserPrefab, TripleShotActiveDeactivate, Quaternion.identity);
        }
    }
    
    public void Damage()
    {
        if (_isShieldActive){
            _isShieldActive = false;
            _shield.SetActive(false);
            return;
        }
        _lives--;
        if (_lives == 0) 
        {
            _spawnManager.PlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedActive = false;
        _speed /= _speedMultiplier;
    }
    
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
    }
}
