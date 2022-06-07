using System;
using Unity.Netcode;
using UnityEngine;

public class ThrowableSpear : MonoBehaviour
{
    [HideInInspector] public int damage;
    public NetworkBehaviourReference player;
    private bool cringeFlag;
    private Animal animal;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude < 5) return;
        transform.LookAt((transform.position - rb.velocity), transform.up);
        transform.Rotate(new Vector3(-90, 0, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (cringeFlag) return;

        if (collision.gameObject.CompareTag("Animal"))
        {
            collision.gameObject.GetComponent<Animal>().TakeDamageServerRpc(damage, player);
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().isTrigger = true;
            animal = collision.gameObject.GetComponent<Animal>();
            if (animal) transform.SetParent(animal.transform);
            transform.position = transform.position + (collision.transform.position - transform.position) * 0.1f;
            cringeFlag = true;
            GetComponent<InventoryGroundItem>().durability -= 0.15f;
            return;
        }

        Destroy(this);
    }
}