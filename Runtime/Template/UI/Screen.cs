using System.Collections;
using UnityEngine;

namespace ProjectTemplate
{
    public abstract partial class Screen : UI
    {
        #region Public Methods

        public partial void Play()
        {
            if (!IsPlayed)
            {
                StartCoroutine(Play());

                IEnumerator Play()
                {
                    IsPlayed = true;

                    Display(true);

                    StartCoroutine(Execute());

                    yield return new WaitUntil(() => !IsPlayed);

                    Display(false);
                }
            }
        }

        public partial void Stop() => IsPlayed = false;

        #endregion
    }
}