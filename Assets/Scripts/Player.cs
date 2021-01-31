using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 3;
    private SpriteRenderer sr;
    private GameManager gameManager;

    private float horizontalPos = 0.0f;
    private float verticalPos = 0.0f;

    private AudioManager _audioManager;
    private bool _footstepPlaying = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        sr = GetComponent<SpriteRenderer>();

        Camera cam = Camera.main;
        _audioManager = cam.GetComponent<AudioManager>();
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

        if (tempVect.magnitude > 0 && !_footstepPlaying)
        {
            _audioManager.PlayFootstep();
            _footstepPlaying = true;
            StartCoroutine("PlaySound");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("npc"))
        {
            _audioManager.PlayCompanion();
            gameManager.reduceNpcsRemain();
            Destroy(other.gameObject, .0f);
        }
    }

    private IEnumerator PlaySound() {
        yield return new WaitForSeconds(0.5f);
        _footstepPlaying = false;
    }
}    
