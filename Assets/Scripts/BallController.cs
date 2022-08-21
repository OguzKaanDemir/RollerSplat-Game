using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    [SerializeField] Material NewMaterial;
    Vector2 firstPos, secondPos;
    public Vector2 currentPos;
    public float speed;
    Rigidbody rb;
    public bool canSwipe;
    public Image LevelBar;
    public int currentGrounds, a;
    GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canSwipe)
        {
            canSwipe = false;
            StartCoroutine(Swipe());
        }
        UpdateLevelBar();
    }

    public IEnumerator Swipe()
    {

        if (!gm.isWon)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
            if (Input.GetMouseButtonUp(0))
            {
                secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                currentPos = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);
            }

            currentPos.Normalize();

            if (currentPos.x != 0 && currentPos.y != 0)
            {
                if (currentPos.y > 0 && currentPos.x > -.5f && currentPos.x < .5f)
                {
                    rb.velocity = Vector3.forward * speed;
                }
                else if (currentPos.y < 0 && currentPos.x > -.5f && currentPos.x < .5f)
                {
                    rb.velocity = Vector3.back * speed;
                }
                else if (currentPos.x > 0 && currentPos.y > -.5f && currentPos.y < .5f)
                {
                    rb.velocity = Vector3.right * speed;
                }
                else if (currentPos.x < 0 && currentPos.y > -.5f && currentPos.y < .5f)
                {
                    rb.velocity = Vector3.left * speed;
                }
                yield return new WaitForSeconds(.2f);
            }
            currentPos = Vector2.zero;
            canSwipe = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            collision.gameObject.GetComponent<MeshRenderer>().material = NewMaterial;
            collision.gameObject.tag = "Untagged";
            currentGrounds++;
        }
    }

    void UpdateLevelBar()
    {
        if (gm.isWon == false && gm._groundNumbers != 0)
        {
            LevelBar.DOFillAmount(currentGrounds / gm._groundNumbers, .1f);
        }
        if (LevelBar.fillAmount == 1 && a == 0)
        {
            gm.isWon = true;
            gm.CheckWin();
            currentGrounds = 0;
            a++;
        }
    }
}
