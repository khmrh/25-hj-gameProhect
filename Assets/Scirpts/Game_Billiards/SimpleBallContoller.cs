using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBallContoller : MonoBehaviour
{
    [Header("기본 설정")]
    public float power = 10f;
    public Sprite arrowSprite;

    private Rigidbody rb;
    private GameObject arrow;
    private bool isDragging = false;
    private Vector3 startPos;

    //턴 관리를 위한 전역 변수(모든 공이 공유)
    static bool isAnyBatlPlayig = false;
    static bool isAnybatlMoveing = false;

    // Start is called before the first frame update
    void Start()
    {
        SetupBall();
    }

    // Update is called once per frame
    void Update()
    {
        Handleinput();
        UpdateArrow();
    }

    void SetupBall()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        //물리설정
        rb.mass = 1;
        rb.drag = 1;
    }

    public bool isMoving()
    {
        return rb.velocity.magnitude > 0.2f;
    }

    void Handleinput()
    {
        if(isMoving()) return;

        if (Input.GetMouseButtonDown(0))
        {
           StartDrag();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mouseDelta = Input.mousePosition - startPos;
        float force = mouseDelta.magnitude * 0.01f * power;

        if (force < 5) force = 5;

        Vector3 direction = new Vector3(-mouseDelta.x, 0, -mouseDelta.y).normalized;

        rb.AddForce(direction * force, ForceMode.Impulse);

        //턴 매니저에게 공을 쳐다고 신호

        //정리

        isDragging = false;
        Destroy(arrow);
        arrow = null;

        Debug.Log("발싸! 힘 : " +  force);
    }

    void CreateArrow()
    {
        if (arrow != null)
        {
            Destroy(arrow);
        }

        arrow = new GameObject("Arrow");
        SpriteRenderer sr = arrow.AddComponent<SpriteRenderer>();

        sr.sprite = arrowSprite;
        sr.color = Color.green;
        sr.sortingOrder = 10;

        arrow.transform.position = transform.position + Vector3.up;
        arrow.transform.localScale = Vector3.one;
    }

    void UpdateArrow()
    {
        if(!isDragging  || arrow == null) return;

        Vector3 mouseDelta = Input.mousePosition - startPos;
        float distance = mouseDelta.magnitude;

        float size = Mathf.Clamp(distance * 0.01f, 0.5f, 2f);
        arrow.transform.localScale = Vector3.one * size;

        SpriteRenderer sr =arrow .GetComponent<SpriteRenderer>();
        float colorRatio = Mathf.Clamp01(distance * 0.005f);
        sr.color = Color.Lerp(Color.green, Color.red, colorRatio);

        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);

        if (distance > 10f)
        {
            Vector3 direction = new Vector3(-mouseDelta.x, 0, -mouseDelta.y);
            //2D평면(위에서 본 시점)에서 direction벡터가 가르키는 방향으로 각도를 변환
            float angle= Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(90, angle, 0);
        }
    }

    void StartDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                startPos = Input.mousePosition;
                CreateArrow();
                Debug.Log("드래그 시작");
            }
        }
    }
}
