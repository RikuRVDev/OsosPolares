using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int npcRemain;
    public float timeRemaining = 10;
    private MapGenerator mg;
    private CanvasManager canvasManager;
    private bool timerIsRunning = false;

    private void Awake() {
        mg = GetComponent<MapGenerator>();
        canvasManager = FindObjectOfType<CanvasManager>();
  
    }

    // Start is called before the first frame update
    void Start()
    {
        npcRemain = mg.npcs;
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        updateTimer();
    }
    public void reduceNpcsRemain(){
        
        this.npcRemain --;
        if(this.npcRemain == 0){
            Debug.Log("Digimones");
        }
    }
    void updateTimer() {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                canvasManager.DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
}
