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
        Vector3 dir = GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
    }
}
