using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] weaponStats weapon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapon.currentAmmo = weapon.maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            iPickup pickup = other.GetComponent<iPickup>();
            pickup.GetWeapon(weapon);
            Destroy(gameObject);
        }
    }
}
