using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Global vars")]
    public GameObject myPlayer;
    public float timer;
    public int score; 

    [Header("NPC vars")]
    public GameObject collectible1;
    public float spawnInterval;
    public float spawnTimer;
    public Vector2 spawnXBounds;
    public Vector2 spawnYBounds;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //timer is global, spawnTimer tracks collectibles
        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        //this is the world position where our collectible spawns
        float x = Random.Range(spawnXBounds.x, spawnXBounds.y);
        float y = Random.Range(spawnYBounds.x, spawnYBounds.y);
        Vector3 targetPos = new Vector3(x, y, 0);

        //instantiate and reset timer when condition is met
        if (spawnTimer > spawnInterval)
        {
            Instantiate(collectible1, targetPos, Quaternion.identity);
            spawnTimer = 0;
        }
        public void onCollisionEnter2D(Collision 2D collision) {
            Debug.Log("other " + collision.gameObject.name);
            Debug.Log("other tag " + collision.gameObject.tag);

            if(collision.gameObject.tag == colectible)
        }
    }
}