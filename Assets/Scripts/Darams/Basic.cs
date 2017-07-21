﻿using UnityEngine;
using System.Collections;

public class Basic : Daram {

    private int past_move_stat = 0; // 현재 움직임 상태
    private int now_move_stat; //이전 움직임 상태

    

    void Start()
    {
        Anim = GetComponent<Animator>();
        base.Start();
    }

    void FixedUpdate()
    {
        now_move_stat = base.move_stat;
        if (now_move_stat != past_move_stat)
        {
            switch (now_move_stat)
            {
                case 0:
                    Anim.SetTrigger("Up");
                    break;
                case 1:
                    Anim.SetTrigger("Down");
                    break;
                case 2:
                    Anim.SetTrigger("Left");
                    break;
                case 3:
                    Anim.SetTrigger("Right");
                    break;
            }
            past_move_stat = now_move_stat;
        }
    }
}
