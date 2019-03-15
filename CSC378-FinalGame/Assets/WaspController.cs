using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspController : MonoBehaviour
{
    public Transform PointToPatrol;
    public float speed = 1.0f;

    private Vector3 pos1;
    private Vector3 pos2;

    private float lastPos;
    private SpriteRenderer waspSprite;

    void Start(){
        pos1 = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        pos2 = new Vector3(PointToPatrol.transform.position.x, PointToPatrol.transform.position.y, PointToPatrol.transform.position.z);
        waspSprite = GetComponent<SpriteRenderer>();
        lastPos = this.transform.position.x;
    }

    void Update() {
        transform.position = Vector3.Lerp (pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
        if(this.transform.position.x > lastPos){
            waspSprite.flipX = true;
        }else{
            waspSprite.flipX = false;
        }
        lastPos = this.transform.position.x;
    }
}
