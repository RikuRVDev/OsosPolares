using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;
    private SpriteRenderer sr;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalPos = Input.GetAxis("Horizontal");
        float verticalPos = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(horizontalPos, verticalPos, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;

        transform.position += tempVect;

        if(horizontalPos > 0) {
            sr.flipX = true;
        }
        if (horizontalPos < 0) {
            sr.flipX = false;
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
  
        if(other.gameObject.CompareTag("npc"))
        {
            gameManager.reduceNpcsRemain();
            Destroy(other.gameObject, .0f);
        }
    }
}    
