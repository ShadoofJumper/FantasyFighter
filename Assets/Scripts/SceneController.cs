using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    #region Singlton

    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Try create another instance of game manager!");
        }

    }
    #endregion

    [SerializeField] private GameObject player;
    [SerializeField] private AnimationCurve rotateCurve;

    private GameObject[] billboardSprites;
    private float rotateStep = 90;
    private float rotateTime = 0.5f;
    private bool isRotate   = false;


    void Start()
    {
        ActivateBillboardsEffect();
    }


    private void Update()
    {
        RotateWorld();
    }

    private void RotateWorld()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotatePlayer(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RotatePlayer(false);
        }
    }

    private void RotatePlayer(bool isClockWise)
    {
        if (isRotate)
            return;

        float angle = isClockWise ? rotateStep : rotateStep * -1;
        Vector3 eulerAngleStep = new Vector3(0, angle, 0);

        Quaternion startRotatetion = player.transform.rotation;
        Quaternion finalRotation = Quaternion.Euler(player.transform.eulerAngles + eulerAngleStep);

        StartCoroutine(RotateOverTime(startRotatetion, finalRotation, rotateTime));
    }

    IEnumerator RotateOverTime(Quaternion a, Quaternion b, float rTime)
    {
        isRotate = true;
        float t     = 0.0f;
        float step  = 1.0f / rTime;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * step;
            float moveStep = rotateCurve.Evaluate(t);
            player.transform.rotation = Quaternion.Slerp(a, b, moveStep);

            yield return null;
        }
        isRotate = false;
    }



    private void ActivateBillboardsEffect()
    {
        billboardSprites = GameObject.FindGameObjectsWithTag("Billboard");
        foreach (GameObject spriteObject in billboardSprites)
        {
            spriteObject.AddComponent<Billboards>();
        }
    }
}
