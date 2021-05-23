using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CapsuleCollider))]
public class CamCheckWall : MonoBehaviour
{

    private CapsuleCollider capsuleCollider;
    private List<GameObject> listPrevObstacleObject = new List<GameObject>();

    [SerializeField]
    Transform cam;

    [SerializeField]
    SkinnedMeshRenderer skinnedMeshRenderer;

    GameObject obj;

    bool stayCheck;

    // Use this for initialization
    void Start()
    {
        stayCheck = false;
        StartCoroutine("waitForSec");

        capsuleCollider = GetComponent<CapsuleCollider>();
    }


    
    IEnumerator waitForSec()
    {
        yield return new WaitForSeconds(0.1f);
        stayCheck = true;
        StopCoroutine("waitForSec");
    }





    void FixedUpdate()
    {
        if (cam == null) return;

        Vector3 pointCenter = transform.TransformPoint(capsuleCollider.center);
        Vector3 pointLeft = transform.TransformPoint(capsuleCollider.center) - new Vector3(capsuleCollider.radius, 0, 0);
        Vector3 pointRight = transform.TransformPoint(capsuleCollider.center) + new Vector3(capsuleCollider.radius, 0, 0);
        Vector3 pointUp = transform.TransformPoint(capsuleCollider.center) + new Vector3(0, capsuleCollider.height / 2.0f, 0);
        Vector3 pointDown = transform.TransformPoint(capsuleCollider.center) - new Vector3(0, capsuleCollider.height / 2.0f, 0);

        List<Ray> listRay = new List<Ray>();
        Vector3 targetPosition = cam.position; 

        // 
        listRay.Add(new Ray(pointCenter, targetPosition - pointCenter));
        listRay.Add(new Ray(pointLeft, targetPosition - pointLeft));
        listRay.Add(new Ray(pointRight, targetPosition - pointRight));
        listRay.Add(new Ray(pointUp, targetPosition - pointUp));
        listRay.Add(new Ray(pointDown, targetPosition - pointDown));

        List<RaycastHit[]> listHitInfo = new List<RaycastHit[]>();


        foreach (Ray ray in listRay)
        {
            RaycastHit[] hitInfo = Physics.RaycastAll(ray, 1000.0f);
            listHitInfo.Add(hitInfo);
        }

        RaycastHit[] listHit = listHitInfo[0];

        List<GameObject> listNewObstacleObject = new List<GameObject>();

        foreach (RaycastHit hitInfo in listHit)
        {
            if (gameObject.name == hitInfo.collider.name)
            {
                continue;
            }
            if (hitInfo.collider.name.Contains("Bip"))
            {
                continue;
            }

            if (FindColliderByName(listHitInfo[1], hitInfo.collider.name)
                && FindColliderByName(listHitInfo[2], hitInfo.collider.name)
                && FindColliderByName(listHitInfo[3], hitInfo.collider.name)
                && FindColliderByName(listHitInfo[4], hitInfo.collider.name)
                )
            {
                listNewObstacleObject.Add(hitInfo.transform.gameObject);
            }
        }

        foreach (GameObject obstacleObject in listNewObstacleObject)
        {
            if (stayCheck == true &&  !listPrevObstacleObject.Find(delegate (GameObject inObject) { return (inObject.name == obstacleObject.name); }))
            {
                string nameShader = "Transparent/VertexLit";

                MeshRenderer renderer = obstacleObject.GetComponent<MeshRenderer>();
                renderer.material.shader = Shader.Find(nameShader);
                if (renderer.material.HasProperty("_Color"))
                {
                    Color prevColor = renderer.material.GetColor("_Color");
                    renderer.material.SetColor("_Color", new Color(prevColor.r, prevColor.g, prevColor.b, 0.5f));         
                }
            }
        }

        foreach (GameObject obstacleObject in listPrevObstacleObject)
        {
            // remove
            if (!listNewObstacleObject.Find(delegate (GameObject inObject) { return (inObject.name == obstacleObject.name); }))
            {
                // changed to opaque
                string nameShader = "Standard";
                MeshRenderer renderer = obstacleObject.GetComponent<MeshRenderer>();
                renderer.material.shader = Shader.Find(nameShader);
                Color prevColor = renderer.material.GetColor("_Color");
                renderer.material.SetColor("_Color", new Color(prevColor.r, prevColor.g, prevColor.b, 1f));
            }
        }

        listPrevObstacleObject = listNewObstacleObject;             
    }

    private bool FindColliderByName(RaycastHit[] inListRayCastInfo, string inName)
    {
        foreach (RaycastHit hitInfo in inListRayCastInfo)
        {

            if (hitInfo.collider.name == inName)
            { return true; }
        }

        return false;
    }
}