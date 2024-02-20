using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //speed is a multiplier used to control actual speed of player
    public float speed;
    private Vector3 myDir;
    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= 8)
        {
            transform.position = new Vector3(8, transform.position.y, 0);
        }
        if (transform.position.x <= -8)
        {
            transform.position = new Vector3(-8, transform.position.y, 0);
        }
        Dir();
        transform.Translate(Dir());
        
        
    }

    public Vector3 Dir()
    {
        //referencing Unity's virtual axis - these pick up KBM OR controller inputs
        //float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        myDir = new Vector3(x*speed, 0, 0); //combining them into one vector
        //Debug.Log(myDir);
        return myDir; //return the value
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("hit");
        if (collision.gameObject.CompareTag("projectile"))
        {
            isAlive = false; 
            //Debug.Log("other " + collision.gameObject.name);
            //Debug.Log("other tag " + collision.gameObject.tag);
        }
        if (collision.gameObject.CompareTag("collectible"))
        {
            Debug.Log("collected");
            Destroy(collision.gameObject);
        }

    }
   /* private void onTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("collectible"))
        {
            Destroy(other.gameObject);
            Debug.Log("collected");
        }
    } */
}