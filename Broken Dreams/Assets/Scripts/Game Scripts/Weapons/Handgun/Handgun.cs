﻿using UnityEngine;
using System.Collections;

public class Handgun : MonoBehaviour {

    //Animation
    public Animator animator;

    //Functionality

    public Camera player;
    public float damage = 5;
    public float range = 5;
    public float force = 5;
    public float fireRateDelay = 0.35f;
    public bool canShoot = true;
    public int startingAmmo = 0;
    public int magazineSize = 7;

    protected int ammo = 0;
    protected int reserveAmmo = 0;

    //VIsuals

    public GameObject impactEffect;
   // public ParticleSystem muzzleFlash;

    void Awake()
    {
        if (startingAmmo > 0)
        {
            reserveAmmo += startingAmmo;
            if (reserveAmmo >= magazineSize)
            {
                ammo += magazineSize;
                reserveAmmo -= magazineSize;
            }
            else
            {
                ammo += reserveAmmo;
                reserveAmmo = 0;
            }
        }
    }
   
    void Update()
    {
        if (!WeaponWheel.isShowing)
        {
            if (Input.GetMouseButton(0))
            {
                if (canShoot && ammo > 0)
                    StartCoroutine(Shoot(fireRateDelay));
            }
            if (Input.GetKeyDown(KeyCode.R))
                StartCoroutine(Reload(2f));
        }

    }

    IEnumerator Shoot(float x)
    {
        canShoot = false;
        int rand = Random.RandomRange(1, 3);
        switch (rand)
        {
            case 1:
                animator.Play("Shoot1");
                break;
            case 2:
                animator.Play("Shoot2");
                break;
        }
        if (!(this.animator.GetCurrentAnimatorStateInfo(1).IsName("Shoot1") && this.animator.GetCurrentAnimatorStateInfo(1).IsName("Shoot2")))
            Fire();
        yield return new WaitForSeconds(x);
        canShoot = true;
        StopCoroutine(Shoot(x));
    }

    IEnumerator Reload(float x)
    {
        if (reserveAmmo > 0 && ammo < magazineSize)
        {
            canShoot = false;
            animator.Play("Reload");
            yield return new WaitForSeconds(x);
            Reload();
            canShoot = true;
        }

        StopCoroutine(Reload(x));
    }

    //Functionality

    void Fire()
    {
        //   muzzleFlash.Play();
        ammo--;
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range, 1 << LayerMask.NameToLayer("Default")))
        {
            //Debug.Log(hit.transform.name);

            if (hit.transform.GetComponent<DestroyableObject>() != null)
            {
                hit.transform.GetComponent<DestroyableObject>().takeDamage(damage);
            }

            else if (hit.transform.GetComponent<DamagePoint>() != null)
            {
                hit.transform.GetComponent<DamagePoint>().takeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 1000 * force);
            }

            //Particle Effect
            GameObject effectParticle = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
            Destroy(effectParticle, 2f);
        }
    }

    void Reload()
    {
        int ammoValue;
        if (reserveAmmo > 0)
        {
            while (ammo < magazineSize && reserveAmmo > 0)
            {
                ammo++;
                reserveAmmo--;
            }
        }
    }
   
    //Outisde Access Functions

    public void addAmmo(int x)
    {
        reserveAmmo += x*magazineSize;
    }

    public int getAmmo()
    {
        return ammo;
    }

    public int getReserveAmmo()
    {
        return reserveAmmo;
    }
}
