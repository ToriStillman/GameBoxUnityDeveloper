using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
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
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<DistanceController>().LoadAllDis();
        gameObject.GetComponent<DieController>().DeathScreen();
    }
}
