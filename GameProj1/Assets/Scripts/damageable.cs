using System.Collections;
using UnityEngine;

public class damageable : MonoBehaviour, iDamage
{
    [SerializeField] Renderer model;

    Color colorOrig;

    public void takeDmg(float damage)
    {
        StartCoroutine(flashRed());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator flashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;
    }
}
