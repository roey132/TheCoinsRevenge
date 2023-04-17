using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class enemyInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 5;
    public Image[] hearts;
    public GameObject heart;

    private float enemyX;
    private float enemyY;
    private float heartY;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().isDashing)
            {
                health -= 1;
                Debug.Log("health is " + health.ToString());
                hearts[health].gameObject.SetActive(false);
                setHeartPositions(hearts, heartY, enemyX);

                if (health == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }

    }
    void Start()
    {
        
        GameObject heart = Resources.Load<GameObject>("Prefabs/heart");
        enemyX = gameObject.transform.position.x;
        enemyY = gameObject.transform.position.y;
        heartY = enemyY + gameObject.GetComponent<BoxCollider2D>().bounds.size.y;

        hearts = new Image[health];
        for (int i = 0; i < health; i++) 
        {
            GameObject heartObject = Instantiate(heart, new Vector2(enemyX, enemyY), Quaternion.identity, transform.Find("Canvas"));
            hearts[i] = heartObject.GetComponent<Image>();
        }
        setHeartPositions(hearts, heartY, enemyX);
    }

    void setHeartPositions(Image[] hearts, float y, float x)
    {
        for (int i = 0; i< health; i++)
        {
            float heartWidth = hearts[i].rectTransform.rect.width;
            float spacing = heartWidth * 0.5f;
            float totalWidth = (health * heartWidth) + ((health - 1) * spacing);
            float startX = -(totalWidth / 2f) + (heartWidth / 2f) + (i * (heartWidth + spacing));
            hearts[i].transform.position = new Vector2(startX + x,y);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
