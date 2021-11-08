using UnityEngine;
using UnityEngine.UI;

public class cp_data : MonoBehaviour

{
    enum Target_Zone // Перечисление мест атаки и защиты закодированных в числовом формате 
    {
        Unselected = 0,
        Head = 1,
        Body = 2,
        Foot = 3
    }

    public Fighter controlled_fighter;

    public Button head_block_btn;
    public Button body_block_btn;
    public Button foot_block_btn;

    public Button head_attack_btn;
    public Button body_attack_btn;
    public Button foot_attack_btn;

    public Button fight_btn;

    void Start()
    {
        head_block_btn.onClick.AddListener(SetDeffanceZone_head);
        body_block_btn.onClick.AddListener(SetDeffanceZone_body);
        foot_block_btn.onClick.AddListener(SetDeffanceZone_foot);

        head_attack_btn.onClick.AddListener(SetAttacZone_head);
        body_attack_btn.onClick.AddListener(SetAttacZone_body);
        foot_attack_btn.onClick.AddListener(SetAttacZone_foot);

        fight_btn.onClick.AddListener(Reset_btn);
    }
    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            SetDeffanceZone_head();
        }
        if (Input.GetKeyDown("a"))
        {
            SetDeffanceZone_body();
        }
        if (Input.GetKeyDown("z"))
        {
            SetDeffanceZone_foot();
        }
        if (Input.GetKeyDown("w"))
        {
            SetAttacZone_head();
        }
        if (Input.GetKeyDown("s"))
        {
            SetAttacZone_body();
        }
        if (Input.GetKeyDown("x"))
        {
            SetAttacZone_foot();
        }
    }
    void SetAttacZone_head()
    {
        controlled_fighter.SetAttZone((int)Target_Zone.Head);
        body_attack_btn.enabled = false;
        foot_attack_btn.enabled = false;
    }
    void SetAttacZone_body()
    {
        controlled_fighter.SetAttZone((int)Target_Zone.Body);
        head_attack_btn.enabled = false;
        foot_attack_btn.enabled = false;
    }
    void SetAttacZone_foot()
    {
        controlled_fighter.SetAttZone((int)Target_Zone.Foot);
        body_attack_btn.enabled = false;
        head_attack_btn.enabled = false;
    }
    void SetDeffanceZone_head()
    {
        controlled_fighter.SetDefZone((int)Target_Zone.Head);
        body_block_btn.enabled = false;
        foot_block_btn.enabled = false;
    }
    void SetDeffanceZone_body()
    {
        controlled_fighter.SetDefZone((int)Target_Zone.Body);
        head_block_btn.enabled = false;
        foot_block_btn.enabled = false;
    }
    void SetDeffanceZone_foot()
    {
        controlled_fighter.SetDefZone((int)Target_Zone.Foot);
        body_block_btn.enabled = false;
        head_block_btn.enabled = false;
    }
    public void Reset_btn()
    {
        head_block_btn.enabled = true;
        body_block_btn.enabled = true;
        foot_block_btn.enabled = true;

        head_attack_btn.enabled = true;
        body_attack_btn.enabled = true;
        foot_attack_btn.enabled = true;
    }
}
