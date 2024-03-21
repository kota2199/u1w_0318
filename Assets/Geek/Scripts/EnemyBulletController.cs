using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{

    public float speed = 1.0f;

    public float lifeTime = 3.0f;

    public Vector2 launchVector = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        Invoke("AutoDestroy", lifeTime);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(launchVector * speed * Time.deltaTime);
    }

    private void AutoDestroy()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Damage
            Destroy(this.gameObject);
        }
    }
}
