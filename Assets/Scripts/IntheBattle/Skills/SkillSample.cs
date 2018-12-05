using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SkillSample : Skill {

    /* 
    
    m_skillPlace = n   //   n+1 번째 스킬임
    m_activeORPassive   //  true = active, false = Passive
    m_attackORAssist    // true = 공격(타겟이 적), false = 도움(타겟이 팀)
    m_cooperation   //  true = 협동공격 터짐, false = 안터짐
    m_compbo    //  true = 연속공격 됨, false = 안댐
    m_IsTargeting    //   ture = 타겟이 잇음, false = 타겟이 없음
    m_targetNum = n   //   n 명을 때림
    
    스킬 사용 순서 Activating -> Jump -> attack -> strike or hit -> Jump back 

    Activating 안에서는
    base.Activating(target, inputStyle_AorP); 으로 시작해야함.

    그리고 그 다음줄에 무조건 이걸 쓰고 안에서 시작하기 바람. 
    if (m_activeORPassive == inputStyle_AorP) -> 패시브 스킬이 액티브 스킬 사용 입력에 사용되지 않도록 하는 장치
    {
       // 코드 입력
    }
            
    if (m_attackORAssist && (m_userCharacter.m_side != m_target.m_side)) -> 액티브 스킬이 공격/방어 대상 지정이 올바른지 체크
    {      

    }
     
     */


}
