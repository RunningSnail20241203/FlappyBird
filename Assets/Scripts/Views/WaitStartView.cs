using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaitStartView : BaseView
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI countDownText;

    private Coroutine _coroutine;

    public override void Show()
    {
        base.Show();
        gameObject.SetActive(true);
        countDownText.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowCountDown(float seconds)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        countDownText.gameObject.SetActive(true);
        button.gameObject.SetActive(false);
        _coroutine = StartCoroutine(CountDown(seconds));
    }

    public void HideCountDown()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        countDownText.gameObject.SetActive(false);
        button.gameObject.SetActive(true);
    }

    private void Awake()
    {
        button.onClick.AddListener(OnClickStartBtn);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    private void OnClickStartBtn()
    {
    }

    private IEnumerator CountDown(float seconds)
    {
        while (seconds > 0f)
        {
            countDownText.text = seconds.ToString(CultureInfo.CurrentCulture);
            seconds -= 1f;
            yield return new WaitForSeconds(1);
        }
    }
}