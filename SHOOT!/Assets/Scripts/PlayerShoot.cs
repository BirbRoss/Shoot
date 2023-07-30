using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    Camera cam;
    LayerMask mask;
    public Animator anim;
    public float animSpeed;

    public float Firerate = 0.25f;
    [SerializeField]
    private float LastShot = 0f;

    public ParticleSystem ShootingSystem;
    public ParticleSystem ImpactParticleSystem;
    public TrailRenderer BulletTrail;
    public Transform BulletSpawnPoint;
    public float bulletForce = 50f;
    public float BulletSpeed = 100;
    public float humanMultiplier = 5f;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        mask = LayerMask.GetMask("Target");
        anim.speed = animSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if (Input.GetMouseButtonDown(0) && LastShot + Firerate < Time.time)
        {

            ShootingSystem.Play();
            anim.Play("ShootGun");

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 30, mask))
            {
                //Hit, Turn object red and spurt blood
                //Should probably alter for non-fleshy targets

                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));


                if (hit.rigidbody != null)
                {
                    if (hit.transform.root.GetChild(0).GetComponent<controlRagdoll>() != null)
                    {
                        //Disables animator so it does't break the rigidbody
                        hit.transform.root.GetChild(0).gameObject.GetComponent<Animator>().enabled = false;

                        //Disables loser timer on that bandit
                        hit.transform.root.GetChild(0).gameObject.GetComponent<banditShoot>().enabled = false;

                        //Enabled ragdoll and adds force
                        hit.transform.root.GetChild(0).SendMessage("setRigidBodiesKinematic", false);
                        hit.rigidbody.AddForceAtPosition(ray.direction * bulletForce * humanMultiplier, hit.point);

                        //Sends a message to the root object (highest parent object) to remove one off the score
                        hit.transform.root.GetChild(0).gameObject.SendMessage("RemoveOne");
                    }
                    else
                    {
                        hit.rigidbody.AddForceAtPosition(ray.direction * bulletForce, hit.point);
                    }
                }
                
                if (hit.transform.GetComponent<Renderer>() != null)
                {
                    hit.transform.GetComponent<Renderer>().material.color = Color.red;
                }
                

                LastShot = Time.time;
            }
            else
            {
                //Miss
                TrailRenderer trail = Instantiate(BulletTrail, BulletSpawnPoint.position, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, BulletSpawnPoint.position + ray.direction * 30, Vector3.zero, false));
                LastShot = Time.time;
            }
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 Hit, Vector3 HitNormal, bool MadeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, Hit);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, Hit, 1 - (remainingDistance / distance));

            remainingDistance -= BulletSpeed * Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = Hit;
        if (MadeImpact)
        {
            Instantiate(ImpactParticleSystem, Hit, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}
