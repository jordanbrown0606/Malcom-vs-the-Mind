using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour
{
    private Agent _myAgent;             //Tracks who owns this sensor
    private SphereCollider _collider;   //Store the sphere collider which dictates what is and isn't in line of sight or hearing

    /// <summary>
    /// Get the attached sphere collider
    /// If we have not set up the sphere collider yet, do so now
    /// </summary>
    public SphereCollider GetSphereCollider
    {
        get
        {
            if (_collider == null)
            {
                _collider = GetComponent<SphereCollider>();
            }

            return _collider;
        }
    }

    /// <summary>
    /// Make sure the sphere collider is a trigger, we dont want objects bouncing off our line of sight
    /// Make sure a parent that is an Agent exists, if not send a message to let us know about the issue
    /// </summary>
    private void Awake()
    {
        GetSphereCollider.isTrigger = true;

        if (GetComponentInParent<Agent>() != null)
        {
            _myAgent = GetComponentInParent<Agent>();
        }
        else
        {
            Debug.LogError("ERROR: Sensors require a parent with an Agent attached");
        }
    }

    /// <summary>
    /// We cannot run trigget and/or collision events from other scripts
    /// This set of methods acts as a workaround so the agent is aware of things entering or leaving its line of sight
    /// </summary>

    private void OnTriggerEnter(Collider other)
    {
        _myAgent?.OnSensorEvent(TriggerEventType.Enter, other);
    }

    private void OnTriggerStay(Collider other)
    {
        _myAgent?.OnSensorEvent(TriggerEventType.Stay, other);
    }

    private void OnTriggerExit(Collider other)
    {
        _myAgent?.OnSensorEvent(TriggerEventType.Exit, other);
    }
}


public enum TriggerEventType
{
    Enter,
    Stay,
    Exit
}