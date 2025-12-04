using System;
using System.Collections;
using System.Collections.Generic;
using Infra;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameModules.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private readonly Dictionary<string, AudioClip> _audioClips = new();
        private readonly Dictionary<string, AsyncOperationHandle<AudioClip>> _asyncOperationHandles = new();
        private AudioSource _audioSource;

        private const string AudioPlayerTag = "AudioPlayer";

        public void PlayBackgroundMusic(string audioName)
        {
        }

        public void PlaySound(string audioName)
        {
            if (_audioClips.TryGetValue(audioName, out var clip))
            {
                PlaySoundInternal(clip);
            }
            else
            {
                StartCoroutine(LoadAudio(audioName, PlaySoundInternal));
            }

            return;

            void PlaySoundInternal(AudioClip audioClip)
            {
                _audioSource.PlayOneShot(audioClip);
                Debug.Log($"Play sound {audioName} success");
            }
        }

        private IEnumerator LoadAudio(string audioName, Action<AudioClip> callback)
        {
            if (_asyncOperationHandles.ContainsKey(audioName))
            {
                yield break;
            }

            var handle = Addressables.LoadAssetAsync<AudioClip>(GetAudioPath(audioName));
            _asyncOperationHandles.Add(audioName, handle);
            yield return handle;
            Debug.Log($"Load audio {audioName} success");
            _audioClips.TryAdd(audioName, handle.Result);
            callback?.Invoke(handle.Result);
        }

        private string GetAudioPath(string audioName)
        {
            return $"Assets/Audios/Sounds/{audioName}.ogg";
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var obj = GameObject.FindGameObjectWithTag(AudioPlayerTag);
            _audioSource = obj.GetComponent<AudioSource>();
        }
    }
}