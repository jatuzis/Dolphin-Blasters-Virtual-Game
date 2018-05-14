using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private ObstacleSpawner _spawner;
    private bool _is_grounded;

    private void Start()
    {
        _is_grounded = false;
        _spawner = FindObjectOfType<ObstacleSpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            _spawner.RemoveObstacle(this);
            Destroy(gameObject);
            return;
        }
        if(collision.gameObject.tag == "Ground")
        {
            _is_grounded = true;
            gameObject.isStatic = true;
        }
        if(collision.gameObject.tag == "Character" && _is_grounded == false)
        {
            collision.gameObject.GetComponent<CharacterBehaviour>().ReceiveDamage(gameObject, transform);
            Destroy(gameObject);
            return;
        }
        if(collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
            return;
        }
    }
}
