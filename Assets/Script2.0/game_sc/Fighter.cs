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
}
