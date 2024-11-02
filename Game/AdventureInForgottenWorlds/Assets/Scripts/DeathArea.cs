using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb;
    void Start()
    {
        if (rb==null) rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.GetComponent<Player_Control>() != null){
            collider2D.GetComponent<Player_Control>()._hp = 0;
        }
    }
}
