using UnityEngine;
using System.Collections;
using System;
namespace UISystem
{
    public abstract class UIBaseWindowAnimation : MonoBehaviour
    {
        public abstract void PlayOpenAnimation();

        public abstract void PlayCloseAnimation();

        public abstract void UpdateAnimation();

        public abstract bool IsAnimationPlaying();

        public abstract void HideImmediately();

        public abstract void ShowImmediately();

        public abstract void StopAnimation();
    }
}