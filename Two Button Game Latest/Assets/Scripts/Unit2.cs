using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit2 : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int healing;
    public int maxMana;
    public int mana;
    public int losemana;
    public int gainmana;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool Heal(int healing)
    {
        currentHP += healing;
        if (currentHP <= 60)
            return true;
        else
            return false;

    }

    public bool ConsumeMana2(int losemana)
    {
        mana -= losemana;

        if (mana <= 150)
            return true;
        else
            return false;
    }

    public bool GainMana2(int gainmana)
    {
        mana += gainmana;

        if (mana <= 150)
            return true;
        else
            return false;
    }


}
