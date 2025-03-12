using System.Collections.Generic;
using UnityEngine;

public class RandomLocationSpawner<TListItem> : MonoBehaviour 
    where TListItem : MonoBehaviour {


    [Header("Spawn Bounds")]
    [SerializeField] private Vector3 _spawnBoundsMin;
    [SerializeField] private Vector3 _spawnBoundsMax;
    [Header("Exclude Bounds")]
    [SerializeField] private Vector3 _excludeBoundsMin;
    [SerializeField] private Vector3 _excludeBoundsMax;
    [Header("Prefab Settings")]
    [SerializeField] private List<Transform> _prefabList = new List<Transform>();
    [SerializeField] private Transform _spawnLocation;
    [Header("Count Settings")]
    [SerializeField] private int _minSpawnCount;
    [SerializeField] private int _maxSpawnCount;
    [Header("Misc")]
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _maxOnSceneCount;
    [SerializeField] private bool _canSpawn = false;

    private List<TListItem> _onSceneList = new List<TListItem>();
    private float _timeToNextSpawn;

    #region Properties

    public Vector3 SpawnBoundsMin {
        get => _spawnBoundsMin;
        set => _spawnBoundsMin = value;
    }
    public Vector3 SpawnBoundsMax {
        get => _spawnBoundsMax;
        set => _spawnBoundsMax = value;
    }
    public Vector3 ExcludeBoundsMin {
        get => _excludeBoundsMin;
        set => _excludeBoundsMin = value;
    }
    public Vector3 ExcludeBoundsMax {
        get => _excludeBoundsMax;
        set => _excludeBoundsMax = value;
    }
    public List<Transform> PrefabList {
        get => _prefabList;
        set => _prefabList = value;
    }
    public Transform SpawnLocation {
        get => _spawnLocation;
        set => _spawnLocation = value;
    }
    public int MinSpawnCount {
        get => _minSpawnCount;
        set => _minSpawnCount = value;
    }
    public int MaxSpawnCount {
        get => _maxSpawnCount;
        set => _maxSpawnCount = value;
    }
    public float SpawnDelay {
        get => _spawnDelay;
        set => _spawnDelay = value;
    }
    public int MaxOnSceneCount {
        get => _maxOnSceneCount;
        set => _maxOnSceneCount = value;
    }
    public bool CanSpawn {
        get => _canSpawn;
        set => _canSpawn = value;
    }

    #endregion

    public void Update() {
        if (CanSpawn && Time.time >= _timeToNextSpawn) {
            if (_onSceneList.Count <= MaxOnSceneCount) {
                Spawn();
            }
            _timeToNextSpawn = Time.time + SpawnDelay;
        }
    }

    private void Spawn() {
        int spawnCount = UnityEngine.Random.Range(MinSpawnCount, MaxSpawnCount + 1);
        if (spawnCount + _onSceneList.Count > MaxOnSceneCount) {
            spawnCount = MaxOnSceneCount - _onSceneList.Count;
        }
        for (int i = 0; i < spawnCount; i++) {
            Vector3 spawnLocationVector = GetRandomSpawnLocation();

            // Only spawn if the location is valid
            if (IsSpawnLocationValid(spawnLocationVector)) {
                SpawnLocation.position = spawnLocationVector;
                int randomPrefabIndex = UnityEngine.Random.Range(0, PrefabList.Count);
                Transform spawnTransform = Instantiate(PrefabList[randomPrefabIndex], SpawnLocation);
                _onSceneList.Add(spawnTransform.gameObject.GetComponent<TListItem>());
                spawnTransform.parent = null; // Detach from the spawner
            }
        }
    }
    public void RemoveSpawnedFromOnSceneList(TListItem item) {
        _onSceneList.Remove(item);
    }

    private Vector3 GetRandomSpawnLocation() {
        float spawnLocationX = UnityEngine.Random.Range(SpawnBoundsMin.x, SpawnBoundsMax.x);
        float spawnLocationZ = UnityEngine.Random.Range(SpawnBoundsMin.z, SpawnBoundsMax.z);
        float spawnLocationY = UnityEngine.Random.Range(SpawnBoundsMin.y, SpawnBoundsMax.y);
        return new Vector3(spawnLocationX, spawnLocationY, spawnLocationZ);
    }

    private bool IsSpawnLocationValid(Vector3 location) {
        return location.x < ExcludeBoundsMin.x || location.x > ExcludeBoundsMax.x ||
               location.z < ExcludeBoundsMin.z || location.z > ExcludeBoundsMax.z;
    }
}
