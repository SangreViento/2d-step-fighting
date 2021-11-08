using UnityEngine;
using UnityEngine.UI;

public class Fight_Director : MonoBehaviour
{
    public GameObject[] objects;
    enum Target_Zone // ������������ ���� ����� � ������ �������������� � �������� ������� 
    {
        Unselected = 0,
        Head = 1,
        Body = 2,
        Foot = 3
    }

    int round = 1;

    // �� - ����������� �������, ��� - ��� ����������, ��� ���������� (������ �� ������) 

    // ��  // ���  // ��� ����������
    public Fighter left_figther; // ������� ���������� ���� Figther ��� ������ ������ 
    public Fighter right_fighter; // ������� ���������� ���� Figther ��� ������� ������ 

    public Text lf_health_bar; // ���������� ���� Text ��� ����������� �������� ������ �����
    public Text rh_health_bar; // ���������� ���� Text ��� ����������� �������� ������� �����

    public Button fight_btn; // ���������� ���� Button ��� ������� � ������ �����
    public Button restart_btn;

    public lifebar_control lifebar_left;
    public lifebar_control lifebar_right;
    

    public cp_data left_cp;
    public cp_data right_cp;

    public Text anonce;

    public GameObject battle_hud;
    public GameObject restart_hud;

    public bool combat_flag;

    void Start()
    {
        combat_flag = true;
        battle_hud.SetActive(true);
        restart_hud.SetActive(false);   
        GameObject lf = Instantiate(objects[0]); // ����������� ������� ������ ������ �����
        GameObject rf = Instantiate(objects[2]); // ����������� ������� ������ ������� �����

        lf.transform.position = new Vector3(-6.5f, -0.5f, 0f); // ������������� ���������� ������ �����
        rf.transform.position = new Vector3(6.5f, -0.5f, 0f); // ������������� ���������� ������� �����
        rf.transform.rotation = Quaternion.Euler(0f,180f,0f); // ������������� ������� ����� �� 180 ��������

        left_figther = lf.GetComponent<Fighter>(); // ������� ��������� Fighter �� �������� ������� ��� �������������� �� �������� ������ �����
        right_fighter = rf.GetComponent<Fighter>(); // ������� ��������� Fighter �� �������� ������� ��� �������������� �� �������� ������� �����

        left_figther.enemy = right_fighter; // ��������� ������ ������� ���� �����
        right_fighter.enemy = left_figther;

        left_cp.controlled_fighter = left_figther; // �������� ������� ���������� ������� ������ �� ������� ����������
        right_cp.controlled_fighter = right_fighter;

        fight_btn.onClick.AddListener(Fight); // ����� Fight ������� ������ ����� ������ ��� ������� �� ������ fight_btn
        restart_btn.onClick.AddListener(Restart_Fight);

        lifebar_left.watched_figther = left_figther;
        lifebar_right.watched_figther = right_fighter;
    }


    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (combat_flag)
            {
                Fight();
            }
            
        }

        lf_health_bar.text = "�� " + left_figther.health.ToString(); // ���������� �� ����� ���������� � �������� ������ �����
        rh_health_bar.text = "�� " + right_fighter.health.ToString(); // ���������� �� ����� ���������� � �������� ������� �����
       
        if (left_figther.health == 0 && right_fighter.health == 0)
        {
            
            anonce.text = "�����!";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
            combat_flag = false;

        }
        else if (left_figther.health == 0)
        {
            //anonce.text = "������� " + right_fighter.name;
            
            anonce.text = "��������� :-(";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
            combat_flag = false;
        }
        else if (right_fighter.health == 0)
        {
            //anonce.text = "������� " + left_figther.name;
            anonce.text = "������!";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
            combat_flag = false;

        }
        else if (left_figther.health > 0 && right_fighter.health > 0)
        {
            anonce.text = "����� " + round;
            combat_flag = true;
        }

    }
    void Fight()
    {
        right_fighter.AI_SetAttZone();
        right_fighter.AI_SetDefZone();

        left_figther.Make_Attack(); // ����� ������� ������ �������� ����� � ����� ������ Fighter
        right_fighter.Make_Attack(); // ����� ������� ������ �������� ����� � ����� ������ Fighter

        left_figther.ResetDefAttZone(); // ����� ������� ������ �������� ����� � ����� ������ Fighter
        right_fighter.ResetDefAttZone(); // ����� ������� ������ �������� ����� � ����� ������ Fighter

        left_cp.Reset_btn();
        right_cp.Reset_btn();

        round++;
    }
    void Restart_Fight()
    {
        round = 1;
        combat_flag = true;
        left_figther.health = 100;
        right_fighter.health = 100;
        left_figther.ResetDefAttZone();
        right_fighter.ResetDefAttZone();
        left_cp.Reset_btn();
        right_cp.Reset_btn();
        battle_hud.SetActive(true);
        restart_hud.SetActive(false);
    }
}
