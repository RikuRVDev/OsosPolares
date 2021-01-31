using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int npcRemain;
    public float timeRemaining = 10;
    private float timeTriggerChange;
    private MapGenerator mg;
    private CanvasManager canvasManager;
    private bool timerIsRunning = false;
    private AudioManager _audioManager;

    private void Awake() {
        mg = GetComponent<MapGenerator>();
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        npcRemain = mg.npcs;
        timerIsRunning = true;
        timeTriggerChange = (timeRemaining - 1) / 7;
        Camera cam = Camera.main;
        _audioManager = cam.GetComponent<AudioManager>();
    }

    private void FixedUpdate() {
        updateTimer();
    }

    public void reduceNpcsRemain(){
        
        this.npcRemain --;
        if(this.npcRemain == 0){
            _audioManager.PlayVictory();
            timerIsRunning = false;
        }
    }

    void updateTimer() {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                canvasManager.DisplayTime(timeRemaining, timeTriggerChange);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                _audioManager.PlayFail();
            }
        }
    }
}
