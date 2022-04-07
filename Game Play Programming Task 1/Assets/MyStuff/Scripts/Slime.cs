using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Slime : MonoBehaviour
{
    
    public GameObject player;
    public Slime slime1;
    public Slime slime2;
    public Slime a_SlimePrefab;
    public float rotationSpeed;
    public float movementSpeed;
    public float stoppingDistance;
    public int counter;
    public int count;
    public int maxHP;

    public bool move = false;
    public bool spotted = false;
    public bool attacked = false;
    public bool knockback = false;
    int timer = 0;
    
    public Material red;
    public Material green;

    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool isWandering = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Enemy Target");
    }

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
        else
        {
            if (isWandering == false)
            {
                StartCoroutine(Roaming());
            }

            if (isRotatingRight == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotationSpeed * 50.0f);
            }

            if (isRotatingLeft == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed * 50.0f);
            }

            if (isWalking == true)
            {
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * 25.0f);
            }
        }       

        if (maxHP <= 0)
        {
            if (counter != count)
            {
                slime1 = Instantiate(a_SlimePrefab);
                var newPos = transform.position + transform.right * 2;
                var newRot = transform.rotation;
                slime1.Init(newPos, newRot);
                slime1.player = player;
                slime1.count++;
                slime1.GetComponent<SphereCollider>().radius = gameObject.GetComponent<SphereCollider>().radius * 2;

                slime2 = Instantiate(a_SlimePrefab);
                newPos = transform.position - transform.right * 2;
                newRot = transform.rotation;
                slime2.Init(newPos, newRot);
                slime2.player = player;
                slime2.count++;
                slime2.GetComponent<SphereCollider>().radius = gameObject.GetComponent<SphereCollider>().radius * 2;
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
                    knockback = true;
                    timer = 1;
                    GetComponent<MeshRenderer>().material = red;
                    StartCoroutine(TimeDelay(1));
                }
            }
        }

        if (knockback == true)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3((transform.position.x - player.transform.position.x),
                2.0f, (transform.position.z - player.transform.position.z)).normalized, ForceMode.Impulse);
        }
    }

    public void Init(Vector3 position, Quaternion rotation)
    {
        enabled = true;
        maxHP = 16;
        transform.rotation = rotation;
        transform.position = position;
        transform.localScale = gameObject.transform.localScale / 2;
    }

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
        knockback = false;
        GetComponent<MeshRenderer>().material = green;
    }

    IEnumerator Roaming()
    {
        int RotationTime = Random.Range(1, 4);
        int RotateWait = Random.Range(1, 4);
        int RotateDirection = Random.Range(1, 2);
        int WalkWait = Random.Range(1, 5);
        int WalkTime = Random.Range(1, 4);

        isWandering = true;

        yield return new WaitForSeconds(WalkWait);

        isWalking = true;

        yield return new WaitForSeconds(WalkTime);

        isWalking = false;

        yield return new WaitForSeconds(RotateWait);

        if(RotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(RotationTime);
            isRotatingLeft = false;
        }
        
        if (RotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(RotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }
}
