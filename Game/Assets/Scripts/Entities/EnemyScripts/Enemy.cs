using Entities.PlayerScripts;
using Scripts.Entities;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject enemyTurret;
    
    private EnemyModel _model;
    
    private PlayerController _playerController;
    private Player _playerModel;
    
    private ObjectPool<EnemyBullet> bulletPool;
    
    public EnemyModel Model => _model;
    
    
    
    public void Awake()
    {   
        bulletPool = new ObjectPool<EnemyBullet>(
            createFunc: () => Instantiate(enemyBulletPrefab, firePoint.position, firePoint.rotation).GetComponent<EnemyBullet>(), 
            actionOnGet: (obj) => obj.gameObject.SetActive(true), 
            actionOnRelease: (obj) => obj.gameObject.SetActive(false), 
            actionOnDestroy: (obj) => Destroy(obj.gameObject), 
            collectionCheck: false, 
            defaultCapacity: 5, 
            maxSize: 5);
        
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _playerModel = _playerController.PlayerModel;
        
    }
    
    public void Initialize(EnemyModel model)
    {
        _model = model;
        _model.OnDeath.AddListener(Die);
        Debug.Log($"Враг создан! Тип: {_model.EnemyType}, HP: {_model.MaxHp}");
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    
    protected void FixedUpdate()
    {
        if (_model == null) return;
        
        if (_playerController == null)
        {
            return;
        }

        // Поворот к цели
        var dir = _playerController.transform.position - enemyTurret.transform.position;
        var lookRotation = Quaternion.LookRotation(dir);
        var rotation = Quaternion.Lerp(enemyTurret.transform.rotation, lookRotation, 
            _model.RotationSpeed * Time.deltaTime).eulerAngles;
        enemyTurret.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        // Стрельба
        if (_model.FireCountdown <= 0f)
        {
            Shoot();
            _model.FireCountdown = 1f / _model.FireRate;
        }
        _model.FireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        var bulletGo = bulletPool.Get();
        bulletGo.transform.position = firePoint.position;
        bulletGo.Seek(_playerController.transform, _playerModel, _model.Damage, _model.BulletSpeed, bulletPool);
    }
}