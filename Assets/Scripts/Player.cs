using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _xMax = 10.25f;
    private float _xMin = -10.25f;
    private float _yMax = -1f;
    private float _yMin = -3.45f;
    private float _speed = 7.5f;
    private float fireRate = 0.375f;
    private float _lastFireTime = -1f;
    private float _health = 100;
    private bool tripleShotActive = false;

    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject tripleShotPrefab;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        calculateMovement();
        checkFire();
    }

    void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.x >= _xMax && horizontalInput > 0)
        {
            transform.position = new Vector3(_xMin, transform.position.y, 0);
        }
        else if (transform.position.x <= _xMin && horizontalInput < 0)
        {
            transform.position = new Vector3(_xMax, transform.position.y, 0);
        }

        // if((yPosition >= _yMax && verticalInput > 0) || (yPosition <= _yMin && verticalInput < 0) ){
        //     verticalInput = 0;
        // }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yMin, _yMax), 0);

    }

    void checkFire()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= _lastFireTime + fireRate)
        {
            GameObject fireType = laserPrefab;
            if (tripleShotActive)
            {
                fireType = tripleShotPrefab;
            }
            Instantiate(fireType, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);
            _lastFireTime = Time.time;
        }
    }

    public void decreaseHealth(float damage)
    {
        this._health -= damage;
        if (this._health <= 0)
        {
            SpawnManager spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
            spawnManager.stopSpawn();
            Destroy(this.gameObject);
            Debug.Log("PLAYER DIED");
        }
        else
        {
            Debug.Log("REMAINING HEALTH = " + this._health);
        }
    }

    public float getHealth()
    {
        return _health;
    }

    public void activateTripleShot()
    {
        this.tripleShotActive = true;
        StartCoroutine(deactivateTripleShotAfterDelay(5f));
    }

    private IEnumerator deactivateTripleShotAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.tripleShotActive = false;
    }

}
