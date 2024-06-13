using System.Collections;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    public static CameraPos Instance;

    private GameObject PlayerObj;

    Vector3 localPosition = new Vector3();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }
    public void PlayCameraShake()
    {
        StopAllCoroutines();
        StartCoroutine(CameraShakeProcess(1.0f, 0.2f));
    }
    IEnumerator CameraShakeProcess(float shakeTime, float shakeSense)
    {
        float deltaTime = 0.0f;

        while (deltaTime < shakeTime)
        {
            deltaTime += Time.deltaTime;

            transform.localPosition = localPosition;
            Vector3 pos = new Vector3(PlayerObj.transform.position.x, 0, -10);
            pos.x = Random.Range(-shakeSense, shakeSense);
            pos.y = Random.Range(-shakeSense, shakeSense);
            pos.z = Random.Range(-shakeSense, shakeSense);
            transform.localPosition += pos;

            yield return new WaitForEndOfFrame();

            transform.localPosition = localPosition;

            yield return null;
        }
    }
    void Update()
    {
        localPosition = transform.localPosition;
        if (PlayerObj == null)
        {
            PlayerObj = GameObject.FindGameObjectWithTag("Player");
        }
        transform.position = new Vector3(PlayerObj.transform.position.x, 0, -10);
    }


}