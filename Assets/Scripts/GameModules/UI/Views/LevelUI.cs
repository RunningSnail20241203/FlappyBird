using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using GameModules.UI.ViewModels;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace GameModules.UI.Views
{
    public class LevelUI : UIBase
    {
        [SerializeField] private Button returnBtn;
        [SerializeField] private AssetReferenceGameObject itemPrefab;
        [SerializeField] private Transform content;

        private readonly List<LevelItemCom> _items = new();
        private List<LevelViewModel.LevelData> _latestDatas;
        private bool _isUpdating;

        public override void Initialize()
        {
            base.Initialize();

            returnBtn.onClick.AddListener(OnReturnBtnClick);
        }

        protected override void OnShow()
        {
            base.OnShow();

            var viewModel = GetViewModel<LevelViewModel>();
            UpdateLevels(viewModel.LevelInfos.Value);

            // viewModel.LevelInfos.CollectionChanged += OnCollectionChanged;
            viewModel.LevelInfos.OnValueChanged += UpdateLevels;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var viewModel = GetViewModel<LevelViewModel>();
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Move:
                default:
                    UpdateLevels(viewModel.LevelInfos.Value);
                    break;
            }
        }

        private void UpdateLevels(List<LevelViewModel.LevelData> datas)
        {
            if (_isUpdating)
            {
                _latestDatas = datas;
                return;
            }

            _isUpdating = true;
            StartCoroutine(UpdateLevelsAsync(datas));
        }

        private IEnumerator UpdateLevelsAsync(List<LevelViewModel.LevelData> datas)
        {
            // 先看一下有没有要新增的
            for (var i = _items.Count; i < datas.Count; i++)
            {
                var handle = itemPrefab.InstantiateAsync(content);
                yield return handle;
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    _isUpdating = false;
                    yield break;
                }

                var item = handle.Result;
                _items.Add(item.GetComponent<LevelItemCom>());
            }

            // 再把要减少的隐藏掉
            for (var i = datas.Count; i < _items.Count; i++)
            {
                _items[i].SetActive(false);
            }

            // 最后把最新数据全部刷上去
            for (var i = 0; i < datas.Count; i++)
            {
                var data = datas[i];
                _items[i].SetData(data.StarCount, data.IsPlayed, data.LevelId, OnItemClick);
            }

            _isUpdating = false;

            if (_latestDatas != null)
            {
                _latestDatas = null;
                var temp = _latestDatas;
                UpdateLevels(temp);
            }
        }

        private void OnItemClick(int levelId)
        {
            var viewModel = GetViewModel<LevelViewModel>();
            viewModel.PlayGame(levelId);
        }

        private void OnReturnBtnClick()
        {
            var viewModel = GetViewModel<LevelViewModel>();
            viewModel.ReturnMainMenu();
        }
    }
}