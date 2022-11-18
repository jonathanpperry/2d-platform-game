using UnityEngine;

public class HurtEnemyOnContact : MonoBehaviour
{
    public int damageToGive;

    public float bounceOnEnemy;

    private Rigidbody2D myrigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        myrigidBody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthManager>().GiveDamage(damageToGive);
            myrigidBody2D.velocity = new Vector2(myrigidBody2D.velocity.x, bounceOnEnemy);

        }
    }
}
