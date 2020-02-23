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
    public float timer;
    public Transform muzzle;
    public Vector2 damage = Vector2.one;

    private void Update()
    {
        if (active)
        {
            RotateToMousePosition();
            timer += Time.deltaTime;
            animator.speed = animatorSpeed;
            if (timer > putAwayTime)
            {
                animator.SetBool("Visible", false);
            }
        }
    }

    Camera main;
    void RotateToMousePosition()
    {
        if (!main) main = Camera.main;
        Ray ray = new Ray(main.transform.position, main.transform.forward); //main.ScreenPointToRay(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(ray.GetPoint(100f) - transform.position);
    }

    public void PullTrigger()
    {
        if (timer > cooldown)
        {
            animator.SetBool("Visible", true);
            animator.SetTrigger("Shot");
            timer = 0f;
        }
    }

    public void CalculateShot()
    {
        RaycastHit hit;
        if(Physics.Raycast(muzzle.transform.position, muzzle.transform.forward,out hit)){
            Health health = hit.collider.GetComponent<Health>();
            if (health)
            {
                int dmg = health.Damage(Random.Range((int)damage.x, (int)damage.y));
                UserInterface.RenderDamageNumbers(dmg, hit.point);
                DamageVFX.DamageAt(hit.point, hit.normal);
            }
        }
    }
    public AudioSource gunSound;
    public Vector2 gunPitch = new Vector2(0.8f, 1.2f);
    public void PlayShotSound()
    {
        gunSound.pitch = Random.Range(gunPitch.x, gunPitch.y);
        gunSound.Play();
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
