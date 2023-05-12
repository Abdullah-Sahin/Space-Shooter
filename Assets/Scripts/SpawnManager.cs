using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    // Start is called before the first frame update
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private GameObject[] powerups;
    private bool _shouldContinue = true;
    private int _powerUpIndex;
    void Start()
    {
        _enemyContainer = GameObject.Find("Enemy Container").transform;
        StartCoroutine(spawnEnemy());
        StartCoroutine(spawnPowerUp());
    }

    IEnumerator spawnEnemy()
    {
        while (_shouldContinue)
        {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-Enemy.getXBoundary(), Enemy.getXBoundary()), Enemy.geyMaxY()), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer;
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator spawnPowerUp()
    {
        while (_shouldContinue)
        {
            yield return new WaitForSeconds(Random.Range(3.8f, 5.3f));
            int _powerUpIndex = Random.Range(0, 3);
            GameObject powerUp = powerups[_powerUpIndex];
            Instantiate(powerUp, new Vector3(Random.Range(-9.25f, 9.25f), 5.75f), Quaternion.identity);
        }
    }

    public void stopSpawn()
    {
        _shouldContinue = false;
    }

    public int getPowerUpIndex(){
        return this._powerUpIndex;
    }
}
