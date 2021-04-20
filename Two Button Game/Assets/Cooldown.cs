using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour
{
    public Image AttackCooldown;
    public float cooldown1 = 5;
    bool isCooldown = false;
    public KeyCode Cooldown1;
    // Start is called before the first frame update
    void Start()
    {
        AttackCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
         Attack1();
    }

    void Attack1()
    {
        if(Input.GetKey(Cooldown1) && isCooldown == false)
        {
            isCooldown = true;
             AttackCooldown.fillAmount = 1;

            if(isCooldown)
            {
                AttackCooldown.fillAmount -=1 / cooldown1 * Time.deltaTime;
                {
                    if(AttackCooldown.fillAmount <= 0)
                    {
                        AttackCooldown.fillAmount = 0;
                        isCooldown = false;
                    }
                }
            }
        }
    }
}

