using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A generic script to apply to a trigger Collider, to listen to OnTriggerEnter and OnTriggerExit
/// and notify an observer via the events <see cref="TriggerEntered"/> and <see cref="TriggerExited"/>.
/// Useful for melee weapons or for area damage, like traps.
/// </summary>
public class TriggerDetection : MonoBehaviour
{
    public string tagToCheckFor = "Player";
    
    public event UnityAction<GameObject> TriggerEntered;
    public event UnityAction<GameObject> TriggerExited;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToCheckFor))
        {
            TriggerEntered?.Invoke(other.gameObject);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagToCheckFor))
        {
            TriggerExited?.Invoke(other.gameObject);
        }
    }
}
