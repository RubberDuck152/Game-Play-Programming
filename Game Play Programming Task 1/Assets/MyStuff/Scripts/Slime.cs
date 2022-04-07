using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public bool spotted = false;
    public GameObject player;
    public float rotationSpeed;
    public float movementSpeed;
    public bool move = false;
    public float stoppingDistance;
    public int maxHP;
    public Slime slime1;
    public Slime slime2;
    public int counter;
    public int count;
    public Slime a_SlimePrefab;
    public bool attacked = false;
    int timer = 0;
    // Start is called before the first frame update

    private void Update()
    {
        if (spotted == true)
        {
            float step = movementSpeed * Time.deltaTime;
            if (move)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            }
            if (Vector3.Distance(transform.position, player.transform.position) <= stoppingDistance)
            {
                move = false;
            }
            else
            {
                move = true;
            }
        }

        if (maxHP <= 0)
        {
            if (counter != count)
            {
                slime1 = Instantiate(a_SlimePrefab);
                var newPos = transform.position + transform.right * 2;
                slime1.Init(newPos);
                slime1.player = player;
                slime1.count++;

                slime2 = Instantiate(a_SlimePrefab);
                newPos = transform.position - transform.right * 2;
                slime2.Init(newPos);
                slime2.player = player;
                slime2.count++;
            }
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= (stoppingDistance + 2))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>().anim.GetCurrentAnimatorStateInfo(0).IsName("Armed-Attack-1"))
            {
                if (timer == 0)
                {
                    maxHP = maxHP - 4;
                    timer = 1;
                    StartCoroutine(TimeDelay(1));
                }
            }
        }
    }

    public void Init(Vector3 position)
    {
        enabled = true;
        maxHP = 16;
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.position = position;
        transform.localScale = gameObject.transform.localScale / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (spotted == true)
        {
            Vector3 targetDirection = player.transform.position - transform.position;
            targetDirection.y = Vector3.zero.y;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            spotted = true;
            move = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            spotted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        spotted = false;
        move = false;
    }

    IEnumerator TimeDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timer = 0;
    }
}
