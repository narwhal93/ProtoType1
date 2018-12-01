using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Skill {

    public override void Activating(Character target)
    {
        base.Activating(target);
        m_animator.SetBool("1",true);
    }
}
