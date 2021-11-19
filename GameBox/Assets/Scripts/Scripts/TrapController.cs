using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject buttonMenu;

    private bool ok = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap") && ok)
        {
            gameObject.GetComponent<PlayerController>().StopSpeed();
            gameObject.GetComponent<PlayerController>().StartDeath();
            MusicController audio = GetComponent<MusicController>();
            audio.DieMusic();
            ok = !ok;
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        buttonMenu.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<DistanceController>().LoadAllDis();
        gameObject.GetComponent<DieController>().DeathScreen();
    }
}
