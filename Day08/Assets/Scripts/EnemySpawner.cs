using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float maxHp = 0f;
    [SerializeField] private float damage = 0f;
    [SerializeField] private float attackDelay = 0f;
    [SerializeField] private float attackDistance = 0f;
    [SerializeField] private float interactionDistance = 0f;
    [SerializeField] private float followPlayerTime = 0f;
    [SerializeField] private GameObject playerObject = null;
    [SerializeField] private GameObject enemyObject = null;
    [SerializeField] private float spawnDelay = 0;

    private EnemyController _enemy;
    private bool _isEnemyInSpawnProcess;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        if (_enemy.IsDead && !_isEnemyInSpawnProcess)
        {
            Invoke("Spawn", spawnDelay);
            _isEnemyInSpawnProcess = true;
        }
    }

    void Spawn()
    {
        _isEnemyInSpawnProcess = false;

        var currentEnemy = Instantiate(enemyObject, transform.position, Quaternion.identity);
        _enemy = currentEnemy.GetComponent<EnemyController>();
        _enemy.PlayerObject = playerObject;
        _enemy.AttackDelay = attackDelay;
        _enemy.AttackDistance = attackDistance;
        _enemy.Damage = damage;
        _enemy.MaxHp = maxHp;
        _enemy.InteractionDistance = interactionDistance;
        _enemy.FollowPlayerTime = followPlayerTime;
    }
}
