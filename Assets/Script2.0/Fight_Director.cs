using UnityEngine;
using UnityEngine.UI;

public class Fight_Director : MonoBehaviour
{
    public GameObject[] objects;
    enum Target_Zone // Перечисление мест атаки и защиты закодированных в числовом формате 
    {
        Unselected = 0,
        Head = 1,
        Body = 2,
        Foot = 3
    }

    int round = 1;

    // мд - модификатор доступа, тип - тип переменной, имя переменной (ссылка на объект) 

    // мд  // тип  // имя переменной
    public Fighter left_figther; // Создаем переменную типа Figther для левого игрока 
    public Fighter right_fighter; // Создаем переменную типа Figther для правого игрока 

    public Text lf_health_bar; // Переменная типа Text для отображения здоровья левого бойца
    public Text rh_health_bar; // Переменная типа Text для отображения здоровья правого бойца

    public Button fight_btn; // Переменная типа Button для доступа к кнопке атака
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
        GameObject lf = Instantiate(objects[0]); // Динамически создаем объект левого бойца
        GameObject rf = Instantiate(objects[2]); // Динамически создаем объект правого бойца

        lf.transform.position = new Vector3(-6.5f, -0.5f, 0f); // устанавливаем координаты левого бойца
        rf.transform.position = new Vector3(6.5f, -0.5f, 0f); // устанавливаем координаты правого бойца
        rf.transform.rotation = Quaternion.Euler(0f,180f,0f); // разворачиваем правого бойца на 180 градусов

        left_figther = lf.GetComponent<Fighter>(); // достаем компонент Fighter из игрового объекта для взаимодействия со скриптом левого бойца
        right_fighter = rf.GetComponent<Fighter>(); // достаем компонент Fighter из игрового объекта для взаимодействия со скриптом правого бойца

        left_figther.enemy = right_fighter; // Назначаем бойцов врагами друг другу
        right_fighter.enemy = left_figther;

        left_cp.controlled_fighter = left_figther; // Передаем панелям управления героями ссылки на скрипты управления
        right_cp.controlled_fighter = right_fighter;

        fight_btn.onClick.AddListener(Fight); // Метод Fight данного класса будет вызван при нажатии на кнопку fight_btn
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

        lf_health_bar.text = "ОЗ " + left_figther.health.ToString(); // Отправляем на экран информацию о здоровье левого бойца
        rh_health_bar.text = "ОЗ " + right_fighter.health.ToString(); // Отправляем на экран информацию о здоровье правого бойца
       
        if (left_figther.health == 0 && right_fighter.health == 0)
        {
            
            anonce.text = "Ничья!";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
            combat_flag = false;

        }
        else if (left_figther.health == 0)
        {
            //anonce.text = "Победил " + right_fighter.name;
            
            anonce.text = "Поражение :-(";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
            combat_flag = false;
        }
        else if (right_fighter.health == 0)
        {
            //anonce.text = "Победил " + left_figther.name;
            anonce.text = "Победа!";
            anonce.enabled = true;
            battle_hud.SetActive(false);
            restart_hud.SetActive(true);
            combat_flag = false;

        }
        else if (left_figther.health > 0 && right_fighter.health > 0)
        {
            anonce.text = "Раунд " + round;
            combat_flag = true;
        }

    }
    void Fight()
    {
        right_fighter.AI_SetAttZone();
        right_fighter.AI_SetDefZone();

        left_figther.Make_Attack(); // Метод данного класса вызывает метод у бойца класса Fighter
        right_fighter.Make_Attack(); // Метод данного класса вызывает метод у бойца класса Fighter

        left_figther.ResetDefAttZone(); // Метод данного класса вызывает метод у бойца класса Fighter
        right_fighter.ResetDefAttZone(); // Метод данного класса вызывает метод у бойца класса Fighter

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
