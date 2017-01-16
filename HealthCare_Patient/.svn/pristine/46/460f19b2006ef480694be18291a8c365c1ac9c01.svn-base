using System.Threading;
using Xamarin.Forms;

namespace HealthCare.Helpers
{
    /// <summary>
    ///     This class will do a variety of animation types on View Custom
    /// </summary
    public class MyAnimation
    {
        public enum AnimationType
        {
            Wink,
            Vibrate,
            Rotate,
            Translate
        };

        private readonly AnimationType _Type;
        private readonly View _View;
        private CancellationTokenSource tokenSource;

        public MyAnimation(AnimationType type, View view)
        {
            _Type = type;
            _View = view;
        }

        public bool isPlay { get; set; }

        public async void play()
        {
            if (isPlay)
                return;
            isPlay = true;
            switch (_Type)
            {
                case AnimationType.Wink:
                    for (;;)
                    {
                        await _View.FadeTo(0, 500, Easing.Linear);
                        if (!isPlay) break;
                        await _View.FadeTo(1, 500, Easing.Linear);
                        if (!isPlay) break;
                    }
                    break;
                case AnimationType.Rotate:
                    break;
                case AnimationType.Vibrate:
                    break;
                case AnimationType.Translate:
                    break;
                default:
                    return;
            }
        }

        public void Stop()
        {
            if (isPlay)
            {
                isPlay = false;
                ViewExtensions.CancelAnimations(_View);
            }
        }
    }
}