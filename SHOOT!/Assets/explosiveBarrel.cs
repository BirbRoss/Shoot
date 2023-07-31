using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveBarrel : MonoBehaviour
{

    LayerMask mask;
    public float range = 5f;
    public float explosiveForce = 1000f;
    public ParticleSystem explosionParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Target");
    }

    public void explode()
    {

        Instantiate(explosionParticleSystem, transform.position, Quaternion.identity);

        Collider[] hits = Physics.OverlapSphere(this.transform.position, range, mask);
        foreach (Collider hit in hits)
        {
            //Disables animator so it does't break the rigidbody
            hit.transform.root.GetChild(0).gameObject.GetComponent<Animator>().enabled = false;

            //Disables loser timer on that bandit
            hit.transform.root.GetChild(0).gameObject.GetComponent<banditShoot>().enabled = false;

            //Enabled ragdoll and adds force
            hit.transform.root.GetChild(0).SendMessage("setRigidBodiesKinematic", false);
            hit.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, range, 3.0f);

            //Sends a message to the root object (highest parent object) to remove one off the score and only if the name is spine, as bandits only have one spine, so it doesn't send it one bajillion times
            if (hit.transform.name == "spine")
            {
                hit.transform.root.GetChild(0).gameObject.SendMessage("RemoveOne");
            }
            
        }

        gameObject.SetActive(false); //Change to timed so particles play first?
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
