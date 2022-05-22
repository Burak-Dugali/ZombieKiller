using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{    
    [SerializeField] private Transform target;
    [SerializeField] private float stoppingDistance = 3;

    private ZombieStats stats = null;
    private NavMeshAgent agent = null;
    private Animator anim = null;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        anim.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        RotateToTarget();

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= agent.stoppingDistance)
        {
            anim.SetFloat("Speed", 0f);
            //Attack
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            AttackTarget(targetStats);
        }
    }

    private void RotateToTarget()
    {
        transform.LookAt(target);
    }

    private void GetReferences()
    {
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        stats = GetComponent<ZombieStats>();
    }

    private void AttackTarget(CharacterStats statsToDamage)
    {
        StartCoroutine(Damagee());
        stats.DealDamage(statsToDamage);
    }

    public IEnumerator Damagee()
    {
        yield return new WaitForSeconds(1f);
        anim.SetTrigger("attack");
    }
}
