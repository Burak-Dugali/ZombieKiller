using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{

    NavMeshAgent yapayZeka;
    public GameObject Hedef;

    [SerializeField]
    public float mesafe;
    public float TakipMesafesi = 5;
    public float TakipBirakmaMesafesi = 20;
    public bool takip;
    public float DurmaMesafesi = 1.5f;
    public float KosmaHizi = 5;
    public float YurumeHizi = 1;
    public bool Durdu;


    void Start()
    {
        yapayZeka = GetComponent<NavMeshAgent>();
        Vector3 pos = new Vector3(Random.Range(400.0f, 700.0f), 0, Random.Range(400.0f, 700.0f));
        yapayZeka.destination = pos;
    }

    void Update()
    {
        mesafe = Vector3.Distance(Hedef.transform.position, yapayZeka.transform.position);


        if (mesafe < TakipMesafesi)
        {
            takip = true;
        }
        if (takip == true)
        {
            yapayZeka.SetDestination(Hedef.transform.position);
            yapayZeka.speed = KosmaHizi;
            //Kosuyor
        }
        if (takip == true && mesafe > TakipBirakmaMesafesi)
        {
            takip = false;
            yapayZeka.speed = YurumeHizi;
            Vector3 pos = new Vector3(Random.Range(400.0f, 700.0f), 0, Random.Range(400.0f, 700.0f));
            yapayZeka.destination = pos;
            //Debug.LogWarning(pos);
        }
        if (mesafe < DurmaMesafesi)
        {
            yapayZeka.speed = 0;
            yapayZeka.velocity = Vector3.zero;
            Durdu = true;
        }
        if (mesafe > DurmaMesafesi + 0.1 && Durdu == true)
        {
            yapayZeka.speed = KosmaHizi;
            Durdu = false;
        }
        if (takip == false)
        {
            if (yapayZeka.remainingDistance < 1)
            {
                Vector3 pos = new Vector3(Random.Range(400.0f, 700.0f), 0, Random.Range(400.0f, 700.0f));
                yapayZeka.destination = pos;
                //Debug.LogWarning(pos);
            }
        }

    }
}