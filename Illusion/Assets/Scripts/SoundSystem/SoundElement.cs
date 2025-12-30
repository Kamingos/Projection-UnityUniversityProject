using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SoundManager
{
    [Serializable]
    public struct SoundElement
    {
        [SerializeField] private Sound sound;
        [SerializeField] private AudioType type;
        [SerializeField] private List<AudioClip> audioList;

        [field: NonSerialized] public Sound Sound => sound;
        [field: NonSerialized] public AudioType Type => type;
        [field: NonSerialized] public List<AudioClip> AudioList => audioList;
    }
}