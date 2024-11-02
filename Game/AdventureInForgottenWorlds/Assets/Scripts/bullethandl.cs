using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class bullethandl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float lifetime = 10;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask mask;
    [SerializeField] private int damage;
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject,lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * Time.deltaTime * speed * 10;
    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.layer != mask && collision.gameObject.layer != LayerMask.GetMask("Bullets")){
            if (collision.gameObject.TryGetComponent<Player_Control>(out Player_Control player)){
                // damage player
            } 
            if (collision.gameObject.TryGetComponent<Enemy_Handler>(out Enemy_Handler enem)){
                if (enem._hp>0){
                    if (enem._hp-damage>=0) enem._hp -= damage;
                    else if (enem._hp-damage<0) enem._hp = 0;
                }
            }
            Destroy(gameObject);
        }
    }
}
