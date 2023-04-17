using Unity.VisualScripting;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    GameManager gameManager;
    public float speed = 5f;
    public float cooldown = 5f;
    private Transform parentCannon;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        parentCannon = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            gameManager.playerDeath();
        }
        if (!collision.collider.name.Contains("Cannon"))
        {
            gameObject.SetActive(false);
            parentCannon.GetComponent<Cannon>().ballIsActive = false;
        }

    }
}
