using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    //Rotations
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    //Recoil
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float snappy;
    [SerializeField] private float returnSpeed;

    [SerializeField] private float recoilX2;
    [SerializeField] private float recoilY2;
    [SerializeField] private float recoilZ2;



    void Start()
    {

    }

    void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappy * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }
    public void RecoilFire2()
    {
        targetRotation += new Vector3(recoilX2, Random.Range(-recoilY2, recoilY2), Random.Range(-recoilZ2, recoilZ2));
    }
}
