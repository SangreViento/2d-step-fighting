using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class cp_data : MonoBehaviour

{
    public Fighter controlled_fighter;
    public int avail_ap;

    public Dictionary<string, bool> actions =  new Dictionary<string, bool>();

    public Button head_attack_btn;
    public Button body_attack_btn;
    public Button foot_attack_btn;

    public Button head_block_btn;
    public Button body_block_btn;
    public Button foot_block_btn;

    public Button fight_btn;
    public Text ap_label;

    private void Update()
    {
        ap_label.text = "AP " + avail_ap.ToString();

        if (actions["AHead"] == true)
            head_attack_btn.image.color = Color.red;
        else
            head_attack_btn.image.color = Color.white;

        if (actions["ABody"] == true)
            body_attack_btn.image.color = Color.red;
        else
            body_attack_btn.image.color = Color.white;

        if (actions["AFoot"] == true)
            foot_attack_btn.image.color = Color.red;
        else
            foot_attack_btn.image.color = Color.white;

        if (actions["DHead"] == true)
            head_block_btn.image.color = Color.green;
        else
            head_block_btn.image.color = Color.white;

        if (actions["DBody"] == true)
            body_block_btn.image.color = Color.green;
        else
            body_block_btn.image.color = Color.white;

        if (actions["DFoot"] == true)
            foot_block_btn.image.color = Color.green;
        else
            foot_block_btn.image.color = Color.white;
    }
    void Start()
    {
        avail_ap = controlled_fighter.ap;

        actions.Add("AHead", false);
        actions.Add("ABody", false);
        actions.Add("AFoot", false);
        actions.Add("DHead", false);
        actions.Add("DBody", false);
        actions.Add("DFoot", false);

        head_block_btn.onClick.AddListener(Set_DHead);
        body_block_btn.onClick.AddListener(Set_DBody);
        foot_block_btn.onClick.AddListener(Set_DFoot);

        head_attack_btn.onClick.AddListener(Set_AHead);
        body_attack_btn.onClick.AddListener(Set_ABody);
        foot_attack_btn.onClick.AddListener(Set_AFoot);

        fight_btn.onClick.AddListener(clearActions);
    }
    void Set_AHead()
    {
        if (avail_ap > 0)
        {
            if (actions["AHead"] == false)
            {
                actions["AHead"] = true;
                avail_ap--;
            }
            else
            {
                actions["AHead"] = false;
                avail_ap++;
            }
        }
        else if (actions["AHead"] == true)
        {
            actions["AHead"] = false;
            avail_ap++;
        }
    }
    void Set_ABody()
    {
        if (avail_ap > 0)
        {
            if (actions["ABody"] == false)
            {
                actions["ABody"] = true;
                avail_ap--;
            }
            else
            {
                actions["ABody"] = false;
                avail_ap++;
            }
        }
        else if (actions["ABody"] == true)
        {
            actions["ABody"] = false;
            avail_ap++;
        }
    }
    void Set_AFoot()
    {
        if (avail_ap > 0)
        {
            if (actions["AFoot"] == false)
            {
                actions["AFoot"] = true;
                avail_ap--;
            }
            else
            {
                actions["AFoot"] = false;
                avail_ap++;
            }
        }
        else if (actions["AFoot"] == true)
        {
            actions["AFoot"] = false;
            avail_ap++;
        }
    }
    void Set_DHead()
    {
        if (avail_ap > 0)
        {
            if (actions["DHead"] == false)
            {
                actions["DHead"] = true;
                avail_ap--;
            }
            else
            {
                actions["DHead"] = false;
                avail_ap++;
            }
        }
        else if (actions["DHead"] == true)
        {
            actions["DHead"] = false;
            avail_ap++;
        }
    }
    void Set_DBody()
    {
        if (avail_ap > 0)
        {
            if (actions["DBody"] == false)
            {
                actions["DBody"] = true;
                avail_ap--;
            }
            else
            {
                actions["DBody"] = false;
                avail_ap++;
            }
        }
        else if (actions["DBody"] == true)
        {
            actions["DBody"] = false;
            avail_ap++;
        }
    }
    void Set_DFoot()
    {
        if (avail_ap > 0)
        {
            if (actions["DFoot"] == false)
            {
                actions["DFoot"] = true;
                avail_ap--;
            }
            else
            {
                actions["DFoot"] = false;
                avail_ap++;
            }
        }
        else if (actions["DFoot"] == true)
        {
            actions["DFoot"] = false;
            avail_ap++;
        }
    }

    public void TransmiteFlags()
    {
        controlled_fighter.reciveFlags(actions);
    }

    public void clearActions()
    {
        avail_ap = avail_ap + controlled_fighter.ap;

        actions["AHead"] = false;
        actions["ABody"] = false;
        actions["AFoot"] = false;
        actions["DHead"] = false;
        actions["DBody"] = false;
        actions["DFoot"] = false;
    }
}

