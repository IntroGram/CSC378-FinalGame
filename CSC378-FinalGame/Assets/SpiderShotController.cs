using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderShotController : MonoBehaviour
{
    public GameObject player;
    public Vector3 normPosition;
    public float projSpeed;

    void Start()
    {
        // Get last know player position"
        normPosition = (player.transform.position - this.transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the projectile towards player's last position
        transform.position += normPosition * projSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        /*
        if (collision.gameObject != player && collision.gameObject.tag == "Player")
        {
            var damage = Mathf.Lerp(0f, 100f, Mathf.InverseLerp (0f, this.maxVelocity, Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.x)));
            collision.gameObject.GetComponent<PlayerController>().receiveDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }*/
    }
}
