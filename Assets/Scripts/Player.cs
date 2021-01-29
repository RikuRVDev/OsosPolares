using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 100;
    public Transform obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalPos = Input.GetAxis("Horizontal");
        float verticalPos = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(horizontalPos, verticalPos, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;

        obj.transform.position += tempVect;
    }
}    
