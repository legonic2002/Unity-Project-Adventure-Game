using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuyAnhPlayer : MonoBehaviour
{
    [SerializeField] private GameObject obKunai;
    [SerializeField] private Transform tfKunaiSpawn;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed=250;
    private bool isGrounded=true;
    private bool isJumping = false;
    private bool isAttack = false;
    private float horizontal;
    private string currentAnimName;
    [SerializeField] private float jumpForce = 350;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = CheckGrounded();
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isAttack)
        {
           /* rb.velocity = Vector2.zero;*/
            return;
        }
        if(isGrounded )
        {
            if (isJumping)
            {
                return;
            }
            //jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            
            
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
            if(Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
            }
            if(Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }
            

        }
        //checkFall
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        //run
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            
            rb.velocity = new Vector2(horizontal *Time.fixedDeltaTime * speed,rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0)) ;
        }
        //idle
        else if(isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }


    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down *1.1f,Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }

    private void Attack()
    {
        ChangeAnim("attack");
        isAttack=true;
        Invoke(nameof(ResetAttack), 0.5f);
      GameObject ob =   Instantiate(obKunai, tfKunaiSpawn.position , Quaternion.identity);
        ob.GetComponent<Weapon>().isThrow = true;
    }
    private void Throw()
    {
        isAttack = true;
        ChangeAnim("throw");
        Invoke(nameof(ResetAttack), 0.5f);
    }
    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("ilde");
    }
    private void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    private void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
