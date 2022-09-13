using UnityEngine;

public class FXCanvas : MonoBehaviour
{
    public static FXCanvas instance;
    public ParticleSystem StarExplode;
    Canvas self;

    public void Initialize()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = 1;
    }


    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Canvas>();
        self.renderMode = RenderMode.ScreenSpaceCamera;
        self.worldCamera = Camera.main;
    }

    public static void explodeStars(Vector3 pos)
    {
        instance.StarExplode.transform.position = pos;
        instance.StarExplode.Play();
    }

    public static void explodeStarsLocalPos(Vector3 pos)
    {
        instance.StarExplode.transform.localPosition = pos;
        instance.StarExplode.Play();
    }


    // Update is called once per frame
    void Update()
    {
    }
}