using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpCollider : MonoBehaviour
{
    public float ResummonColliderCooldown = 1f;
    public SpriteRenderer Renderer;
    public CircleCollider2D Collider2D;

    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Collider2D = GetComponent<CircleCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Renderer.enabled = false;
            Collider2D.enabled = false;
            collision.gameObject.GetComponent<PlayerMovement>().canAirJump = true;
            StartCoroutine(ResummonObject());
        }

    }
    IEnumerator ResummonObject()
    {
        yield return new WaitForSeconds(ResummonColliderCooldown);
        Renderer.enabled = true;
        Collider2D.enabled=true;
    }
}
