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
    ParticleSystem particleSys;
    bool isStop = false;






    void Start()
    {
        isStop = false;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        fireBallTransform = GetComponent<Transform>();
        particleSys = GetComponent<ParticleSystem>();
        rid = GetComponent<Rigidbody>();

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;


        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        boomParticle.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isStop == false) fireBallTransform.Translate(Vector3.forward * 0.2f);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DistanceAttackEnemy01") return;

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Wall")
        {
            if (PlayerInputScript.Instance.isDodge == true) return;

            isStop = true;

            particleSys.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
            rid.velocity = gameObject.transform.forward * 0;

            boomParticle.SetActive(true);
            Destroy(gameObject, 3);

            return;
        }
    }
}
