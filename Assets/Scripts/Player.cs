using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _xMax = 10.25f;
    private float _xMin = -10.25f;
    private float _yMax = -1f;
    private float _yMin = -3.45f;
    private float _speed = 8.5f;
    private float fireRate = 0.375f;
    private float _lastFireTime = -1f;
    private float lives = 3;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;
    private int _score;
    private SpawnManager spawnManager;
    private UIManager uiManager;
    private Coroutine _speedCoroutine;
    private Coroutine _tripleShotCoroutine;
    private Coroutine _shieldCoroutine;

    private GameObject _playerShield;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    
    void Awake(){
        _playerShield = GameObject.Find("Player_Shield");
        this.setShieldVisibility();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.zero;
        spawnManager = GameObject.Find("Spawn_Manager")?.GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        this._score = 0;
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
        float speed = _isSpeedBoostActive ? _speed * 2 : _speed;
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x >= _xMax && horizontalInput > 0)
        {
            transform.position = new Vector3(_xMin, transform.position.y, 0);
        }
        else if (transform.position.x <= _xMin && horizontalInput < 0)
        {
            transform.position = new Vector3(_xMax, transform.position.y, 0);
        }
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yMin, _yMax), 0);

    }

    void checkFire()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time >= _lastFireTime + fireRate)
        {
            GameObject fire = _isTripleShotActive ? _tripleShotPrefab : _laserPrefab;
            Instantiate(fire, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);
            _lastFireTime = Time.time;
        }
    }

    public void decreaseHealth()
    {
        if (_isShieldActive)
        {
            StopCoroutine(deactivateShield());
            this._isShieldActive = false;
            _playerShield.SetActive(false);
            Debug.Log("Shield deactived");
            return;
        }

        this.lives -= 1;
        if (this.lives <= 0)
        {
            spawnManager.stopSpawn();
            Destroy(this.gameObject);
            Debug.Log("Player Died");
            return;

        }
        Debug.Log("Reamining Health : " + this.lives);




    }

    public void activateTripleShot()
    {
        if (_tripleShotCoroutine != null)
        {
            StopCoroutine(_tripleShotCoroutine);
        }
        this._isTripleShotActive = true;
        _tripleShotCoroutine = StartCoroutine(deactivateTripleShot());
    }

    IEnumerator deactivateTripleShot()
    {
        yield return new WaitForSeconds(3f);
        this._isTripleShotActive = false;
    }

    public void activateSpeedBoost()
    {
        if (_speedCoroutine != null)
        {
            StopCoroutine(_speedCoroutine);
        }
        this._isSpeedBoostActive = true;
        _speedCoroutine = StartCoroutine(deactivateSpeedBoost());
    }
    IEnumerator deactivateSpeedBoost()
    {
        yield return new WaitForSeconds(3f);
        this._isSpeedBoostActive = false;
    }

    public void activateShield()
    {
        if (_shieldCoroutine != null)
        {
            StopCoroutine(_shieldCoroutine);
        }
        this._isShieldActive = true;
        this.setShieldVisibility();
        StartCoroutine(deactivateShield());
    }
    IEnumerator deactivateShield()
    {
        yield return new WaitForSeconds(3f);
        this._isShieldActive = false;
        this.setShieldVisibility();
    }
    void setShieldVisibility(){
        _playerShield.SetActive(_isShieldActive);
    }
    public float getHealth()
    {
        return lives;
    }

    public int getScore(){
        return this._score;
    }

    public void setScore(int score){
        this._score = score;
        uiManager.updateScoreText(score);
    }

}
