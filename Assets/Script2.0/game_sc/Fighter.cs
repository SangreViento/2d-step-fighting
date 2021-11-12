using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Fighter : MonoBehaviour
{
    public int health = 0; // � ���������� health ���� int ��������� �������� 100 ������ ������������ ���������� HP 
    public int attack_power = 0; // � ���������� strike_power ���� int ��������� �������� 5 ������ ���� �����
    public int armor = 0; // ����� ���������
    public int crit = 0; // ���� ������������ ����� � ��������� ���� �2
    public int ap = 2; // ���� ��������
    
    public Fighter enemy;

    public Dictionary<string, bool> actions = new Dictionary<string, bool>();
    
    SpriteRenderer appearance;
    Transform pos;
    AudioSource punch_audio;

    private IEnumerator attacked;
   
    public IEnumerator Damaged_animation()
    {
        appearance.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        appearance.color = Color.white;
    }

    void Start()
    {
        appearance = GetComponent<SpriteRenderer>();
        punch_audio = GetComponent<AudioSource>();

        actions.Add("AHead", false);
        actions.Add("ABody", false);
        actions.Add("AFoot", false);
        actions.Add("DHead", false);
        actions.Add("DBody", false);
        actions.Add("DFoot", false);
    }

    public void clearActions()
    {
        actions["AHead"] = false;
        actions["ABody"] = false;
        actions["AFoot"] = false;
        actions["DHead"] = false;
        actions["DBody"] = false;
        actions["DFoot"] = false;
    }
    public void reciveFlags(Dictionary<string, bool> flags)
    {
        actions = flags;
    }
    int checkCrit()
    {
        if (Random.Range(1,100) <= crit)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public void Attack()
    {
        enemy.Defence(actions["AHead"], actions["ABody"], actions["AFoot"], attack_power);
    }

    public void Defence(bool ah, bool ab, bool af, int dmg)
    {
        if (ah && actions["DHead"] != true)
        {
            if ((health - (dmg*checkCrit() - armor))>=0)
            health -= (dmg*checkCrit() - armor);
            else
            health = 0;
            Debug.Log("Atack power " + (dmg*checkCrit()).ToString() + " Blocked " + armor.ToString() + " Demage " + (dmg*checkCrit() - armor).ToString());
        }
        
        if (ab && actions["DBody"]!= true)
        {
            if ((health - (dmg*checkCrit() - armor))>=0)
            health -= (dmg*checkCrit() - armor);
            else
            health = 0;
            Debug.Log("Atack power " + (dmg*checkCrit()).ToString() + " Blocked " + armor.ToString() + " Demage " + (dmg*checkCrit() - armor).ToString());
        }

        if (af && actions["DFoot"]!= true)
        {
            if ((health - (dmg*checkCrit() - armor))>=0)
            health -= (dmg*checkCrit() - armor);
            else
            health = 0;
            Debug.Log("Atack power " + (dmg*checkCrit()).ToString() + " Blocked " + armor.ToString() + " Demage " + (dmg*checkCrit() - armor).ToString());
        }
    }
}
