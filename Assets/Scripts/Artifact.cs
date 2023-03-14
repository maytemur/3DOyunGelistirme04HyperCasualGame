using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public int minScore, maxScore;
    private GameManager gameManager;
    public ParticleSystem collectEffect;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            gameManager.AddScore(Random.Range(minScore, maxScore));
            //Destroy(gameObject);
            collectEffect.Play();
            Destroy(gameObject, 0.5f);
        }
    }
    private void Update() {
        transform.Rotate(0f, 0f, -360f * Time.deltaTime);
    }
}
