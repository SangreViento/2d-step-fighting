using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Fighter : MonoBehaviour
{
    public int max_hp = 0;
    public int max_ap = 0;
    public int health = 0; 
    public int attack_power = 0; 
    public int armor = 0; 
    public int crit = 0; 
    public int ap = 2; 
    
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
        max_hp = health;
        max_ap = ap;
        appearance = GetComponent<SpriteRenderer>();
        punch_audio = GetComponent<AudioSource>();

        actions.Add("AHead", false);
        actions.Add("ABody", false);
        actions.Add("AFoot", false);
        actions.Add("DHead", false);
        actions.Add("DBody", false);
        actions.Add("DFoot", false);
    }

    public void Refresh_stats()
    {
        health = max_hp;
        ap = max_ap;
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

    public void Ai_Prepare_attack()
    {
        {
            for (int i = ap; i > 0; i--)
            {
                if ((int)Random.Range(1,3) == 1)
                {
                    Debug.Log("Attack action");
                    int rnd = (int)Random.Range(1,3);
                    if (rnd == 1)
                        if (actions["AHead"] != true)
                            actions["AHead"] = true;
                        else
                            i++;
                    if (rnd == 2)
                        if (actions["ABody"] != true)
                            actions["ABody"] = true;
                        else
                            i++;
                    if (rnd == 3)
                        if (actions["AFoot"] != true)
                            actions["AFoot"] = true;
                        else
                            i++;
                }
                else
                {
                    Debug.Log("Defance action");
                    int rnd = (int)Random.Range(1,3);
                    if (rnd == 1)
                        if (actions["DHead"] != true)
                            actions["DHead"] = true;
                        else
                            i++;
                    if (rnd == 2)
                        if (actions["DBody"] != true)
                            actions["DBody"] = true;
                        else
                            i++;
                    if (rnd == 3)
                        if (actions["DFoot"] != true)
                            actions["DFoot"] = true;
                        else
                            i++;
                }
            }
        }
       Debug.Log(actions["AHead"].ToString() + actions["ABody"].ToString() +actions["AFoot"].ToString() +actions["DHead"].ToString()  + actions["DBody"].ToString() +actions["DFoot"].ToString() );
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
            StartCoroutine(Damaged_animation());
            punch_audio.Play();
            if ((health - (dmg*checkCrit() - armor))>=0)
            health -= (dmg*checkCrit() - armor);
            else
            health = 0;
            Debug.Log("Atack power " + (dmg*checkCrit()).ToString() + " Blocked " + armor.ToString() + " Demage " + (dmg*checkCrit() - armor).ToString());
        }
        
        if (ab && actions["DBody"]!= true)
        {
            StartCoroutine(Damaged_animation());
            punch_audio.Play();
            if ((health - (dmg*checkCrit() - armor))>=0)
            health -= (dmg*checkCrit() - armor);
            else
            health = 0;
            Debug.Log("Atack power " + (dmg*checkCrit()).ToString() + " Blocked " + armor.ToString() + " Demage " + (dmg*checkCrit() - armor).ToString());
        }

        if (af && actions["DFoot"]!= true)
        {
            punch_audio.Play();
            StartCoroutine(Damaged_animation());
            if ((health - (dmg*checkCrit() - armor))>=0)
            health -= (dmg*checkCrit() - armor);
            else
            health = 0;
            Debug.Log("Atack power " + (dmg*checkCrit()).ToString() + " Blocked " + armor.ToString() + " Demage " + (dmg*checkCrit() - armor).ToString());
        }
    }
}
