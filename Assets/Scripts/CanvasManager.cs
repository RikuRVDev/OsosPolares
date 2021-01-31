using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text timeText;
    public Sprite _CatIcon;
    public List<Sprite> _timmerStages;

    private GameManager gameManager;
    private Canvas canvas;
    private Camera cam;
    private List<Image> _CatsWithoutRecolect = new List<Image>();
    private int IconCounter = 0;
    private Image timmerIcon;
    private int timerChangeIndex = 1;
    private float countdownToTimerChanges = 0;
    private float timeRamaining;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        canvas = FindObjectOfType<Canvas>();
        cam = Camera.main;
        
        generateNcpsIcons(); 
        generateTimmerStages();
        
    }
    private void generateNcpsIcons(){
        for (int i = 0; i < gameManager.npcRemain; i++)
        {
            GameObject obj = new GameObject("timer", typeof(RectTransform));
            obj.AddComponent<Image>();
            Image img = obj.GetComponent<Image>();
            _CatsWithoutRecolect.Add(img);
            img.sprite = _CatIcon;
            obj.transform.SetParent(transform);
            //Nuevo
            Vector3 screenPos = cam.WorldToScreenPoint(obj.transform.position);
            float h = Screen.height;
            float w = Screen.width;
            float x = screenPos.x - w + 25 + (Constants.ICON_SEPARATION * i);
            float y = screenPos.y - 25;

            float s = canvas.scaleFactor;
            RectTransform trm = obj.GetComponent<RectTransform>();
            trm.localScale = new Vector3(0.5f,0.5f,0);  
            trm.anchoredPosition = new Vector2(x, y) / s;
        }

    }
    private void generateTimmerStages(){
        GameObject obj = new GameObject("npc", typeof(RectTransform));
        obj.AddComponent<Image>();
        Image img = obj.GetComponent<Image>();
        timmerIcon = img;
        img.sprite = _timmerStages[0];
        obj.transform.SetParent(transform);
        //Nuevo
        Vector3 screenPos = cam.WorldToScreenPoint(obj.transform.position);
        float h = Screen.height;
        float w = Screen.width;
        float x = screenPos.x - (w /2);
        float y = screenPos.y - 25;
        float s = canvas.scaleFactor;
        RectTransform trm = obj.GetComponent<RectTransform>();
        trm.localScale = new Vector3(0.5f,0.5f,0);  
        trm.anchoredPosition = new Vector2(x, y) / s;
    }
    public void DisplayTime(float timeToDisplay, float timeTriggerChange)
    {
        
        countdownToTimerChanges += Time.deltaTime;
        if(countdownToTimerChanges >= timeTriggerChange) {
            timmerIcon.sprite = _timmerStages[timerChangeIndex];
            timerChangeIndex++;
            countdownToTimerChanges = 0;
        }
        /*
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        */
    }

    public void renderSprite(Sprite sprite){
        _CatsWithoutRecolect[IconCounter].sprite = sprite;
        IconCounter++;
    }
}
