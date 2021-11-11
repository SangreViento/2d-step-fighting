using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Fighter : MonoBehaviour
{
    public int health = 0; // В переменной health типа int сохраняем значение 100 равное изначальному количеству HP 
    public int attack_power = 0; // В переменной strike_power типа int сохраняем значение 5 равное силе удара
    public int armor = 0; // Броня персонажа
    public int crit = 0; // Шанс критического урона в процентах крит Х2
    public int ap = 2; // Очки действий
    
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
}
