using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMS_UI
{
    public abstract class CustomUIComponent : MonoBehaviour
    {
        private void Awake()
        {
            Init();
        }

        public abstract void Setup();
        public abstract void Configure();

        private void Init()
        {
            Setup();
            Configure();
        }

        private void OnValidate()
        {
            Init();
        }

    }


}
