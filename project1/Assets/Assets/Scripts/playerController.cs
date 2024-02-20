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
        //Dir();
        transform.Translate(Dir());
    }

    public Vector3 Dir()
    {
        //referencing Unity's virtual axis - these pick up KBM OR controller inputs
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        myDir = new Vector3(x, y, 0); //combining them into one vector
                                      // Debug.Log(myDir);
        return myDir; //return the value
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("hit");
        if (collision.gameObject.tag == "projectile")
        {
            isAlive = false; 
            //Debug.Log("other " + collision.gameObject.name);
            //Debug.Log("other tag " + collision.gameObject.tag);
        }

    }
}