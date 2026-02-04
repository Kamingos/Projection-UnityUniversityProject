using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SoundManager
{
    /// <summary>
    /// Пример работы: SoundManager.Instance.___;
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource otherSFXSource;
        [SerializeField] private AudioSource playerSFXSource;
        [SerializeField] private AudioSource uiSource;

        [SerializeField] private List<SoundElement>? soundslist;


        private static Sound _currentSound;

        private static AudioSource _temp;

        private static SoundManager _soundManager;
        public static SoundManager Instance => _soundManager;

        private void Awake()
        {
            if (_soundManager == null)
            {
                _soundManager = this;

                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);

            if (Instance.musicSource == null || Instance.playerSFXSource == null || Instance.uiSource == null)
                throw new Exception("Audio Sources is null");
        }


        private void OnValidate()
        {
            int soundsCount = Enum.GetValues(typeof(Sound)).Length;

            if (soundslist.Count > soundsCount)
            {
                soundslist.RemoveAt(soundsCount - 1);
                Debug.Log("Превышено максимальное количество звуков\n" + (soundsCount + 1).ToString() + " > " + soundsCount.ToString());
            }
        }

        /* 
            Реализовано через Enum, но можно и через строку при желании. разницы нет особо, но так удобнее вроде
            берётся случайный элемент из списка звуков
        */
        /// <returns>получилось ли запустить аудио</returns>
        public static bool Play(Sound sound, float volume = 0.2f, bool isLoop = false, AudioSource specialAudioSource = null)
        {
            if (Instance == null) return false;

            foreach (SoundElement? soundElement in Instance?.soundslist)
                if (sound == soundElement?.Sound)
                {
                    if (soundElement?.Type == AudioType.Music && sound == _currentSound) 
                        return false;

                    if (soundElement?.AudioList.Count == 0) 
                        return false;

                    int randInd = UnityEngine.Random.Range(0, (int)soundElement?.AudioList.Count);

                    _currentSound = sound;

                    // разные типы вызова, потому что для музыки важно прерывание, а для эффектов нет. эффекты могут накладываться друг на друга
                    switch (soundElement?.Type)
                    {
                        case AudioType.Music:

                            _temp = (specialAudioSource == null) ? Instance.musicSource: specialAudioSource;

                            _temp.clip = soundElement?.AudioList[randInd];

                            _temp.loop = isLoop;
                            _temp.volume = volume;

                            _temp.Play();
                            break;
                        case AudioType.SFX_Player:
                            _temp = (specialAudioSource == null) ? Instance.playerSFXSource : specialAudioSource;

                            _temp.loop = isLoop;
                            _temp.PlayOneShot(soundElement?.AudioList[randInd], volume);
                            break;
                        case AudioType.SFX_Other:
                            _temp = (specialAudioSource == null) ? Instance.otherSFXSource : specialAudioSource;

                            _temp.loop = isLoop;
                            _temp.PlayOneShot(soundElement?.AudioList[randInd], volume);
                            break;
                        case AudioType.UI:
                            Instance.uiSource.loop = isLoop;
                            Instance.uiSource.PlayOneShot(soundElement?.AudioList[randInd], volume);
                            break;
                        default:
                            break;
                    }


                    return true;
                }

            return false;
        }

        public static void Stop(AudioType audioType)
        {
            GetAudioSource(audioType).Stop();
        }

        private static AudioSource GetAudioSource(AudioType audioType)
        {
            return audioType switch
            {
                AudioType.Music => Instance.musicSource,
                AudioType.SFX_Other => Instance.playerSFXSource,
                AudioType.UI => Instance.uiSource,
                _ => null
            };
        }

    }
}