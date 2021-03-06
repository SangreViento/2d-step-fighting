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
    Animator anim;

    public IEnumerator Animations(bool attack_anim, bool dmg_anim)
    {
        if (attack_anim)
        {
            anim.SetBool("atack_perform", true);
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("atack_perform", false);
        }
        
        yield return new WaitForSeconds(0.50f);

        if (dmg_anim)
        {
            anim.SetBool("dmg_taken", true);
            appearance.color = Color.red;
            yield return new WaitForSeconds(0.50f);
            appearance.color = Color.white;
            anim.SetBool("dmg_taken", false);
        }
        
    }
   

    void Start()
    {
        max_hp = health;
        max_ap = ap;
        appearance = GetComponent<SpriteRenderer>();
        punch_audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

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
        if ((int)Random.Range(1,100) <= crit)
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
        bool dmg_flag = false;
        if (ah && actions["DHead"] != true)
            ReciveDmg(dmg);
        if (ab && actions["DBody"] != true)
            ReciveDmg(dmg);
        if (af && actions["DFoot"] != true)
            ReciveDmg(dmg);

        if (!dmg_flag && actions["AHead"] || actions["ABody"] || actions["AFoot"])
        {
            StartCoroutine(Animations(true, false));
        }
    }

    void ReciveDmg(int dmg)
    {
        int calc_dmg = dmg*checkCrit()-armor;
        if (calc_dmg>0)
        {
            // insert animation mode selector
            if (actions["AHead"] || actions["ABody"] || actions["AFoot"])
            {
                StartCoroutine(Animations(true, true));
            }
            else
            {
                StartCoroutine(Animations(false, true));
            }
            punch_audio.Play();
            if ((health-calc_dmg) >= 0)
                health -= calc_dmg;
            else
                health = 0;
        }
        else
        {
            // insert damage block animation
        }
    }
}
