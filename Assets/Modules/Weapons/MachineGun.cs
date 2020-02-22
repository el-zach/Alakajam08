using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public bool active = false;
    public Animator animator;
    public float cooldown = 0.1f;
    public float putAwayTime = 3f;
    public float animatorSpeed = 1f;
    float timer;

    private void Update()
    {
        if (active)
        {
            timer += Time.deltaTime;
            animator.speed = animatorSpeed;
        }
    }

    public void PullTrigger()
    {
        if(timer > cooldown)
        {
            animator.SetBool("Visible", true);
            animator.SetTrigger("Shot");
            timer = 0f;
        }
        if(timer > putAwayTime)
        {
            animator.SetBool("Visible", false);
        }
    }

    public void Activate()
    {
        active = true;
        transform.GetChild(0).gameObject.SetActive(true);
        animator.SetBool("Visible", true);
    }

    public void Deactivate()
    {
        active = false;
        transform.GetChild(0).gameObject.SetActive(false);
        timer = 0f;
    }
}
