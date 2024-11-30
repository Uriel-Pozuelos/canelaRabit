using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private string walkParameter = "WalkParam";
    private string attackParameter = "AttackParam";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void WalkAnim()
    {
        anim.Play("Walk");
    }

    public void PLayerWalk(bool walk)
    {
        anim.SetBool(walkParameter, walk);
    }

    public void PlayerAttack()
    {
        anim.SetBool(attackParameter, true);
    }

    public void PlayerAttackEnd()
    {
        anim.SetBool(attackParameter, false);
    }

	public void SetAttackParam(bool isAttacking)
	{
		Animator animator = GetComponent<Animator>();
		animator.SetBool("AttackParam", isAttacking);
	}
}
