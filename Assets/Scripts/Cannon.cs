using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // Start is called before the first frame update
    private float cannonXPos;
    private float cannonYPos;

    public float xShootForce = 1000f;
    public float yShootForce = 1000f;
    public float shootDir = -1;
    public float cooldown = 5f;
    private float timer;

    public bool ballIsActive;

    private GameObject cannonBall;
    void Start()
    {
        timer = cooldown;
        cannonXPos = gameObject.transform.position.x;
        cannonYPos = gameObject.transform.position.y;

        GameObject ballPrefab = Resources.Load<GameObject>("Prefabs/CannonBall");
        cannonBall = Instantiate(ballPrefab,new Vector2(cannonXPos,cannonYPos),quaternion.identity,transform);
        cannonBall.transform.localScale = Vector2.one;
        cannonBall.SetActive(false);
        ballIsActive = false;
        ShootBall(cannonBall);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!ballIsActive)
        {
            resetBallPos(cannonXPos, cannonYPos, cannonBall);
            ShootBall(cannonBall);
            ballIsActive=true;
        }

    }
    private void ShootBall(GameObject ball)
    {
        ball.transform.localScale = Vector2.one;
        ball.SetActive(true);
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        ballRb.AddForce(new Vector2(xShootForce * shootDir,yShootForce));
        
    }
    private void resetBallPos(float x,float y, GameObject ball)
    {
        ball.transform.position = new Vector2(x,y);
        ball.transform.localScale = Vector2.one;
        ball.SetActive(false);
    }
}
