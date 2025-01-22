using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class WaypointPatrol : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform[] waypoints;
    private int _currentWaypointIndex = 0;
    
    private GameEnding _gameEnding;

    private void Awake()
    {
        _gameEnding = GameObject.Find("GameEnding").GetComponent<GameEnding>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        //_navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameEnding.gameState != GameState.InGame) return;
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            _navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
        }
    }
}
