using UnityEngine;

public class Potion : MonoBehaviour
{
    public int healingPower = 4;
    
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IHealth>().ChangeHealth(healingPower);
        Destroy(gameObject);
    }
}
