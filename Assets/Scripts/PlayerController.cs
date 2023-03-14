using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBd;
    public float speed = 4f;
    CurrentDirection currDir;
    public bool isPlayerDead;
    private GameManager gameManager;
    public ParticleSystem deadEffect;

    // Start is called before the first frame update
    void Start(){
        rigidBd = GetComponent<Rigidbody>();
        currDir = CurrentDirection.left;
        isPlayerDead = false;
        gameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update(){
        if (!isPlayerDead) {
            RaycastDetector();
            if (Input.GetKeyDown("space")) {
                //if (Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Began) { 
                //herhangi bir dokunmayı tespit etmek için şu an pc de olduğumuzdan değiştirdik
                ChangeDirection();
                PlayerStop();
            }
            else {
                return;
            }
        }                
    }
    //altımızda zemin var mı dedektörü, zamanında dönememişsek kaybetmemizi sağlayacak
    private void RaycastDetector() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position,Vector3.down,out hit)) {
            MovePlayer();
        }
        else {
            PlayerStop();
            isPlayerDead = true;
            this.gameObject.SetActive(false); 
            //burda öldüğümüzde objemizi yok etmiyoruz setactive false yapıyoruz ,verilen particle efekti prefab yapıp initiate ederek efekt verdik
            gameManager.LevelEnd();
            Instantiate(deadEffect, this.transform.position, Quaternion.identity);
        }
    }
    private enum CurrentDirection {
        right,
        left
    }
    private void ChangeDirection() {
        MovePlayer();
        if (currDir==CurrentDirection.right) {
            currDir = CurrentDirection.left;
        }
        else if (currDir==CurrentDirection.left) {
            currDir = CurrentDirection.right;
        }
    }
    private void MovePlayer() {
        if (currDir==CurrentDirection.right) {
            rigidBd.AddForce(speed * Time.deltaTime * Vector3.forward.normalized, ForceMode.VelocityChange);
        }
        else if (currDir==CurrentDirection.left) {
            rigidBd.AddForce(speed * Time.deltaTime * Vector3.right.normalized, ForceMode.VelocityChange);

        }
    }
    private void PlayerStop() {
        rigidBd.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("EndTrigger")) {
            gameManager.WinLevel();
            PlayerStop();
            gameObject.SetActive(false);            
        }
    }
}
