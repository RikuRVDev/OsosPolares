using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int _columns = 320;
    public int _rows = 320;

    public GameObject _externalSprite;
    public GameObject _floorSprite;
    public GameObject _playerSprite;
    private GameObject player;

    void Start() {
        GenerateMap();
        SpawnPlayer();
    }

    private void GenerateMap() {
        for(int i = 0;i < _columns;i++) {
            for(int j = 0;j < _rows;j++) {
                GameObject selectedTile;
                if(i == 0 || i == _columns - 1 || j == 0 || j == _rows - 1) {
                    selectedTile = _externalSprite;
                } else {
                    selectedTile = _floorSprite;
                }
                Instantiate(selectedTile,new Vector3(i,j,0.0f),Quaternion.identity);
            }
        }
    }

    private void SpawnPlayer() {
        if (_playerSprite != null) 
        {
            player = Instantiate(_playerSprite, new Vector3(1, 1, 0.0f), Quaternion.identity);
            Camera cam = Camera.main;
            cam.transform.SetParent(player.transform);
            Camera2DFollow scriptFollow = cam.GetComponent<Camera2DFollow>();
            scriptFollow.target = player.transform;
        }
    }
}
