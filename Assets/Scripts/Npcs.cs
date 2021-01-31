using UnityEngine;
public class Npcs : MonoBehaviour     
{
    // NPC properties
    public int id;
    public Sprite color;
    private BoxCollider2D _boxCollider;

    // Game properties
    private GameManager _gameManager;
    private Tile _campPosition;
    private bool _returningToCamp = false;
    private bool _reachedEnd = false;
    private float _time = 0.0f;

    private void Awake() {
        _boxCollider = GetComponent<BoxCollider2D>();

        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        if (_returningToCamp && !_reachedEnd)
        {
            _time += Time.deltaTime / 200.0f;
            transform.position = Vector3.Lerp(transform.position, new Vector3(_campPosition.x, _campPosition.y, 0.0f), _time);
        }

        if (_returningToCamp && transform.position.x <= (_campPosition.x - 0.01) && transform.position.y <= (_campPosition.y - 0.01))
        {
            _reachedEnd = true;
        }
    }

    public void GoToCamp() {
        _boxCollider.enabled = false;
        _campPosition = _gameManager.GetNextCampPosition();
        _returningToCamp = true;
    }
} 