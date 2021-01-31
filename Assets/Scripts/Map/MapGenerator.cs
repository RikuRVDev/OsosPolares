using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

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
    private List<Tile> _campTiles = new List<Tile>();
    private List<Tile> _npcTiles = new List<Tile>();
    private List<Tile> _obstacleTiles = new List<Tile>();

    // Map tiles
    public GameObject _pathSprite;
    public GameObject _externalSprite;
    public GameObject[] _floorSprites;
    public GameObject _playerSprite;
    public GameObject[] _npcSprites;
    private GameObject _player;
    public int npcs = 4;

    // Big obstacle tiles
    private List<GameObject[]> _bigObstaclesList = new List<GameObject[]>();
    public GameObject[] _nakedTree;
    public GameObject[] _bigTree;
    public int _bigObstacles = 2;
    private int _maxRetries = 5;

    // Obstacle tiles
    public GameObject[] _obstacleSprites;
    public int obstacles = 30;

    void Start() {
        BuildBigObstacles();
        GenerateAll();
    }

    public void GenerateAll() {
        GenerateMap();
        GenerateNpcs();
        SetNpcPaths();
        GenerateBigObstacles();
        GenerateSmallObstacles();
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
                    selectedTile = _floorSprites[Random.Range(0, _floorSprites.Length)];
                    if(i <= _campSize && j <= _campSize) {
                        _campTiles.Add(new Tile(i, j, Constants.TILE_TYPE_CAMP));
                    } else {
                        _mapTiles.Add(new Tile(i, j, Constants.TILE_TYPE_GROUND));
                    }
                }
                Instantiate(selectedTile,new Vector3(i, j, 0.0f),Quaternion.identity);
            }
        }
    }

    /**
     * Generate npcs depending on npc count
     * Get available positions from Map Tiles
     **/
    private void GenerateNpcs() {
        for(int i = 0;i < npcs;i++)
        {
            Tile npcPosition = GetRandomTile();
            _npcTiles.Add(new Tile(npcPosition.x,npcPosition.y,Constants.TILE_TYPE_NPC));
            GameObject randomNpc = _npcSprites[Random.Range(0,_npcSprites.Length)];
            Instantiate(randomNpc,new Vector3(npcPosition.x,npcPosition.y,0.0f),Quaternion.identity);
        }
    }

    /**
     * Build paths for the player to get to all the NPCs
     **/
    private void SetNpcPaths() {
        List<Tile> pathTiles = new List<Tile>();

        foreach(Tile t in _npcTiles)
        {
            List<Tile> directions = RandomPathfinding.GenerateRandomPath(_playerSpawnX,_playerSpawnY, t.x, t.y, 0.3);
            string str = "";
            for(int i = 0; i < directions.Count; i++)
            {
                if(i == 0) str += "[";
                str += directions[i].ToString();
                if(i < directions.Count - 1) str += ", ";
                if(i == directions.Count - 1) str += "]";

                Tile tile = directions[i];
                Tile matchTiles = _mapTiles.Find(tt => tt.x == tile.x && tt.y == tile.y);
                if (matchTiles != null && !ElementIntoArray(pathTiles, matchTiles))
                {
                    pathTiles.Add(matchTiles);
                    _mapTiles.RemoveAt(_mapTiles.IndexOf(matchTiles));
                }
            }
        }
    }

    /**
     * Build all big obstacles to generate a list of sprite arrays
     **/
    private void BuildBigObstacles() {
        _bigObstaclesList.Add(_nakedTree);
        _bigObstaclesList.Add(_bigTree);

        /*
        Debug.Log(_bigObstacles.Count);
        foreach (GameObject[] bo in _bigObstacles)
        {
            foreach (GameObject go in bo)
            {
                Debug.Log(go.name);
            }
        }
        */
    }

    /**
     * Generate big obstacles to block the player
     * Get available positions from Map Tiles and set them onto the map
     **/
    private void GenerateBigObstacles() {
        for (int i = 0; i < _bigObstacles; i++)
        {
            GameObject[] bigObstacle = _bigObstaclesList[Random.Range(0,_bigObstaclesList.Count)];
            List<Tile> obstacleTiles = GetRandomTileGroup(bigObstacle.Length);

            if (obstacleTiles.Count > 0)
            {
                for (int j = 0; j < obstacleTiles.Count; j++)
                {
                    Tile t = obstacleTiles.ElementAt(j);
                    Instantiate(bigObstacle[j],new Vector3(t.x,t.y,0.0f),Quaternion.identity);
                }
            }

        }
    }

    /**
     * Genetate obstacles to block the player
     * Get available positions from Map Tiles and set them onto the map
     **/
    private void GenerateSmallObstacles() {
        for (int i = 0; i < obstacles; i++)
        {
            GameObject selectedTile = _obstacleSprites[Random.Range(0,_obstacleSprites.Length)];
            Tile obstaclePosition = GetRandomTile();
            _obstacleTiles.Add(new Tile(obstaclePosition.x,obstaclePosition.y,Constants.TILE_TYPE_OBSTACLE));
            Instantiate(selectedTile,new Vector3(obstaclePosition.x,obstaclePosition.y,0.0f),Quaternion.identity);
        }
    }

    /**
     * Spawn player at start position
     **/
    private void SpawnPlayer() {
        if (_playerSprite != null) {
            _player = Instantiate(_playerSprite, new Vector3(_playerSpawnX,_playerSpawnY,0.0f), Quaternion.identity);
            Camera cam = Camera.main;
            cam.transform.SetParent(_player.transform);
            Camera2DFollow scriptFollow = cam.GetComponent<Camera2DFollow>();
            scriptFollow.target = _player.transform;
            scriptFollow.hasTarget = true;
        }
    }

    private Tile GetRandomTile() {
        int randomIndex = Random.Range(0,_mapTiles.Count);
        Tile npcPosition = _mapTiles[randomIndex];
        _mapTiles.RemoveAt(randomIndex);
        return npcPosition;
    }

    private bool ElementIntoArray(List<Tile> tileArray, Tile tile) {
        return tileArray.Any( arrayTile => arrayTile.x == tile.x && arrayTile.y == tile.y );
    }

    private List<Tile> GetRandomTileGroup(int count) {

        List<Tile> tileGroup = new List<Tile>();
        bool tryAgain = false;
        bool found = false;
        int retries = 0;

        // Find a spot that fits the sprite, max 10 tries
        while ((retries < _maxRetries) && !found)
        {
            tryAgain = false;

            int randomIndex = Random.Range(0,_mapTiles.Count);
            Tile randomInitTile = _mapTiles[randomIndex];
            tileGroup.Add(randomInitTile);

            // Check if sprite fits into the tilemap
            for(int i = 1;i < count && !tryAgain;i++)
            {
                Tile nextTile = _mapTiles.Find(t => randomInitTile.x == t.x && (randomInitTile.y + 1) == t.y);
                if(nextTile != null)
                {
                    tileGroup.Add(nextTile);
                }
                else
                {
                    tryAgain = true;
                }
            }

            if (tileGroup.Count == count)
            {
                found = true;

                foreach (Tile t in tileGroup)
                {
                    _mapTiles.RemoveAt(_mapTiles.IndexOf(t));
                }
            } else
            {
                retries++;
                tileGroup = new List<Tile>();
            }
        }

        return tileGroup;
    }
}
