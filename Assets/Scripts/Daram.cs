using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 다람쥐 기본 클래스이며 세부 항목은 상속시켜서 사용
/// </summary>
public class Daram : MonoBehaviour {

    public static List<Daram> All = new List<Daram>();
    public static int DaramVariety = 1; // 현재 뿌려지는 다람쥐의 종류 개수 (레벨 차이는 고려 안함), 최솟값 1
    public static float VarietyModifier = 1;    // 다람쥐의 종류별 비율까지 고려한 보정값
    //public static int FindByType(string type, int level);

    public string Type;
    public int Level;
    [HideInInspector] public int InitialHP; // 다람쥐 초기 체력은 Stage1.cs에서 조정하세요
    public int Speed;
    //public int Cost;
    public GameObject Carcass;
    public string feature;

    public int HP {
        get { return _HP; }
        set { _HP = value; if (value <= 0) Die(); }
    }

    protected int move_stat; // 상하좌우중 어디로 움직이는지 

    private int _HP = 0;
    private Vector2 dir;
    private bool DieFlag = false;

    protected Animator Anim;


    // *중요*
    // Daram 하위 클래스의 Start()에 "base.Start();" 를 넣어주세요.
    // 생성자 쓰면 버그 생겨서 Start()로 써야함.
    protected void Start()
    {
        HP = InitialHP;

        All.Add(this);
    }

    // *중요*
    // Update()도 위와 동일
    protected void Update()
    {
        if (GameManager.gm.isPaused)
            Anim.speed = 0;
        else
        {
            Anim.speed = 1;
            Move();
        }
            
    }

    public void Die()
    {
        Vector2 pos = transform.position;
        Instantiate(Carcass, pos, Quaternion.identity);
        All.Remove(this);
        DieFlag = true;
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if(!DieFlag)
            All.Remove(this);
    }

    private float NextMove = 0;
    void Move()
    {
        if (NextMove <= GameManager.gm.time)
        {
            switch (move_stat=Random.Range(0, 4))
            {
                case 0:
                    dir = Vector2.up;
                    break;
                case 1:
                    dir = Vector2.down;
                    break;
                case 2:
                    dir = Vector2.left;
                    break;
                case 3:
                    dir = Vector2.right;
                    break;
            }

            NextMove = GameManager.gm.time + Random.Range(1.0f, 3.0f);    //방향전환 시간
        }

        CheckIfCantMove();

        transform.position += (Vector3) dir * (Speed * 0.001f);
    }

    void CheckIfCantMove() {
        float xPos = gameObject.transform.position.x;
        float yPos = gameObject.transform.position.y;
        if (Mathf.Abs(xPos-GameManager.gm.fieldCenterX) >= GameManager.gm.fieldWidth/2f && dir.x != 0)
            dir.x = (dir.x == xPos / Mathf.Abs(xPos)) ? 0 : dir.x;

        if (Mathf.Abs(yPos-GameManager.gm.fieldCenterY) >= GameManager.gm.fieldHeight/2f && dir.y != 0)
            dir.y = (dir.y == yPos / Mathf.Abs(yPos)) ? 0 : dir.y;
    }

    /// <param name="Type">"" = 종류에 관계없이</param>
    /// <param name="Level">0 = 레벨에 관계없이</param>
    public static int FindByType(string Type, int Level)
    {
        int sum = 0;
        foreach (Daram d in Daram.All)
            if ((Type == "" || d.Type == Type) && (Level == 0 || d.Level == Level))
                sum++;
        return sum;
    }

    public static void CalculateDaramVariety()
    {
        List<DaramDatabase> db = new List<DaramDatabase>();
     
        foreach (Daram d in Daram.All)
        {
            bool exist = false;
            for(int i = 0; i < db.Count; i++)
            {
                if (db[i].type == d.Type)
                {
                    exist = true;
                    db[i].num++;
                    break;
                }
            }
            if (!exist)
            {
                db.Add(new DaramDatabase());
                db[db.Count - 1].type = d.Type;
                db[db.Count - 1].num = 1;
            }
        }

        DaramVariety = Mathf.Max(db.Count, 1);

        float max = 0;
        foreach (DaramDatabase dd in db)
            if (dd.num > max)
                max = dd.num;
        VarietyModifier = Mathf.Max(Daram.All.Count / max, 1.0f);
    }

    //All.Remove()를 쓰기 위해 비교연산자가 필요함.
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        else if (((Daram)obj).GetInstanceID() == GetInstanceID())
            return true;
        else
            return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }


}

class DaramDatabase
{
    public string type;
    public int num;
}

