using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text textNpcs;
    public Text timeText;
    private GameManager gameManager;
    public Sprite _CatIcon;
    private Canvas canvas;
    private Camera cam;
    private RectTransform icon;
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        canvas = FindObjectOfType<Canvas>();
        cam = Camera.main;
        generateNcpsIcons(); 
        
    }
    private void generateNcpsIcons(){
            
        for (int i = 0; i < gameManager.npcRemain; i++)
        {
            GameObject obj = new GameObject("npc", typeof(RectTransform));
            obj.AddComponent<Image>();
            Image img = obj.GetComponent<Image>();
            img.sprite = _CatIcon;
            obj.transform.SetParent(transform);
            //Nuevo
            Vector3 screenPos = cam.WorldToScreenPoint(obj.transform.position);
            float h = Screen.height;
            float w = Screen.width;
            float x = screenPos.x - (w - 25)+ (Constants.ICON_SEPARATION * i);
            float y = screenPos.y - 25;
            float s = canvas.scaleFactor;
            RectTransform trm = obj.GetComponent<RectTransform>();
            trm.localScale = new Vector3(0.5f,0.5f,0);  
            trm.anchoredPosition = new Vector2(x, y) / s;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        textNpcs.text = "npcs Remain: " + gameManager.npcRemain;
        //ImageViewBuilder(new Vector2(32,32),new Vector2(0,0), _catIconPrefab.transform);

    }
 
    public void DisplayTime(float timeToDisplay)
    {

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /*
    private void generateNcpsIcons(){
        float h = canvas.GetComponent<RectTransform>().rect.height;
        float w = canvas.GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < gameManager.npcRemain; i++)
        {
            GameObject obj = new GameObject("npc", typeof(RectTransform));
            obj.AddComponent<Image>();
            Image img = obj.GetComponent<Image>();
            img.sprite = _CatIcon;

            obj.transform.position += new Vector3(-3.5f+(-w/300)+(i*0.8f),4.5f,0);
            obj.transform.localScale = new Vector3(0.009f,0.009f,0);  
            Vector2 tmp = new Vector2(0.5f,0.5f);
            obj.transform.SetParent(transform);
        }
    }
    */
}
