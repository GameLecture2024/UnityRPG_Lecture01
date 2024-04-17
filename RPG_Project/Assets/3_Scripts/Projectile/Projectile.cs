using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float startHeight;

    public GameObject muzzlePrefabs;
    public GameObject hitPrefabs;

    public AudioClip shotSFX;
    public AudioClip hitSFX;

    private bool collided;
    private Rigidbody rigidbody;

    public AttackBehaviour attackBehaviour;
    public GameObject owner;
    public GameObject target;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if(target != null)
        {
            Vector3 dest = target.transform.position;
            dest.y += startHeight;
            transform.LookAt(dest);
        }

        if (owner)
        {
            Collider projectileCollider = GetComponent<Collider>();
            Collider[] ownerColliders = owner.GetComponentsInChildren<Collider>();

            foreach(Collider collider in ownerColliders)
            {
                Physics.IgnoreCollision(projectileCollider, collider);
            }
        }

        if (muzzlePrefabs)
        {
            GameObject muzzleVFX = Instantiate(muzzlePrefabs, transform.position, Quaternion.identity);
            muzzleVFX.transform.forward = gameObject.transform.forward;
            ParticleSystem particle = muzzleVFX.GetComponent<ParticleSystem>();

            if(particle) 
            {
                Destroy(muzzleVFX, particle.main.duration);
            }
            else
            {
                ParticleSystem childParticle = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                if (childParticle)
                {
                    Destroy(muzzleVFX, childParticle.main.duration);
                }
            }
        }

        if(shotSFX != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(shotSFX);
        }
    }

    private void FixedUpdate()
    {
        if(speed != 0 && rigidbody != null)
        {
            rigidbody.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collided)
            return;

        collided = true;

        Collider projectileCollider = GetComponent<Collider>();
        projectileCollider.enabled = false;

        if(hitSFX != null && GetComponent<AudioSource>())
        {
            GetComponent<AudioSource>().PlayOneShot(hitSFX);
        }

        speed = 0;
        rigidbody.isKinematic = true;

        ContactPoint contact = collision.contacts[0];
        Quaternion contactRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 contactPosition = contact.point;

        if (hitPrefabs)
        {
            GameObject hitVFX = Instantiate(hitPrefabs, contactPosition, contactRotation);
            ParticleSystem particle = hitVFX.GetComponent<ParticleSystem>();

            if (particle)
            {
                Destroy(hitVFX, particle.main.duration);
            }
            else
            {
                ParticleSystem childParticle = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                if (childParticle)
                {
                    Destroy(hitVFX, childParticle.main.duration);
                }
            }
        }

        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(attackBehaviour?.damage ?? 0, contactPosition);
        }
    }

    public IEnumerator DestoryParticle(float waitTime)
    {
        if(transform.childCount > 0 && waitTime != 0)
        {
            List<Transform> childs = new List<Transform>();

            foreach(Transform t in transform.GetChild(0).transform)
            {
                childs.Add(t);
            }
            
            while(transform.GetChild(0).localScale.x > 0)
            {
                yield return new WaitForSeconds(0.01f);

                transform.GetChild(0).localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                for(int i = 0; i < childs.Count; i++)
                {
                    childs[i].localScale -= new Vector3(0.1f, 0.1f, 0.1f);
                }
            }
        }

        yield return new WaitForSeconds (waitTime);
        Destroy(gameObject);
    }
}
