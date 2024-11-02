using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Collider2D collidera;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveforce;
    bool canmove = false;
    bool grabbed = false;

    // Update is called once per frame
    void Update()
    {
        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics2D.OverlapPoint(mousepos) == collidera){
                canmove = true;
                grabbed = true;
            } else canmove = false;
        } else if (Input.GetKeyUp(KeyCode.Mouse0)){
            grabbed = false;
            canmove = false;
        }
        if (grabbed&&canmove){
            var modpos = new Vector2(transform.position.x,transform.position.y);
            rb.velocity = (mousepos - modpos).normalized*moveforce;
        }
    }
}
