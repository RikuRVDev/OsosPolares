using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int npcRemain;
    // Start is called before the first frame update
    void Start()
    {
        npcRemain = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reduceNpcsRemain(){
        
        this.npcRemain --;
        if(this.npcRemain == 0){
            Debug.Log("Digimones");
        }
    }
}
