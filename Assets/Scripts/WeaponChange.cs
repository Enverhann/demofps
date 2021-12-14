using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    //Weapons Change
    public GameObject[] guns;
    int index;
    
    //Change Sound
    AudioSource audioSource;
    public AudioClip weaponChangeSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        index = 1;
        ChangeWeapon(1);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && index == 2)
        {
            ChangeWeapon(1);
            Debug.Log("M4A1 Selected");
            audioSource.clip = weaponChangeSound;
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.W) && index == 1)
        {
            ChangeWeapon(2);
            Debug.Log("Beretta M9 Selected");
            audioSource.clip = weaponChangeSound;
            audioSource.Play();
        }
    }

    public void ChangeWeapon(int number)
    {
        guns[index - 1].GetComponent<Fire>().ResetAnim();

        for (int i =0; i< guns.Length; i++)
        {
            guns[i].SetActive(false);
        }
        guns[number - 1].SetActive(true);
        index = number;
    }
}
