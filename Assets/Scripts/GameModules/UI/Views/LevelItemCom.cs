using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace GameModules.UI.Views
{
    public class LevelItemCom : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private Image star1;
        [SerializeField] private Image star2;
        [SerializeField] private Image star3;
        [SerializeField] private Image bg;
        [SerializeField] private AssetReferenceSprite notPlayedBg;
        [SerializeField] private AssetReferenceSprite playedBg;
        [SerializeField] private AssetReferenceSprite completedBg;
        [SerializeField] private Color unLightColor = Color.white;
        [SerializeField] private Color lightColor = Color.yellow;
        
        private Action<int> _onClick;
        private int _levelId;

        public void SetData(int starCount, bool isPlayed, int levelId, Action<int> clickCallback)
        {
            _onClick = clickCallback;
            _levelId = levelId;
            levelText.text = levelId.ToString();

            star1.color = starCount >= 1 ? lightColor : unLightColor;
            star2.color = starCount >= 2 ? lightColor : unLightColor;
            star3.color = starCount >= 3 ? lightColor : unLightColor;

            var sp = isPlayed ? starCount >= 3 ? completedBg : playedBg : notPlayedBg;
            StartCoroutine(SetBg(sp));
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        IEnumerator SetBg(AssetReferenceSprite ars)
        {
            if (ars.IsValid() && ars.IsDone && ars.Asset != null)
            {
                bg.sprite = ars.Asset as Sprite;
                yield break;
            }

            var handle = ars.LoadAssetAsync();
            yield return handle;

            bg.sprite = ars.Asset as Sprite;
        }

        private void Awake()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            if (notPlayedBg.IsValid())
                notPlayedBg.ReleaseAsset();
            if (playedBg.IsValid())
                playedBg.ReleaseAsset();
            if (completedBg.IsValid())
                completedBg.ReleaseAsset();
        }

        private void OnButtonClick()
        {
            _onClick?.Invoke(_levelId);
        }
    }
}