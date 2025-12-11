using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    //int Idle = Animator.StringToHash("Idle");
    //int Walk = Animator.StringToHash("Walk");

    private Vector3 previousPos;
    [SerializeField]
    private bool isWalk;
    void Start()
    {
        anim = GetComponent<Animator>();
        previousPos = this.transform.position;
        isWalk = false;
        SetPlayerAnimation(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 currentPos;
    private float distance;
    private void FixedUpdate()
    {
        if(anim == null) return;
        
        currentPos = this.transform.position;
        distance = Mathf.Abs(Vector3.Distance(currentPos, previousPos));
        
        if(!isWalk && distance > 0.001f)
        {
            SetPlayerAnimation(1);
            isWalk = true;
        }
        else if(isWalk && distance == 0f)
        {
            SetPlayerAnimation(0);
            isWalk = false;
        }
        previousPos = currentPos;

    }
    public void SetPlayerAnimation(int id)
    {
        switch (id)
        {
            case 0:
                //anim.SetTrigger(Idle);
                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                break;
            case 1:
                //anim.SetTrigger(Walk);
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);
                break;
            
            default:
                //anim.SetTrigger(Idle);
                break;
        }
    }
}
