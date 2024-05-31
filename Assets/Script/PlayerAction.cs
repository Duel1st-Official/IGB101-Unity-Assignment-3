using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float idleTime;
    private bool isOffensive;

    void Start()
    {
        animator = GetComponent<Animator>();
        idleTime = 0;
        isOffensive = false;
    }

    void Update()
    {
        // Handle movement inputs
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float speed = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));
        animator.SetFloat("Speed", speed);

        // Check if the Shift key is pressed
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        animator.SetBool("IsRunning", isRunning);

        // Debugging
        Debug.Log("Vertical: " + vertical + ", Horizontal: " + horizontal + ", Speed: " + speed + ", IsRunning: " + isRunning);

        // Increment idle time if the player is idle
        if (speed == 0)
        {
            idleTime += Time.deltaTime;

            // Switch to offensive idle after 5 seconds of idling
            if (idleTime >= 5)
            {
                isOffensive = true;
            }
        }
        else
        {
            idleTime = 0;
            isOffensive = false;
        }

        // Update the IdleType parameter in the Animator
        animator.SetFloat("IdleType", isOffensive ? 1f : 0f);

        // Trigger waving animation when 1 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("Wave");
        }

        // Trigger Rumba Dancing animation when 2 is pressed and the character is idle
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleBlendTree"))
            {
                animator.SetTrigger("Dance");
            }
        }

        // Trigger Praying animation when 3 is pressed and the character is idle
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleBlendTree"))
            {
                animator.SetTrigger("Pray");
            }
        }
    }
}
