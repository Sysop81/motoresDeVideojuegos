using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class WaypointPatrol : MonoBehaviour
{
    
    [SerializeField] private Transform[] waypoints;
    private NavMeshAgent _navMeshAgent;
    private int _currentWaypointIndex = 0;
    private GameEnding _gameEnding;
    
    /// <summary>
    /// Method Start [Life cycle]
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _gameEnding = GameObject.Find("GameEnding").GetComponent<GameEnding>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        
        // GameEnding take an initial control 
        // _navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
    }

    /// <summary>
    /// Method Update
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // GameEnding have a control. Only patrol when the game state is in game.
        if (_gameEnding.gameState != GameState.InGame) return;
        
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            _navMeshAgent.SetDestination(waypoints[_currentWaypointIndex].position);
        }
    }
}
