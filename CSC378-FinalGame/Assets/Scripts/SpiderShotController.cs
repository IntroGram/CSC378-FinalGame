using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderShotController : MonoBehaviour
{
    public GameObject player;
    public Vector3 normPosition;
    public float projSpeed;
    public float damage = 15f;

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
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().receiveDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
