using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable<T>
{
    [SerializeField] private T _prefab;

    [SerializeField] private SpawnArea<T> _spawnArea;

    [SerializeField, Min(1)] private int _maxAmountOfItemsOnScene;
    [SerializeField, Min(1)] private int _poolCapacity;
    [SerializeField, Min(1)] private int _poolMaxSize;

    [SerializeField, Min(1)] private float _delay = 2f;

    private ObjectPool<T> _pool;

    private SpawnZone _currentSpawnZone;

    private WaitForSeconds _waitSeconds;

    private int _amountOfItemsOnScene;

    private void Awake()
    {
        InitializePool();

        _waitSeconds = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    protected virtual void ActOnGet(T item)
    {
        DetermineSpawnPosition(item);
        item.gameObject.SetActive(true);
    }

    protected SpawnZone GiveSpawnZone()
    {
        return _currentSpawnZone;
    }

    protected virtual void SetSpawnPosition(T item, SpawnZone spawnZone)
    {
        float randomX = Random.Range(spawnZone.LeftmostX, spawnZone.RightmostX);

        Vector2 spawnPosition = new Vector2(randomX, spawnZone.Y + item.gameObject.transform.localScale.y);
        item.transform.position = spawnPosition;
    }

    private void InitializePool()
    {
        _pool = new ObjectPool<T>(
        createFunc: () => Create(),
        actionOnGet: (item) => ActOnGet(item),
        actionOnRelease: (item) => item.gameObject.SetActive(false),
        actionOnDestroy: (item) => ActOnDestroy(item),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private void ActOnDestroy(T item)
    {
        item.ReadyToSpawn -= ReturnToPool;
        Destroy(item.gameObject);
    }

    private T Create()
    {
        T item = Instantiate(_prefab);
        item.ReadyToSpawn += ReturnToPool;

        return item;
    }

    private IEnumerator Spawn()
    {
        while (isActiveAndEnabled)
        {
            yield return _waitSeconds;

            yield return new WaitUntil(() => _amountOfItemsOnScene < _maxAmountOfItemsOnScene);

            _pool.Get();

            _amountOfItemsOnScene++;
        }
    }

    private void DetermineSpawnPosition(T item)
    {
        SpawnZone spawnZone = _spawnArea.GetSpawnZone();
        SetSpawnPosition(item, spawnZone);

        _currentSpawnZone = spawnZone;
    }

    private void ReturnToPool(T item)
    {
        if (item.gameObject.activeSelf)
        {
            _pool.Release(item);

            _amountOfItemsOnScene--;
        }
    }
}