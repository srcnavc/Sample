using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetPointer : MonoBehaviour
{
    RectTransform rectTransform;
    public GameObject follower, target;
    
    Image image;
    
    float MaxDistance = 50;
    Vector3 localscale;

    Camera cam; 
    void Awake()
    {
        
        cam = Camera.main;
        rectTransform = (RectTransform)transform;
        rectTransform.localScale = ((float)Screen.width / 1080f) * rectTransform.localScale;
        image = GetComponent<Image>();
        localscale =  (transform as RectTransform).localScale;



    }

    public void set(GameObject follower, GameObject target, Color color)
    {
        this.follower = follower;
        this.target = target;
        GetComponent<Image>().color = color;

    }

    // Update is called once per frame
    void Update()
    {
        rotateToTarget();
        setScale();
        SetPosition();
        
    }
    public void setScale()
    {
        float distance = Mathf.Clamp(Vector3.Distance(follower.transform.position, target.transform.position), 0, MaxDistance);
        float scale = 1;
        scale = Mathf.Lerp(1, .4f, distance / MaxDistance);

        (transform as RectTransform) .localScale = scale * localscale;
    
    }
    public void SetPosition()
    {
        

        Vector2 screenPosition =  cam.WorldToScreenPoint(target.transform.position);

        if (cam.transform.position.z-3.5f > target.transform.position.z)
            screenPosition *= -1;
     
        image.enabled = (screenPosition.x < 0 || screenPosition.x > Screen.width || screenPosition.y < 0 || screenPosition.y > Screen.height)&&target.gameObject.activeSelf;
        screenPosition.x = Mathf.Clamp(screenPosition.x, 0, Screen.width);
        screenPosition.y = Mathf.Clamp(screenPosition.y, 0, Screen.height);

       

        if (screenPosition.x == 0)
            screenPosition.x += image.rectTransform.sizeDelta.x * localscale.x;

        if (screenPosition.x == Screen.width)
            screenPosition.x -= image.rectTransform.sizeDelta.x * localscale.x;


        if (screenPosition.y == 0)
            screenPosition.y += image.rectTransform.sizeDelta.y * localscale.y;

        if (screenPosition.y == Screen.height)
            screenPosition.y -= image.rectTransform.sizeDelta.y * localscale.y;

        rectTransform.position = screenPosition;


    }

    public void rotateToTarget()
    {
        float angle = Mathf.Atan2(target.transform.position.z - follower.transform.position.z, target.transform.position.x- follower.transform.position.x)*Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle+90 );

    }
}
