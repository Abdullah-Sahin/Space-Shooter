using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour    
{

    private float _speed = 4f;
    private const float _yMin = -5.47f;
    private const float _yMax = 6f;
    private const float _xBound = 9.36f;
    private const float _collisionDamage = 25f;
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(-3,-3,0);
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < _yMin){
            transform.position = new Vector3(Random.Range(-_xBound, _xBound), _yMax, 0);
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.CompareTag("Laser")){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("Player")){
            Player player = other.GetComponent<Player>();
            if(player != null){
                player.decreaseHealth(_collisionDamage);
            }
            Destroy(this.gameObject);
        }

    }

    public static float getXBoundary(){
        return _xBound;
    }

    public static float geyMinY(){
        return _yMin;
    }

    public static float geyMaxY(){
        return _yMax;
    }
   
}
