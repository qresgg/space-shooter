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
    private GameObject[] powerups; // list of powerups
    private bool _isPlayerDeath = false;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    void Update()
    {
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(!_isPlayerDeath)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector3 propToSpawn = new Vector3(randomX, 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, propToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }
    
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(10f);
        while(!_isPlayerDeath)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector3 propToSpawn = new Vector3(randomX, 7, 0);

            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], propToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(15f); 
        }
    }
    public void PlayerDeath()
    {
        _isPlayerDeath = true;
    }
}
