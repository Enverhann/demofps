using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    //Guns/Shooting
    RaycastHit hit;
    public GameObject rayPointer;
    public bool canFire;
    float gunTimer;
    public float gunCD;
    public ParticleSystem muzzle;
    public float range;
    public GameObject impact;
    public float damage;
    public bool isAuto;
    public GameObject anims;

    //Ammo
    public float ammoInGun;
    public float ammoInPocket;
    public float maxAmmo;
    float addableAmmo;
    public Text ammoCount;
    public Text pocketAmmoCount;

    //Reload
    public float reloadCD;
    float reloadTimer;

    private new GameObject gameObject;

    //Recoil
    private Recoil recoilScript;

    //Sound Effects
    AudioSource audioSource;
    public AudioClip firingSound;
    public AudioClip reloadSound;
    Animator gunAnim;
  

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunAnim = GetComponent<Animator>();
        gameObject = GameObject.FindGameObjectWithTag("Polygon");
        recoilScript = GameObject.Find("CameraRot/CameraRecoil").GetComponent<Recoil>();
    }

    
    void Update()
    {
        ammoCount.text = ammoInGun.ToString();
        pocketAmmoCount.text = ammoInPocket.ToString();
        addableAmmo = maxAmmo - ammoInGun;

        if(addableAmmo > ammoInPocket)
        {
            addableAmmo = ammoInPocket;
        }

        if (Input.GetKey(KeyCode.D) && isAuto && canFire==true && Time.time > gunTimer && ammoInGun>0)
        {
            Firing();
            recoilScript.RecoilFire();
            gunTimer = Time.time + gunCD;
        }
        if (Input.GetKeyDown(KeyCode.D) && !isAuto && canFire == true && Time.time > gunTimer && ammoInGun > 0)
        {
            Firing();
            recoilScript.RecoilFire2();
            gunTimer = Time.time + gunCD;
        }
        if (Input.GetKeyDown(KeyCode.R) && addableAmmo > 0 && ammoInPocket > 0)
        {
            if(Time.time > reloadTimer)
            {
                StartCoroutine(Reload());
                reloadTimer = Time.time + reloadCD;
            }
        }
            
        if (ammoInGun == 0)
        {
            Debug.Log("Empty Magazine");
        }     
    }

    private void Firing()
    {
        ammoInGun--;  
        
            if (Physics.Raycast(rayPointer.transform.position, rayPointer.transform.forward, out hit, range))
            {
                audioSource.clip = firingSound;
                muzzle.Play();
                audioSource.Play();


                Debug.Log("Shot Fired");
                Debug.Log((hit.transform.name) + "Got Hit");
                gunAnim.Play("Fire", -1, 0f);
                Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));

                if (hit.collider.tag == "Polygon")
                {
                    hit.collider.gameObject.transform.root.GetComponent<ShootingRange>().health = hit.collider.gameObject.transform.root.GetComponent<ShootingRange>().health - damage;
                }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameObject.SetActive(true);
                hit.collider.gameObject.transform.root.GetComponent<ShootingRange>().health = 100;
            }
            }
            else
            {
            audioSource.clip = firingSound;
            muzzle.Play();
            audioSource.Play();
            
            Debug.Log("Shot Fired");
            Debug.Log("Missed");
            gunAnim.Play("Fire", -1, 0f);
            }
    }

    IEnumerator Reload()
    {
        canFire = false;
        gunAnim.SetBool("isReloading", true);

        audioSource.clip = reloadSound;
        audioSource.Play();
        Debug.Log("Reloading");
        yield return new WaitForSeconds(1.5f);
        gunAnim.SetBool("isReloading", false);

        if (isAuto == true) { 
        yield return new WaitForSeconds(3.5f);
        ammoInGun = ammoInGun + addableAmmo;
        ammoInPocket = ammoInPocket - addableAmmo;
        canFire = true;
        }
        else
        {
            yield return new WaitForSeconds(2.6f);
            ammoInGun = ammoInGun + addableAmmo;
            ammoInPocket = ammoInPocket - addableAmmo;
            canFire = true;
        }
    }

    public void ResetAnim()
    {
        anims.GetComponent<Animator>().Rebind();
        canFire = true;
    }
}
