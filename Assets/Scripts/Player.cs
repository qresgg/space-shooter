using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space)){
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
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
}
