using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceAttackTypeFireAttack : MonoBehaviour
{
    Transform fireBallTransform;
    Rigidbody rid;

    [SerializeField]
    GameObject boomParticle;

    BoxCollider boxCollider;
    MeshRenderer meshRenderer;

    Transform playerTransform;

    bool isStop = false;
    // Start is called before the first frame update
    void Start()
    {
        isStop = false;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        fireBallTransform = GetComponent<Transform>();



        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;


        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        boomParticle.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isStop == false) fireBallTransform.Translate(Vector3.forward * 0.5f);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Wall"
            || other.gameObject.tag == "TrapType2Fire"
            || other.gameObject.tag == "Player")
        {
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
            // rid.velocity = gameObject.transform.forward * 0;
            isStop = true;
            boomParticle.SetActive(true);
            Destroy(gameObject, 3);
        }
    }
}
