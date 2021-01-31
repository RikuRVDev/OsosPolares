using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;
    private SpriteRenderer sr;
    private GameManager gameManager;
    private CanvasManager canvasManager;

    private float horizontalPos = 0.0f;
    private float verticalPos = 0.0f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    void Update()
    {
        horizontalPos = Input.GetAxis("Horizontal");
        verticalPos = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        Vector3 tempVect = new Vector3(horizontalPos, verticalPos, 0);
        tempVect = tempVect * speed * Time.deltaTime;
        transform.Translate(tempVect);

        if (horizontalPos > 0) {
            sr.flipX = true;
        } else if (horizontalPos < 0) {
            sr.flipX = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("npc"))
        {
            Npcs npc = other.gameObject.GetComponent<Npcs>();
            canvasManager.renderSprite(npc.color);
            //Debug.Log("Aqui");
            gameManager.reduceNpcsRemain();
            Destroy(other.gameObject, .0f);
        }
    }
}    
