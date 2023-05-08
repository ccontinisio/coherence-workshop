using UnityEngine;
using UnityEngine.Events;

public class TriggerDetection : MonoBehaviour
{
    public string tagToCheckFor = "Player";
    
    public UnityAction<GameObject> TriggerEntered;
    public UnityAction<GameObject> TriggerExited;
    
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
