using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HitAccuracy : MonoBehaviour
{
    public Slider hitbar;
    public RectTransform hitTexture, rtHandle;
    public Coroutine coroutineSlider;

    private void Start()
    {

        ModifyHitTexture();
        coroutineSlider = StartCoroutine(LerpSlider());
        GameController.singleton.tolerance += Random.Range(5, 7);
        GameController.singleton.tickSpeed += Random.Range(0.009f, 0.01f);
        CombatManager.singleton.hitbarTolerance = Mathf.Clamp(CombatManager.singleton.hitbarTolerance + GameController.singleton.tolerance, 0, 590f);
        CombatManager.singleton.hitbarSpeed = Mathf.Clamp(CombatManager.singleton.hitbarSpeed + GameController.singleton.tickSpeed, 0, 1.9f);
    }

    private void OnEnable()
    {
        EventController.singleton.moveSlide += MakeHit;
    }

    private bool MakeHit()
    {
        EventController.singleton.moveSlide -= MakeHit;
        StopCoroutine(coroutineSlider);
        LeanTween.cancel(AnimationController.singleton.jugador);
        LeanTween.rotateZ(AnimationController.singleton.manoCompleta, Random.Range(-164f,-156f), .2f).setEaseInOutBounce().setOnComplete(
            (o )=> LeanTween.moveY(AnimationController.singleton.manoCompleta, 3f, .2f).setEaseLinear());
        AnimationController.singleton.ScaleOut(CombatManager.singleton.hitbar, .2f);
        return rectOverlaps(hitTexture, rtHandle);
    }

    private IEnumerator LerpSlider()
    {
        while (true)
        {
            yield return null;

            hitbar.value = Mathf.PingPong(Time.time * CombatManager.singleton.hitbarSpeed, 1);
        }
    }

    private bool rectOverlaps(RectTransform rectTransform1, RectTransform rectTransform2)
    {
        var corners = new Vector3[4];
        rectTransform1.GetWorldCorners(corners);
        var rec = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);

        rectTransform2.GetWorldCorners(corners);
        var rec2 = new Rect(corners[0].x, corners[0].y, corners[2].x - corners[0].x, corners[2].y - corners[0].y);

        return rec.Overlaps(rec2);
    }

    private void ModifyHitTexture()
    {
        var hitRatio = CombatManager.singleton.hitRatio;
        hitTexture.offsetMin = new Vector2(hitRatio * CombatManager.singleton.hitbarTolerance, hitTexture.offsetMin.y);
        hitTexture.offsetMax = new Vector2(-hitRatio * CombatManager.singleton.hitbarTolerance, hitTexture.offsetMax.y);
    }
}