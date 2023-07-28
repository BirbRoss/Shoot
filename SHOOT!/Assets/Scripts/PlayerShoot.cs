using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    Camera cam;
    LayerMask mask;
    public Animator anim;
    public float animSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        mask = LayerMask.GetMask("RayCast");
        anim.speed = animSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                Debug.Log(hit.transform.name);
                hit.transform.GetComponent<Renderer>().material.color = Color.red;
                anim.Play("ShootGun");
            }
            else
            {
                Debug.Log("no hit");
            }
        }
    }
}
