using UnityEngine;
using System.Collections;

public class BasicController : MonoBehaviour {
	private Animator  animator;
	public  float     h;
	public  float     v;
	public  bool      bg;
	public  int       rs;	
	public  float     rotSpeed = 90;
	public  float     j;	
	public  bool      canJump;	
	public  bool      grounded;
	public  Rigidbody rb;	
	#region Novo
	private int      winded;
	#endregion


	public void Start() {
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		bg = false;
		rs = 0;
		canJump  = true;
		grounded = true;		
		#region Novo
		winded   = animator.GetInteger("Winded"); 
		#endregion		
	}
	
	public void Update() {
		#region Novo
		if (winded == 2) 
			Input.ResetInputAxes();	
		#endregion

		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");	
		j = Input.GetAxis("Jump");
        rs = Input.GetAxis("Fire3") > 0.1f ? 3 : 0;
		if (Input.GetKeyDown (KeyCode.H)) {
			bg = !bg;
		}

        if (Input.GetKey(KeyCode.E))
        {
            animator.SetLayerWeight(1, 1.0F);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetLayerWeight(1, 0.0F);
        }

        #region Novo
        if (Input.GetKeyDown(KeyCode.J))
			ProcessWinded();		
		#endregion
	}

    public void FixedUpdate() {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        animator.SetFloat("ForwardInput", v);
        animator.SetInteger("RaceState", rs);
        animator.SetBool("BogWalk", bg);
        if (winded == 0) {
            transform.Rotate(0, h * Time.deltaTime * rotSpeed, 0);
            animator.SetFloat("Turning", h);
        }
		
		if (stateInfo.IsName("Base Layer.Idle")) {  
	    	if (canJump && grounded && j == 1) 
	    		rb.AddForce (Vector3.up * 200);
     		ProcessJump();	
		}		
	}

	public void OnCollisionEnter(Collision hit) {
		grounded = true;
	}

	public void ProcessJump() {
	   if (j == 1 && canJump) {
	   		animator.SetBool("Jump", true);
	   		StartCoroutine(JumpRoutine());
	   }
	   else if (j == 0) {
	   		canJump = true; 
	   }	
	}

	public IEnumerator JumpRoutine() {
		yield return 0;
   		animator.SetBool("Jump", false);
   		canJump = false; 
	}

	#region Novo
	public void ProcessWinded() {
		if (winded != 0) return; 
		
		winded = 1; 
		animator.SetInteger("Winded", 1);

		StartCoroutine(WindedRoutine());
	}

	public IEnumerator WindedRoutine() {
		yield return null;

		animator.SetInteger("Winded", 2);

		yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5.5F));
		
		animator.SetInteger("Winded", 0);
		winded = 0; 
	}	
	#endregion
}