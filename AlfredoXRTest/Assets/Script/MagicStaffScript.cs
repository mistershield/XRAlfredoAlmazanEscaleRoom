using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStaffScript : MonoBehaviour
{
    [SerializeField] private float projecctileSpeed = 10f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject projectilePivot;
    [SerializeField] private AudioSource audioSource;

    private float originalFireRate;
    public bool playerShoot;
    private void Start()
    {
        originalFireRate = fireRate;
    }
    private void Update()
    {
        if (playerShoot)
        {
            fireRate-= Time.deltaTime;
            if(fireRate <= 0)
            {
                playerShoot = false;
                fireRate = originalFireRate;
            }
        }
    }
    public void FireMagic()
    {
        if (!playerShoot)
        {
            GameObject tmpProjectile = Instantiate(projectile, projectilePivot.transform.position, Quaternion.identity);
            tmpProjectile.GetComponent<Rigidbody>().AddForce(projectilePivot.transform.forward * projecctileSpeed, ForceMode.Impulse);
            audioSource.Play();
            playerShoot = true;
        }
    }
}
