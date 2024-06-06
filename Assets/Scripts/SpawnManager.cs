using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerUpPrefab;
    private bool _isPlayerDeath = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnTripleShotPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnEnemyRoutine()
    {
        while(!_isPlayerDeath)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector3 propToSpawn = new Vector3(randomX, 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, propToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    
    IEnumerator SpawnTripleShotPowerUp()
    {
        while(!_isPlayerDeath)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector3 propToSpawn = new Vector3(randomX, 7, 0);

            Instantiate(_tripleShotPowerUpPrefab, propToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(7.0f); 
        }
    }

    public void PlayerDeath()
    {
        _isPlayerDeath = true;
    }
}
