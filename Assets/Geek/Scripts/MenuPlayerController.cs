using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerController : MonoBehaviour
{
    [SerializeField]
    private MenuController menuController;

    [SerializeField]
    private CameraController cameraController;

    private Animator playerAnimator;

    Rigidbody2D playerRigidbody2D;

    public bool isGround = false;

    private float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        playerAnimator.SetTrigger("Land");
    }

    // Update is called once per frame
    void Update()
    {
        cameraController.SetPosition(this.transform.position);

        horizontal = menuController.playerVector;

        if (playerRigidbody2D.velocity.y == 0)
        {
            // 地面に接地していると判定
            isGround = true;
        }
        else
        {
            // 地面に接地していないと判定
            isGround = false;
        }

        playerAnimator.SetFloat("Vertical", Mathf.Abs(playerRigidbody2D.velocity.y));
        playerAnimator.SetFloat("Horizontal", horizontal);
        playerAnimator.SetBool("isGround", isGround);
    }

    public void Jump()
    {
        playerAnimator.SetTrigger("Jump");
    }
}
