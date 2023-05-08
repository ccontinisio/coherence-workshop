using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 4;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IHealth>().ChangeHealth(damage);
        Destroy(gameObject);
    }
}
