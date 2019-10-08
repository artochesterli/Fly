using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbility : MonoBehaviour
{
    public float MaxEnergy;
    public float EnergyRecoverGap;
    public float EnergyCostSpeed;
    public float EnergyRecoverSpeed;
    public GameObject EnergyBar;
    public float MaxEnergyIncrement;

    public float FireDis;
    public float FireOffset;

    private float CurrentEnergy { get { return GetComponent<PlayerController>()._curEnergy; } set { GetComponent<PlayerController>()._curEnergy = value; } }
    private bool EnergyMaxed;
    private float EnergyRecoverGapTimeCount;
    public bool EnergyCosting;
    private GameObject Fire;
    private GameObject CastiedIce;

    // Start is called before the first frame update
    void Start()
    {

        CurrentEnergy = MaxEnergy;
        EnergyMaxed = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (InputActivate())
        {
            EnergyCosting = true;
        }

        if (InputDeactivate())
        {
            EnergyCosting = false;
        }

        if (EnergyCosting)
        {
            EnergyRecoverGapTimeCount = 0;
            //CurrentEnergy -= EnergyCostSpeed * Time.deltaTime;
            GetComponent<PlayerController>()._updateEnergy(-EnergyCostSpeed * Time.deltaTime);

            if (CurrentEnergy <= 0)
            {
                Destroy(Fire);
                CurrentEnergy = 0;
                EnergyCosting = false;
            }
            else
            {
                CastIce();
            }
            EnergyMaxed = false;
        }
        else
        {
            Destroy(Fire);
            if (CastiedIce != null)
            {
                CastiedIce.GetComponent<IDamagable>().Damaging = false;
                CastiedIce = null;
            }
        }

        
    }

    private void CastIce()
    {
        int layermask = 1 << LayerMask.NameToLayer("Ice");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, FireDis, layermask))
        {
            Vector3 FirePos = hit.point + hit.normal * FireOffset;
            if (!Fire)
            {
                Fire = (GameObject)Instantiate(Resources.Load("Prefabs/Fire"), FirePos, Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Fire.transform.position = FirePos;
                CastiedIce = hit.collider.gameObject;
                CastiedIce.GetComponent<IDamagable>().Damaging = true;
            }
        }
        else
        {
            Destroy(Fire);
            if (CastiedIce != null)
            {
                CastiedIce.GetComponent<IDamagable>().Damaging = false;
                CastiedIce = null;
            }
        }
    }

    private bool InputActivate()
    {
        return Input.GetMouseButtonDown(0);
    }

    private bool InputDeactivate()
    {
        return Input.GetMouseButtonUp(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnergyBox"))
        {
            GetComponent<PlayerController>().MaxBoosterSpeed += GetComponent<PlayerController>().MaxBoosterSpeed * MaxEnergyIncrement / GetComponent<PlayerController>().MaxEnergy;
            GetComponent<PlayerController>().MaxEnergy += MaxEnergyIncrement;
            Destroy(other.gameObject);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("EnergyBox"))
        {
            MaxEnergy += MaxEnergyIncrement;
            Destroy(hit.gameObject);
        }
    }

}
