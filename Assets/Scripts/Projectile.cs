using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    private Transform target;
    public Transform shooter; // Records who fired the projectile
    public int damage = 1;  // Damage value of the projectile

    public void SetTarget(Transform newTarget, Transform shooter)
    {
        target = newTarget;
        this.shooter = shooter; // Store the attacker
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            if (target.CompareTag("Enemy"))
            {
                target.GetComponent<Enemy>().TakeDamage(damage, shooter); // Deal damage and aggro the enemy
            }
            Destroy(gameObject); 
        }
    }
}
