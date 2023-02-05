using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ExplodeOnCollision : MonoBehaviour
{

    public GameObject ExplosionPrefab;
    Throwable throwable;
    public float power = 500;
    public GameObject gruntSFX;

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
                var hits = Physics2D.OverlapCircleAll(transform.position, 2F);
                foreach (var hit in hits)
                {
                    if (hit.tag.Equals("Player"))
                    {
                        Vector2 direction = (hit.transform.position - transform.position).normalized;
                        direction = new Vector2(direction.x, Math.Abs(direction.y));
                        var rb = hit.GetComponent<Rigidbody2D>();
                        if (rb)
                        {
                            rb.AddForce(direction * power);
                            // Debug.Log($"Apply {collision.gameObject.name} {direction} * {power}");
                            var pc2d = go.GetComponent<PlayerController2D>();
                            if (pc2d)
                            {
                                pc2d.stunTime = pc2d.maxStunTime;
                                Instantiate(gruntSFX, hit.transform.position, Quaternion.identity);
                            }
                        }
                    }
                }
                Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
