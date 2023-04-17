using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public Collider2D Collider;
    public GameManager Manager;
    // Update is called once per frame
    private void Start()
    {
        Collider = GetComponent<Collider2D>();
        Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "spikes") 
        {
            Manager.playerDeath();
        }

    }
}
