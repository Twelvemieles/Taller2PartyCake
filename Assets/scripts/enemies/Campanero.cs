using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campanero : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public LayerMask enemyMask;
    public bool observado;

    private Vector3 Jugador;
    public float meshResolution;
    public int edgeResolveIterations;
    public float edgeDstThreshold;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    [SerializeField]
    private float velocidadGiro = 2;
    private float time;
    private float elapsedTime = 0;
   



    private List<Transform> visibleTargets = new List<Transform>();
    private List<GameObject> visibleEnemies = new List<GameObject>();

    public List<Transform> VisibleTargets
    {
        get
        {
            return visibleTargets;
        }

        set
        {
            visibleTargets = value;
        }
    }

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetWithDelay", .2f);
        StartCoroutine("avisando", 1f);
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    IEnumerator avisando(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindEnemies(Jugador);
        }
    }
    void LateUpdate()
    {
        DrawFieldOfView();
    }
    void FindEnemies(Vector3 jugador)
    {
        time = velocidadGiro;
        //visibleEnemies.Clear();
        if (jugador != new Vector3(0, 0, 0))
        {
            Collider[] EnemiesInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, enemyMask);
            for (int i = 0; i < EnemiesInViewRadius.Length; i++)
            {
                // GameObject target = EnemiesInViewRadius[i].gameObject;

                RootScript enemy = EnemiesInViewRadius[i].GetComponent<RootScript>();
                if (enemy.Estado != 2)
                {
                    enemy.Target = jugador;
                    enemy.Estado = 3;
                    //visibleEnemies.Add(enemy);
                    print("le avisé a los otros ene");
                    
                }

            }
        }
    }
        void FindVisibleTargets()
    {
        
        Jugador = new Vector3(0,0,0);
        visibleTargets.Clear();
        
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    /*
                        */
                    
                    
                    observado = true;
                    visibleTargets.Add(target);
                    Jugador = target.position;
                    time = time * 0.25f;

                }
            }
        }
    }
    
    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
                if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }

            }
            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int u = 0; u < vertexCount - 1; u++)
        {
            vertices[u + 1] = transform.InverseTransformPoint(viewPoints[u]);

            if (u < vertexCount - 2)
            {
                triangles[u * 3] = 0;
                triangles[u * 3 + 1] = u + 1;
                triangles[u * 3 + 2] = u + 2;
            }
        }

        viewMesh.Clear();

        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
            if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }


    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }





    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
       /** if (visibleTargets.Count == 0)
        {
            Collider[] EnemiesInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, enemyMask);
            for (int u = 0; u < EnemiesInViewRadius.Length; u++)
            {
                RootScript enemy = EnemiesInViewRadius[u].GetComponent<RootScript>();

                if (enemy.Estado != 2)
                {
                    enemy.Estado = 0;
                    time = 2;
                }
            }
            observado = false;
        }
    */
        elapsedTime += Time.deltaTime;

        transform.localEulerAngles = Vector3.Slerp(new Vector3(0, 0, 0), new Vector3(0, 359, 0), elapsedTime / time);
        if (transform.localEulerAngles.y >= 359)
        {
            elapsedTime = 0;
        }
    }



    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}

