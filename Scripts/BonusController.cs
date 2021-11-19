using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusController : MonoBehaviour
{
    [SerializeField] private Text bonus;
    [SerializeField] private Text bonus1;
    [SerializeField] private Text bonusScr;
    [SerializeField] private int score;

    private int bonusAdd;
    private int bonusAll;

    private bool ok = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bonus") && ok)
        {
            AddBonus();
            StartCoroutine(DestroyBonus(other.gameObject));
            ok = !ok;
        }
    }

    IEnumerator DestroyBonus(GameObject other)
    {
        Animator anim = other.GetComponentInChildren<Animator>();
        anim.SetBool("isDie", true);

        MusicController audio = GetComponent<MusicController>();
        audio.BonusMusic();

        yield return new WaitForSeconds(0.2f);
        Destroy(other);
        ok = true;
    }

    public void AddBonus()
    {
        bonusAdd += 1;
        bonusAll = score * bonusAdd;
        bonus.text = bonusAdd.ToString();
        bonus1.text = bonusAll.ToString();
        bonusScr.text = ("Bonus " + bonusAdd + " * " + score).ToString();
    }

    public int ScoreBonus()
    {
        return bonusAll;
    }
}
