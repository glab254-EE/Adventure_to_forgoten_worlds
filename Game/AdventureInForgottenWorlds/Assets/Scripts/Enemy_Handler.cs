using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy_Handler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 1;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int maxhp = 5;
    [SerializeField] private int damage = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private float attackrange = 2f;
    [SerializeField] private LayerMask enemymask = 7;
    [SerializeField] private LayerMask allymask = 8;
    int founddir = 0; // 0 - not found; -1 left...
    bool movingright = false;
    public int _hp;
    int oldhp;
    float direction = -1;
    bool dead = false;
    float olddir;
    
    int enemymassk;
    void Start()
    {
        enemymassk = enemymask;
        _hp = maxhp;
        oldhp = _hp;
        if (rb==null){
            rb=gameObject.GetComponent<Rigidbody2D>();
        }
        if (animator == null){
            animator=gameObject.GetComponent<Animator>();
        }
        olddir = direction;
    } 
    void FixedUpdate(){
        if (!dead){ 
            Debug.DrawRay(transform.position,transform.right*attackrange,Color.red);
            Debug.DrawRay(transform.position,-transform.right*attackrange,Color.red);
            leftcheck = Physics2D.Raycast(transform.position,-transform.right*attackrange,attackrange,~allymask);
            rightcheck = Physics2D.Raycast(transform.position,transform.right*attackrange,attackrange,~allymask);
            if (leftcheck.collider != null){
                Debug.DrawRay(transform.position,-transform.right*leftcheck.distance,Color.green);
                if (leftcheck.collider.gameObject.layer == 7){
                    direction = 0;
                    founddir = -1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    animator.SetTrigger("Attack");
                }
            } 
            if (rightcheck.collider != null){
                Debug.DrawRay(transform.position,transform.right*rightcheck.distance,Color.green);
                if (rightcheck.collider.gameObject.layer == 7){
                    direction = 0;
                    founddir = 1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    animator.SetTrigger("Attack");
                }
            } 
            rb.velocity = new Vector2(direction*speed,rb.velocity.y);
        }
    }

    // Update is called once per frame
    RaycastHit2D leftcheck;
    RaycastHit2D rightcheck;
    void Update()
    {
        if (_hp<oldhp) {
            OnHit();
            }
        if (!dead){
            if (direction != 0){
                animator.SetBool("Walking",true);
            }
            if (direction == 1){
                if (!movingright){
                    movingright = true;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                }
            } else if (direction == -1) {
                if (movingright){
                    movingright = false;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
        oldhp = _hp;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("AI_Turn") == true){
            direction = -direction;
            olddir = direction;
        }
    }
    void OnHit(){
        direction = 0;
        if (_hp > 0){
            animator.SetTrigger("Hurt");
        }else {
            rb.bodyType = RigidbodyType2D.Static;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            direction = 0;
            animator.SetTrigger("Dying");
            dead = true;
            Destroy(gameObject,2);
        }
    }
    RaycastHit2D hit;
    public void attackhit(){
            hit = Physics2D.Raycast(transform.position,founddir*transform.right*attackrange,attackrange,~allymask);
            if (hit.collider != null){
                var Player_Control = hit.collider.gameObject.GetComponent<Player_Control>();
                if (Player_Control != null){
                    Player_Control._hp -= damage;
                    hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10*founddir,10);
                }
            }
    }
    public void returnspeed(){
        direction=olddir;
    }
}
