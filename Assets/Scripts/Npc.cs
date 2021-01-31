using UnityEngine;


public class Npc : MonoBehaviour {
    


    private int id;
    public string color;
  
    public void SetId(int id) {
        this.id = id;
        if(id % 4 == 0){
            this.color = "Green";
        }else if(id % 4 == 0){
            this.color = "Blue";
        }else if(id % 4 == 0){
            this.color = "Pink";
        }else{
            this.color = "Red";
        }
        
    }
    
} 