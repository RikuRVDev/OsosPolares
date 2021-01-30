using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;
    private SpriteRenderer sr;
    private GameManager gameManager;

    // Player movement
    private float _horizontalPos = 0.0f;
    private float _verticalPos = 0.0f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalPos = Input.GetAxis("Horizontal");
        _verticalPos = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        Vector3 tempVect = new Vector3(_horizontalPos, _verticalPos, 0);
        tempVect = tempVect * speed * Time.deltaTime;

        transform.Translate(tempVect, Space.World);

        if (_horizontalPos > 0) {
            sr.flipX = true;
        } else if (_horizontalPos < 0) {
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
