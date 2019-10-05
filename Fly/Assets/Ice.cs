using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour,IDamagable
{
    public float MaxHP;
    public float HPRecoverGap;
    public float HPRecoverSpeed;

    public bool Damaging { get; set; }
    public float DamageOverTime;

    public float CurrentHP;
    private float RecoverGapTimeCount;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamaging();
    }

    private void CheckDamaging()
    {
        if (Damaging)
        {
            RecoverGapTimeCount = 0;
            CurrentHP -= DamageOverTime * Time.deltaTime;
            if (CurrentHP <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            RecoverGapTimeCount += Time.deltaTime;
            if (RecoverGapTimeCount > HPRecoverGap)
            {
                CurrentHP += HPRecoverSpeed * Time.deltaTime;
                if (CurrentHP >= MaxHP)
                {
                    CurrentHP = MaxHP;
                }
            }
        }
    }
}
