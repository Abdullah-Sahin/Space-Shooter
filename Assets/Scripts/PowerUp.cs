using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private float _speed = 6f;
    // Update is called once per frame
    [SerializeField] private int _powerUpIndex;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.25f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            switch (_powerUpIndex)
            {
                case 0:
                    player.activateTripleShot();
                    break;
                case 1:
                    player.activateSpeedBoost();
                    break;
                case 2:
                    player.activateShield();
                    break;
                default: break;
            }
            Destroy(this.gameObject);
        }
    }
}
