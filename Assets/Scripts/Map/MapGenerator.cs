using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Map size config
    public int _columns = 320;
    public int _rows = 320;
    private int _minColumns = 30;
    private int _minRows = 30;

    // Camp config
    private int _campSize = 5;
    private int _playerSpawnX = 1;
    private int _playerSpawnY = 1;

    // Map tile types
    private List<Tile> _mapTiles = new List<Tile>();

    // Map tiles
    public GameObject _externalSprite;
    public GameObject _floorSprite;
    public GameObject _playerSprite;
    public GameObject _npcSprite;
    private GameObject _player;
    public int npcs = 4;

    void Start() {
        GenerateAll();
    }

    public void GenerateAll() {
        GenerateMap();
        GenerateNpcs();
        SpawnPlayer();
    }

    /**
     * Map build, depending on column and row count
     * Spawns tiles to build the base map
     **/
    private void GenerateMap() {

        // Check map min size, to prevent a too small map
        if (_columns < _minColumns) { _columns = _minColumns; }
        if (_rows < _minRows) { _rows = _minRows; }

        // Tile map generation
        for (int i = 0; i < _columns; i++) {
            for(int j = 0; j < _rows; j++) {
                GameObject selectedTile;
                if(i == 0 || i == _columns - 1 || j == 0 || j == _rows - 1) {
                    selectedTile = _externalSprite;
                } else {
                    selectedTile = _floorSprite;
                    string tileType = "";
                    if(i <= _campSize && j <= _campSize) {
                        tileType = Constants.TILE_TYPE_CAMP;
                    } else {
                        tileType = Constants.TILE_TYPE_GROUND;
                    }
                    _mapTiles.Add(new Tile(i, j, tileType));
                }
                Instantiate(selectedTile,new Vector3(i, j, 0.0f),Quaternion.identity);
            }
        }
    }

    private void GenerateNpcs() {
        GameObject npc;
        for(int i = 0;i <= npcs;i++)
        {
           npc = Instantiate(_npcSprite,new Vector3(Random.Range(1,_rows-1),Random.Range(1,_columns-1),0.0f),Quaternion.identity);
        }
    }

    private void SpawnPlayer() {
        if (_playerSprite != null) {
            _player = Instantiate(_playerSprite, new Vector3(_playerSpawnX,_playerSpawnY,0.0f), Quaternion.identity);
            Camera cam = Camera.main;
            cam.transform.SetParent(_player.transform);
            Camera2DFollow scriptFollow = cam.GetComponent<Camera2DFollow>();
            scriptFollow.target = _player.transform;
        }
    }
}
