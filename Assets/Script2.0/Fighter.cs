using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fighter : MonoBehaviour
{
    public int health = 0; // ¬ переменной health типа int сохран€ем значение 100 равное изначальному количеству HP 
    public int attack_power = 0; // ¬ переменной strike_power типа int сохран€ем значение 5 равное силе удара
    public int armor = 0; // Ѕрон€ персонажа
    public int crit = 0; // Ўанс критического урона в процентах крит ’2
    
    public Fighter enemy;

    enum Target_Zone // ѕеречисление мест атаки и защиты закодированных в числовом формате 
    {
        Unselected = 0,
        Head = 1,
        Body = 2,
        Foot = 3
    }


    public int Def_Zone = (int)Target_Zone.Unselected;  // ”станавливает значение защищаемой области по умолчанию не выбранным
    public int Att_Zone = (int)Target_Zone.Unselected; // ”станавливает значение атакуемой области по умолчанию не выбранным

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

    // Start is called before the first frame update
    void Start()
    {
        appearance = GetComponent<SpriteRenderer>();
        punch_audio = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDefZone(int dz)
    {
        Def_Zone = dz;
    }

    public void SetAttZone(int az)
    {
        Att_Zone = az;
    }

    public void AI_SetDefZone()
    {
        int rand = (int)Random.Range(1f, 3f);
        Def_Zone = rand;
    }

    public void AI_SetAttZone()
    {
        int rand = (int)Random.Range(1f, 3f);
        Att_Zone = rand;
    }

    public void ResetDefAttZone()
    {
        Def_Zone = (int)Target_Zone.Unselected;
        Att_Zone = (int)Target_Zone.Unselected;
    }
    public void Make_Attack()
    {
        int calculated_attack;

        if ((int)Random.Range(1,100) <= crit) 
        {
            calculated_attack = attack_power*2;
            //Debug.Log("Crit damage " + calculated_attack.ToString() + " to " + enemy.name.ToString());
        }
        else
        {
            calculated_attack = attack_power;
            //Debug.Log("Damage " + calculated_attack.ToString() + " to " + enemy.name.ToString());
        }
            
        enemy.Receive_Attack(Att_Zone, calculated_attack);
    }
    public void Receive_Attack(int at, int ap)
    {
        if (at == (int)Target_Zone.Head && Def_Zone != (int)Target_Zone.Head)
        {
            if ((health - (ap-armor)) > 0)
            {
                health -= (ap - armor);
                StartCoroutine(Damaged_animation());
                punch_audio.Play();
            }
            else
            {
                health = 0;
            }
        }
        if (at == (int)Target_Zone.Body && Def_Zone != (int)Target_Zone.Body)
        {
            if ((health - (ap - armor)) > 0)
            {
                health -= (ap - armor);
                StartCoroutine(Damaged_animation());
                punch_audio.Play();
            }
            else
            {
                health = 0;
            }
        }
        
        if (at == (int)Target_Zone.Foot && Def_Zone != (int)Target_Zone.Foot)
        {
            if ((health - (ap - armor)) > 0)
            {
                health -= (ap - armor);
                StartCoroutine(Damaged_animation());
                punch_audio.Play();
            }
            else
            {
                health = 0;
                
            }
        }

    }
}
