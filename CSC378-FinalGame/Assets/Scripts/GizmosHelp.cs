using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosHelp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected(){ 
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(0.5f, 0.5f, 0));
    }
}
