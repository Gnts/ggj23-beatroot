using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCollision : MonoBehaviour
{

    public GameObject ExplosionPrefab;
    Throwable throwable;


    // Start is called before the first frame update
    void Start()
    {
        throwable = GetComponent<Throwable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider)
        {
            GameObject go = collision.collider.gameObject;

            if (go != throwable.owner)
            {
                if (go.tag.Equals("Player"))
                {
                    var direction = collision.transform.position - transform.position;
                    go.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 500f);
                    var pc2d = go.GetComponent<PlayerController2D>();
                    pc2d.stunTime = pc2d.maxStunTime;
                }
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
