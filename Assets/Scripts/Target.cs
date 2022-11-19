using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticle;

    private Rigidbody targetRb;
    private GameManager gameManager;


    // Min-Max Values
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float torque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    // Score
    public int point = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = gameObject.GetComponent<Rigidbody>();
        
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.UpdateLives(-1);
        }
    }

    public void DestroyTarget()
    {
        if (!gameManager.isGameOver)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(point);

            Destroy(gameObject);
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
