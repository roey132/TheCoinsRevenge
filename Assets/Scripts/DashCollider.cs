using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCollider : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer Render;
    public CircleCollider2D Collider;
    public float ResummonCooldown = 1f;
    void Start()
    {
        Render = GetComponent<SpriteRenderer>();
        Collider = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Render.enabled = false;
            Collider.enabled = false;
            StartCoroutine(WaitSeconds());
            collision.gameObject.GetComponent<PlayerMovement>().canDash = true;
        }

    }
    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(ResummonCooldown);
        Render.enabled = true;
        Collider.enabled = true;
    }
}
