namespace HealthCare.Droid.Renderers.Calendar.MonoDroid.TimesSquare
{
    using Android.Util;

    /// <summary>
    /// Class Logr.
    /// </summary>
    public class Logr
    {
        /// <summary>
        /// ds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void D(string message)
        {
#if DEBUG
            Log.Debug("HealthCare.Droid.Renderers.Calendar.Calendar", message);
#endif
        }

        /// <summary>
        /// ds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="args">The arguments.</param>
        public static void D(string message, params object[] args)
        {
#if DEBUG
            D(string.Format(message, args));
#endif
        }
    }
}