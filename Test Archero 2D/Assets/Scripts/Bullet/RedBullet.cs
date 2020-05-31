using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour, IBullet {

    private Vector2 MovDirection = default;
    private float MovSpeed = default;
    private float Damag = default;

    private void FixedUpdate() {

        Movment(MovDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.GetComponent<Playar>() != null) {

            collision.GetComponent<Playar>()?.GetDamag(Damag);
            DestroiBullet();
        }
        if(collision.GetComponent<Borders>() == true) {

            DestroiBullet();
        }
    }

    public void SetDirection(Vector2 direction) {

        MovDirection = (direction - (Vector2)transform.position).normalized;
    }

    public void SetDamag(float damag) {

        Damag = damag;
    }

    public void SetSpeed(float speed) {

        MovSpeed = speed;
    }

    public void DestroiBullet() {

        Destroy(gameObject);
    }

    private void Movment(Vector3 directioin) {

        transform.position = Vector2.MoveTowards(transform.position,
           transform.position + directioin, MovSpeed * Time.deltaTime);
    }

}
