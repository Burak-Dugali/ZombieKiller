using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIDeneme : CharacterStats
{
    public NavMeshAgent agent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 1;
    public float speedRun = 5;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;
    public Animator anim;

    //public Transform[] waypoints;
    int m_CurrentWaypointIndex;
    //int zombieDamage = 10;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_playerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;

    private ZombieStats stats = null;
    private PlayerHUD hud;

    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_playerInRange = false;
        m_PlayerNear = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        agent = GetComponent<NavMeshAgent>();

        agent.isStopped = false;
        agent.speed = speedWalk;
        //agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        //agent.stoppingDistance = 2f;

        anim = GetComponent<Animator>();
        hud = GetComponent<PlayerHUD>();
        
        //anim.SetBool("Run",false);
        //anim.SetBool("Walk",false);
        //anim.SetBool("Attack",false);
        //anim.SetBool("Idle",true);
    }

    private void Update()
    {
        EnviromentView();
        
        

        if (!m_IsPatrol)
        {
            Chasing();
        }        
        //else if (agent.speed >= 0 && agent.speed < speedRun)
        //{
        //    anim.SetBool("Walk", true);
        //}
        //else if (agent.speed > speedWalk)
        //{
        //    anim.SetBool("Run", true);
        //}
        else
        {
            if (agent.remainingDistance < 1f)
            {
                Vector3 pos = agent.transform.position +  new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));

                agent.destination = pos;
            }
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            //anim.SetBool("Run", true); //Zombi kovalar            
            Move(speedRun);
            agent.SetDestination(m_PlayerPosition);
            anim.SetTrigger("Run");
            AttackTarget(stats);
        }
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                //anim.SetBool("Attack",false);
                //anim.SetBool("Run", false);
                //anim.SetBool("Walk",true); //Chase'den Walk'a gecis
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                //agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    //anim.SetBool("Walk", false);
                    //anim.SetBool("Run", false);
                    Stop();
                }
                    
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    private void Patroling()
    {  
        if (m_PlayerNear)
        {            
            if (m_TimeToRotate <= 0)
            {                
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
                anim.SetTrigger("Walk");
            }
            else
            {                
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            //agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    //anim.SetBool("Walk", false);
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
            //else
            //{
            //    m_CurrentWaypointIndex = 0;
            //}
        }
    }
    // *****************************************************************************************

    public IEnumerator Damagee()
    {
        anim.SetBool("Attack",true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("Attack", false);
    }
    private void AttackTarget(CharacterStats statsToDamage)
    {
        StartCoroutine(Damagee());
        stats.DealDamage(statsToDamage);
        //StopAllCoroutines();
    }
    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }

    // *****************************************************************************************
    public void NextPoint()
    {
        //m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        //m_CurrentWaypointIndex++;
        //agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void Stop()
    {
        anim.SetTrigger("Idle");
        agent.isStopped = true;
        agent.speed = 0;
    }

    void Move(float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        agent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                //agent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_playerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {                   
                    m_playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_playerInRange = false;
            }
            if (m_playerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }
}
