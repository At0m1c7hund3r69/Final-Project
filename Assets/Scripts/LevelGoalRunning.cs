using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class LevelGoalRunning : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Flee Settings")]
    [SerializeField] private float runDistance = 8f;
    [SerializeField] private float repathInterval = 0.2f;
    [SerializeField] private float navMeshSampleRadius = 4f;

    private NavMeshAgent agent;
    private float nextRepathTime;
    private bool playerInRange;
    private bool isStunned;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isStunned)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
            }
            if (agent.hasPath)
                agent.ResetPath();

            return;
        }

        if (!playerInRange || player == null)
        {
            if (agent.hasPath)
                agent.ResetPath();

            return;
        }

        if (LevelGoalManager.Instance != null && LevelGoalManager.Instance.LevelComplete)
        {
            agent.ResetPath();
            return;
        }

        if (Time.time >= nextRepathTime)
        {
            FleeFromPlayer();
            nextRepathTime = Time.time + repathInterval;
        }
    }

    public void SetPlayerInRange(Transform detectedPlayer)
    {
        if (isStunned)
            return;

        player = detectedPlayer;
        playerInRange = true;
    }

    public void ClearPlayerInRange(Transform detectedPlayer)
    {
        if (player == detectedPlayer)
        {
            playerInRange = false;
        }
    }

    public void Stun()
    {
        isStunned = true;
        playerInRange = false;

        if (agent  != null)
        {
            agent.isStopped = true;
            agent.ResetPath();
        }
    }

    private void FleeFromPlayer()
    {
        Vector3 awayDirection = transform.position - player.position;
        awayDirection.y = 0f;

        if (awayDirection.sqrMagnitude < 0.01f)
            awayDirection = -player.forward;

        Vector3 desiredPoint = transform.position + awayDirection.normalized * runDistance;

        if (NavMesh.SamplePosition(desiredPoint, out NavMeshHit hit, navMeshSampleRadius, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        LevelGoalGrab grabCollector = other.GetComponent<LevelGoalGrab>();

        if (grabCollector != null)
        {
            if (LevelGoalManager.Instance != null)
            {
                LevelGoalManager.Instance.CollectObjective(1);
            }

            Destroy(gameObject);
        }
    }
}