using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : CharacterStats
{
    [SerializeField] private GameObject EnemyPrefab;

    private float spawnTime;

    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;

    [SerializeField] private bool canAttack;

    [SerializeField] private GameObject blood;
    //[SerializeField] private GameObject HealthPrefab;

    public delegate void EnemyKilled();
    public static event EnemyKilled OnEnemyKilled;

    private void Start()
    {
        InitVariables();
    }

    public void DealDamage(CharacterStats statsToDamage)
    {
        statsToDamage.TakeDamage(damage);
    }

    public override void Die()
    {
        base.Die();
        Instantiate(blood, transform.position, Quaternion.identity);
        
        //_currentMoney += 100;
        //_moneyText.text = _currentMoney.ToString();
        //GameObject HealthPrefab = Instantiate(HealthPrefab,transform.localPosition, Quaternion.identity);
        //Debug.LogError("blood");
        if(gameObject != null)
        {
            Destroy(gameObject);
        }
        if(OnEnemyKilled != null)
        {
            OnEnemyKilled();
        }
        //Instantiate(EnemyPrefab, new Vector3(0, 1, 0), Quaternion.identity);
    }

    private IEnumerator SpawnEnemy()
    {
        
        yield return new WaitForSeconds(spawnTime);
    }

    public override void InitVariables()
    {
        //blood = GameObject.Find("BloodSplash");
        //EnemyPrefab = GetComponent<>();
        maxHealth = 30;
        SetHealthTo(maxHealth);
        isDead = false;

        damage = 10;
        attackSpeed = 1.5f;
        canAttack = false;

        spawnTime = 2f;
    }
}
