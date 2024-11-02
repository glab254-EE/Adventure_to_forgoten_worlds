using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinArea : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string scenename;
    [SerializeField] private GameObject winframe;
    [SerializeField] private float winduration = 2.5f;
 
    float tick = 0;
    bool won = false;
    // Update is called once per frame
    void Update()
    {
        if (won){
            winframe.SetActive(true);
            tick+=Time.deltaTime;
            if (tick>=winduration){
                SceneManager.LoadScene(scenename);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.GetComponent<Player_Control>() != null){
            won = true;
        }
    }
}
