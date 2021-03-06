using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fight_Director : MonoBehaviour
{
    public GameObject[] objects;

    int round = 1;
    public Fighter left_figther; 
    public Fighter right_figther; 

    public Text lf_health_bar; 
    public Text rh_health_bar; 

    public Button fight_btn; 
    public Button restart_btn;

    public lifebar_control lifebar_left;
    public lifebar_control lifebar_right;

    public cp_data left_cp;

    public Text anonce;

    public GameObject battle_hud;
    public GameObject restart_hud;

    int left_hero_index;
    int right_hero_index;

    private void Awake()
    {
        left_hero_index = hero.left_hero;
        right_hero_index = hero.right_hero;
    }
    void Start()
    {
        battle_hud.SetActive(true);
        restart_hud.SetActive(false);   
        GameObject lf = Instantiate(objects[left_hero_index]); 
        GameObject rf = Instantiate(objects[right_hero_index]); 

        lf.transform.position = new Vector3(-6.5f, -0.5f, 0f); 
        rf.transform.position = new Vector3(6.5f, -0.5f, 0f); 
        rf.transform.rotation = Quaternion.Euler(0f,180f,0f); 

        left_figther = lf.GetComponent<Fighter>(); 
        right_figther = rf.GetComponent<Fighter>(); 

        left_figther.enemy = right_figther; 
        right_figther.enemy = left_figther;

        left_cp.controlled_fighter = left_figther; 

        lifebar_left.watched_figther = left_figther;
        lifebar_right.watched_figther = right_figther;

        fight_btn.onClick.AddListener(Fight);
        restart_btn.onClick.AddListener(Restart);
    }


    void Update()
    {
        lf_health_bar.text = "HP " + left_figther.health.ToString(); 
        rh_health_bar.text = "HP " + right_figther.health.ToString(); 
       
        if (left_figther.health == 0 && right_figther.health == 0)
        {
            anonce.text = "Draw!";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
        }
        else if (left_figther.health == 0)
        {
            anonce.text = "You loose";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
        }
        else if (right_figther.health == 0)
        {
            anonce.text = "WIN!";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
        }
        else if (left_figther.health > 0 && right_figther.health > 0)
        {
            anonce.text = "Round " + round;
            anonce.enabled = true;
        }

    }
    void Fight()

    {
        left_cp.TransmiteFlags();
        right_figther.Ai_Prepare_attack();
        left_figther.Attack();
        right_figther.Attack(); 
        right_figther.clearActions();
        left_figther.clearActions();
        round++; 
    }

    void Restart()
    {
        left_figther.clearActions();
        right_figther.clearActions();
        left_figther.Refresh_stats();
        right_figther.Refresh_stats();
        left_cp.resetAP();
        battle_hud.SetActive(true);
        restart_hud.SetActive(false);
        round = 1;
    }

}
