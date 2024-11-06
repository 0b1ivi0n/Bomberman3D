using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int _width = 13;
    [SerializeField] private int _height = 11;
    [SerializeField] private GameObject _wallPrefab;
    [SerializeField] private GameObject _destructiblePrefab;

    [SerializeField] private float _spawnChance = 0.5f;

    private List<GameObject> _walls = new();
    private List<GameObject> _destrutibles = new();

    [SerializeField] private bool _generateNewSeed = true;
    [SerializeField] private int _seed;

    private void Awake()
    {
        if (_generateNewSeed)
        {
            _seed = Random.Range(0, 256);
        }
        Random.InitState(_seed);
        GenerateMap();
    }

    void GenerateMap()
    {

        for (int x = 0, y = 0; x < _width; x++)
        {
            var position = new Vector3(x, 0.5f, y);
            var wall = Instantiate(_wallPrefab, position, Quaternion.identity, transform);
            _walls.Add(wall);
        }

        for (int x = 0, y = _height - 1; x < _width; x++)
        {
            var position = new Vector3(x, 0.5f, y);
            var wall = Instantiate(_wallPrefab, position, Quaternion.identity, transform);
            _walls.Add(wall);
        }

        for (int x = 0, y = 1; y < _height - 1; y++)
        {
            var position = new Vector3(x, 0.5f, y);
            var wall = Instantiate(_wallPrefab, position, Quaternion.identity, transform);
            _walls.Add(wall);
        }

        for (int x = _width - 1, y = 1; y < _height - 1; y++)
        {
            var position = new Vector3(x, 0.5f, y);
            var wall = Instantiate(_wallPrefab, position, Quaternion.identity, transform);
            _walls.Add(wall);
        }

        for (int x = 1; x < _width - 1; x++)
        {
            for (int y = 1; y < _height - 1; y++)
            {
                if ((x == 1 && (y == 1 || y == 2)) || (x == 2 && y == 1))
                {
                    continue;
                }

                var position = new Vector3(x, 0.5f, y);
                if (x % 2 == 0 && y % 2 == 0)
                {
                    var wall = Instantiate(_wallPrefab, position, Quaternion.identity, transform);
                    _walls.Add(wall);
                }
                else
                {
                    if (Random.value > _spawnChance)
                    {
                        var destrcutible = Instantiate(_destructiblePrefab, position, Quaternion.identity, transform);
                        _destrutibles.Add(destrcutible);
                    }
                }
            }

        }

    }
}