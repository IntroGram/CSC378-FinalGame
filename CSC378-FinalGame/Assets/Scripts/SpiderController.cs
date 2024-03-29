﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public GameObject player;
    public float attackRange = 20;
    public GameObject spiderShotPrefab;
    public float delayAttackTime;

    public GameObject fireLocation;

    private float delayTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        spiderShotPrefab.GetComponent<SpiderShotController>().player = player;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, attackRange);
        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized*attackRange , Color.red);
        if(hit.rigidbody != null && hit.rigidbody.gameObject == player && delayTime <= 0){
            GameObject shot = Instantiate(spiderShotPrefab, new Vector3(fireLocation.transform.position.x, fireLocation.transform.position.y, fireLocation.transform.position.z), fireLocation.transform.rotation);
            delayTime = delayAttackTime;
        }

        if(delayTime > 0){
            delayTime -= Time.deltaTime;
        }   
    }

}
