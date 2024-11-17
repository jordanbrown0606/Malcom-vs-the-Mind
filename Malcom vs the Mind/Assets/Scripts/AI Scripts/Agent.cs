using BetterFSM;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Agent : MonoBehaviour, IDamageable
{
    [SerializeField, Range(0f, 360f)] protected float _fov = 60f;       //Field of view
    [SerializeField, Range(0f, 1f)] protected float _sightRange = 1f;   //Normalized value for the "sensor" child object's sphere collider

    [SerializeField] protected Sensor _sensor;  //A collider that helps dictate how far the character can see and hear

    [SerializeField] protected Color _lineOfSightColour;    //Sets the gizmo colour allowing the user to see what the line of sight is

    [SerializeField] protected int _health;

    public Transform target;

    protected NavMeshAgent _navAgent;                   //Local reference to the nav mesh agent

    public NavMeshAgent GetNavAgent {  get { return _navAgent; } }

    public StateType curState;

    private StateBase _curState;

    private Dictionary<StateType, StateBase> _states = new Dictionary<StateType, StateBase>();

    protected bool _hasReachedDestination = false;      //Tracks if the agent has or has not reached its destination

    private void Start()
    {
        StateBase[] allStates = GetComponents<StateBase>();
        _navAgent = GetComponent<NavMeshAgent>();

        foreach (StateBase state in allStates)
        {
            if (_states.ContainsKey(state.GetStateType) == false)
            {
                _states.Add(state.GetStateType, state);
                state.InitState(this);
            }
        }

        Transition(curState);
    }

    private void Transition(StateType newState)
    {
        if (_curState != null && _curState.GetStateType == newState)
        {
            return;
        }

        if (_states.ContainsKey(newState) == false)
        {
            return;
        }

        curState = newState;
        _curState?.OnStateExit();
        _curState = _states[newState];
        _curState?.OnStateEnter();
    }

    private void Update()
    {
        if (_curState == null)
        {
            return;
        }

        Transition(_curState.OnStateUpdate());
    }

    /// <summary>
    /// Get the true position of the sensor
    /// When an object is a child of another object and its parent's scale is changed
    /// this will change the "true" scale of the child even if the scale does not change in the inspector
    /// This will always return the most accurate version of the position
    /// </summary>
    public Vector3 GetSensorPosition
    {
        get
        {
            if (_sensor == null)
            {
                return Vector3.zero;
            }

            Vector3 pos = _sensor.transform.position;
            pos.x += _sensor.GetSphereCollider.center.x * _sensor.transform.lossyScale.x;
            pos.y += _sensor.GetSphereCollider.center.y * _sensor.transform.lossyScale.y;
            pos.z += _sensor.GetSphereCollider.center.z * _sensor.transform.lossyScale.z;

            return pos;
        }
    }

    /// <summary>
    /// Get the true radius of the sensor
    /// When an object is a child of another object and its parent's scale is changed
    /// this will change the "true" scale of the child even if the scale does not change in the inspector
    /// This will always return the most accurate version of the radius
    /// </summary>
    public float GetSensorRadius
    {
        get
        {
            if (_sensor == null)
            {
                return 0f;
            }

            float sensorRadius = _sensor.GetSphereCollider.radius;

            float radius = Mathf.Max(sensorRadius * _sensor.transform.lossyScale.x, sensorRadius * _sensor.transform.lossyScale.y);
            radius = Mathf.Max(radius, sensorRadius * _sensor.transform.lossyScale.z);

            return radius;
        }
    }

    /// <summary>
    /// If something of interest enters the radius of our sensor we can start to evaluate things about it
    /// In this example, an exit event does nothing and therefore we leave the method
    /// Adapt this as needed
    /// </summary>
    public void OnSensorEvent(TriggerEventType triggerEvent, Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            Debug.Log("Not Player");
            return;
        }

        if (other != null && (triggerEvent == TriggerEventType.Enter || triggerEvent == TriggerEventType.Stay))
        {
            if (IsColliderVisible(other))
            {
                Debug.Log("Player Found");
                target = other.transform;
            }
        }

        //Depending on your game you might want to do something else on exit
        if(other == null || triggerEvent == TriggerEventType.Exit)
        {
            target = null;
        }
    }

    //Template for taking damage, more will need to be added
    public void TakeDamage(int amount)
    {
        _health -= amount;

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Transition(StateType.Death);
        //Destroy(gameObject);
    }

    /// <summary>
    /// Determines if the object of question is within the field of view
    /// If so, then determine if it is behind a wall or something similar
    /// If the object is within the field of view is not blocked from line of sight
    /// The agent can see them, and therefore return true
    /// </summary>
    protected bool IsColliderVisible(Collider other)
    {
        Vector3 direction = other.transform.position - GetSensorPosition;
        float angle = Vector3.Angle(transform.forward, direction);

        if (angle > _fov * 0.5f)
        {
            return false;
        }

        RaycastHit hit;

        Debug.DrawRay(GetSensorPosition, direction.normalized * GetSensorRadius * _sightRange);

        if (Physics.Raycast(GetSensorPosition, direction.normalized, out hit, GetSensorRadius * _sightRange))
        {
            return hit.collider == other;
        }

        return false;
    }

    /// <summary>
    /// Draws an in engine representation of the agent's line of sight
    /// Make sure the gizmo colour has an alpha above 0 otherwise it cannot be seen
    /// </summary>
    private void OnDrawGizmos()
    {
        if(_sensor == null)
        {
            return;
        }
        #if UNITY_EDITOR
        UnityEditor.Handles.color = _lineOfSightColour;
        Vector3 rotatedForward = Quaternion.Euler(0f, -_fov * 0.5f, 0f) * transform.forward;
        UnityEditor.Handles.DrawSolidArc(GetSensorPosition, Vector3.up, rotatedForward, _fov, GetSensorRadius * _sightRange);
        #endif
    }
}
