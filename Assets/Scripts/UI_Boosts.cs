using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Boosts : MonoBehaviour
{
    DoorManager doorManager;
    //public PlayerStats playerStats;
    //public Pacemaker pacemaker;
    //public Cachet cachet;
    //public Oxygen oxygene;
    [SerializeField] private Image warningOpeningImage;
    [SerializeField] private Image warningClosingImage;
    [SerializeField] private Image iconPacemaker;
    [SerializeField] private Image iconPills;
    [SerializeField] private Image iconOxygen;
    [SerializeField] private Image worldPosOxygen;

    private bool fadeInOpening = false, fadeInClosing = false;
    private bool fadePacemaker = false;
    private bool cachetUsed = false, firstTimeCachet = true;

    private float timerWarningOpening = 0.0f, timerWarningClosing = 0.0f;
    private float timerPacemaker = 0.0f;
    private float timerOxygene = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        #region Warnings Update
        if (fadeInOpening)
        {
            if(warningOpeningImage.enabled == false)
            {
                warningOpeningImage.enabled = true;
                timerWarningOpening = 0;
                warningOpeningImage.color = new Color(warningOpeningImage.color.r, warningOpeningImage.color.g, warningOpeningImage.color.b, 1);
            }
            timerWarningOpening += Time.deltaTime;
            warningOpeningImage.color = new Color(warningOpeningImage.color.r, warningOpeningImage.color.g, warningOpeningImage.color.b, Mathf.PingPong(timerWarningOpening, 0.75f));
        }
        else if(fadeInClosing)
        {
            if (warningClosingImage.enabled == false)
            {
                warningClosingImage.enabled = true;
                timerWarningOpening = 0;
                warningClosingImage.color = new Color(warningClosingImage.color.r, warningClosingImage.color.g, warningClosingImage.color.b, 1);
            }
            timerWarningOpening += Time.deltaTime;
            warningClosingImage.enabled = true;
            warningClosingImage.color = new Color(warningClosingImage.color.r, warningClosingImage.color.g, warningClosingImage.color.b, Mathf.PingPong(timerWarningOpening, 0.75f));
        }
        #endregion

        #region Pacemaker Update
        /*if (playerStats.hasPacemaker)
        {
            A DECOMMENTER QUAND INTEGRATION
            EnablePacemaker();
        }*/

        if (fadePacemaker)
        {
            if (iconPacemaker.enabled == false)
            {
                iconPacemaker.enabled = true;
                timerPacemaker = 0;
                iconPacemaker.color = new Color(iconPacemaker.color.r, iconPacemaker.color.g, iconPacemaker.color.b, 1);
            }
            timerPacemaker += Time.deltaTime;
            iconPacemaker.color = new Color(iconPacemaker.color.r, iconPacemaker.color.g, iconPacemaker.color.b, Mathf.PingPong(timerPacemaker, 0.75f));
        }
        #endregion

        #region Cachet Update
        /*if(!playerStats.hasCachet && firstTimeCachet)
        {
            firstTimeCachet = false;
            cachetUsed = true;
        }*/

        if (cachetUsed)
        {
            StartCoroutine("Cachet");
            cachetUsed = false;
        }
        #endregion

        #region Oxygene Update
        /*if (oxygene.hasOxygenBuff)
        {
            timerWarningOpening += Time.deltaTime;
            iconOxygen.color = new Color(iconOxygen.color.r, iconOxygen.color.g, iconOxygen.color.b, Mathf.PingPong(timerOxygene, 0.75f));
        }
        else
        {
            timerOxygene = 0;
            iconOxygen.color = new Color(iconOxygen.color.r, iconOxygen.color.g, iconOxygen.color.b, 1);
        }*/
        #endregion
    }

    #region Warnings
    public void WarningOpening()
    {
        StartCoroutine("WarningOpeningDoor");
    }

    public void WarningClosing()
    {
        StartCoroutine("WarningClosingDoor");
    }

    IEnumerator WarningOpeningDoor()
    {
        yield return new WaitForSeconds(doorManager._doorTimerClosed);
        fadeInOpening = true;
        yield return new WaitForSeconds(4.5f);
        fadeInOpening = false;
        warningOpeningImage.enabled = false;
    }

    IEnumerator WarningClosingDoor()
    {
        yield return new WaitForSeconds(doorManager._doorTimerClosed);
        fadeInClosing = true;
        yield return new WaitForSeconds(4.5f);
        fadeInClosing = false;
        warningClosingImage.enabled = false;
    }
    #endregion

    #region Pacemaker
    public void EnablePacemaker()
    {
        StartCoroutine("PaceMaker");
    }
    /*IEnumerator PaceMaker()
    {
        iconPacemaker.enabled = true;
        yield return new WaitForSeconds((pacemaker.buffTimer * 2) / 3);
        fadePacemaker = true;
        yield return new WaitForSeconds(pacemaker.buffTimer / 3);
        iconPacemaker.enabled = false;
        fadePacemaker = false;
        iconPacemaker.color = new Color(iconPacemaker.color.r, iconPacemaker.color.g, iconPacemaker.color.b, 1);
    }*/
    #endregion

    #region Cachet
    IEnumerator Cachet()
    {
        iconPills.enabled = true;
        yield return new WaitForSeconds(3.0f);
        iconPills.enabled = false;
    }
    #endregion
}
