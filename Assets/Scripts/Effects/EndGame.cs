using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private bool endGame, slowedTime;
    private float angle,rotationSpeed;

    [SerializeField] private GameObject endGameTransition;
    [SerializeField] private GameObject lastScene;
    [SerializeField] private GameObject[] thingsToDesactivate;

    private Transform cam;

    private void Start()
    {
        rotationSpeed = 360;
        cam = Camera.main.transform;
    }

    public void OnBossDefeated()
    {
        endGame = true;
        endGameTransition.SetActive(true);
        angle = Mathf.Acos(cam.localPosition.x);
        StartCoroutine("Transition");

    }
    private void LateUpdate()
    {
        if (!endGame) return;

        if (!slowedTime)
        {
            Time.timeScale -= Time.deltaTime * 0.1f;
            Time.fixedDeltaTime = 0.001F * Time.timeScale;

            angle += rotationSpeed * Time.deltaTime;

            float radians = Mathf.Deg2Rad * angle;

            float x = Mathf.Cos(radians);
            float y = Mathf.Sin(radians);

            Vector3 newPos = new Vector3(x, y, 0);
            cam.localPosition = Vector3.MoveTowards(cam.localPosition, newPos, 1f);
        }
    }
    IEnumerator Transition()
    {
        yield return new WaitForSeconds(0f);
        AudioManager.instance.ResetAudioSource();
        AudioManager.instance.PlaySound("last_scene");
        cam.transform.parent.GetComponent<CameraFollow>().enabled = false;
        cam.GetComponent<Animator>().SetTrigger("LastScene");

        for (int i = 0; i < thingsToDesactivate.Length; i++)
        {
            thingsToDesactivate[i].SetActive(false);
        }
        yield return new WaitForSeconds(3f);
        lastScene.SetActive(true);

        yield return new WaitForSeconds(3f);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        slowedTime = true;

    }
}
