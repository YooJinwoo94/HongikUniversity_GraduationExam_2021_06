using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TrapType2FireAttack : MonoBehaviour
{
    Transform fireBallTransform;
    Rigidbody rid;

    [SerializeField]
    GameObject boomParticle;

    BoxCollider boxCollider;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        fireBallTransform = GetComponent<Transform>();
        rid = GetComponent<Rigidbody>();

        fireBallTransform.rotation = Quaternion.Euler(0, -90, 0);
        rid.velocity = gameObject.transform.forward * 20;

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;


        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
        boomParticle.SetActive(false);
    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Wall" || other.gameObject.tag == "TrapType2Fire")
        {
            boxCollider.enabled = false;
            meshRenderer.enabled = false ;
            rid.velocity = gameObject.transform.forward * 0;

            boomParticle.SetActive(true);
            Destroy(gameObject, 3);
        }
        else if (other.gameObject.tag == "Player")
        {
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
            rid.velocity = gameObject.transform.forward * 0;

            boomParticle.SetActive(true);
            Destroy(gameObject, 3);
        }
    }
}
