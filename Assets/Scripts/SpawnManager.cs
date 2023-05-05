using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]private GameObject _enemyPrefab; 
    // Start is called before the first frame update
    [SerializeField]private Transform _enemyContainer;
    [SerializeField]private GameObject _tripleShotPrefab;
    private bool _shouldContinue = true;
    void Start()
    {
        _enemyContainer = GameObject.Find("Enemy Container").transform;
        StartCoroutine(spawnEnemy());
        StartCoroutine(spawnPowerUp());
        //InvokeRepeating("spawnEnemy", 0f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEnemy(){
        while(_shouldContinue){
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-Enemy.getXBoundary(), Enemy.getXBoundary()), Enemy.geyMaxY()), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer;
            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator spawnPowerUp(){
        while(_shouldContinue){
            yield return new WaitForSeconds(Random.Range(5f, 8f));
            Instantiate(_tripleShotPrefab, new Vector3(Random.Range(-9.2f, 9.2f),6.25f,0), Quaternion.identity);
        }
    }

    public void stopSpawn(){
        _shouldContinue = false;
    }
    // void spawnEnemy(){
    //     GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-Enemy.getXBoundary(), Enemy.getXBoundary()), Enemy.geyMaxY()), Quaternion.identity);
    //     newEnemy.transform.parent = GameObject.Find("Enemy Container").transform;
    // }
}
