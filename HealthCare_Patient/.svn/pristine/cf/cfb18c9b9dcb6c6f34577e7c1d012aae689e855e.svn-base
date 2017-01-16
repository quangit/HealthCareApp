using System;
using System.Net;
using System.Windows.Navigation;
using Facebook;
using Facebook.Client;

namespace HealthCare.WinPhone.Converters
{
    public class CustomFacebookUriMapper : UriMapperBase
    {
        private const int MaxTokenLifeTime = 60;

        /// <summary>
        ///     Defines the key for access token in the query string representation
        /// </summary>
        private const string AccessTokenKey = "access_token";

        /// <summary>
        ///     Defines the key for error code in the query string representation
        /// </summary>
        private const string ErrorCodeKey = "error_code";

        /// <summary>
        ///     Defines the key for error description in the query string representation
        /// </summary>
        private const string ErrorDescriptionKey = "error_description";

        /// <summary>
        ///     Defines the key for error in the query string representation
        /// </summary>
        private const string ErrorKey = "error";

        /// <summary>
        ///     Defines the key for error reason in the query string representation
        /// </summary>
        private const string ErrorReasonKey = "error_reason";

        /// <summary>
        ///     Defines the key for expires_in in the query string representation
        /// </summary>
        private const string ExpiresInKey = "expires_in";

        /// <summary>
        ///     Defines the key for state in the query string representation
        /// </summary>
        private const string StateKey = "state";

        private const string EncodedLaunchUri = "encodedLaunchUri";
        public static bool IsNavigateByFacebookLogin { get; set; }

        public override Uri MapUri(Uri uri)
        {
            AccessTokenData session = null;
            try
            {
                var _uri = uri.ToString();
                var queryString = WebUtility.UrlDecode(_uri);
                session = new AccessTokenData();
                // parse out errors, if any
                var error = GetQueryStringValueFromUri(queryString, ErrorKey);
                var errorDescription = GetQueryStringValueFromUri(queryString, ErrorDescriptionKey);
                var errorReason = GetQueryStringValueFromUri(queryString, ErrorReasonKey);

                var errorCodeValue = 0;
                int.TryParse(GetQueryStringValueFromUri(queryString, ErrorCodeKey), out errorCodeValue);

                if (string.IsNullOrWhiteSpace(error) && errorCodeValue == 0)
                {
                    // parse out string values
                    session.AccessToken = GetQueryStringValueFromUri(queryString, AccessTokenKey);
                    session.State = GetQueryStringValueFromUri(queryString, StateKey);
                    //var encodedLaunchUri = GetQueryStringValueFromUri(queryString, EncodedLaunchUri);
                    //if (!String.IsNullOrWhiteSpace(encodedLaunchUri))
                    //{
                    //    //session.AppId = encodedLaunchUri.Substring(2); // ignore the fb
                    //    var index = encodedLaunchUri.IndexOf(":", StringComparison.InvariantCulture);
                    //    var appId = encodedLaunchUri.Substring(2, index - 2);
                    //    if (!String.IsNullOrWhiteSpace(appId))
                    //    {
                    //    }
                    //}

                    // parse out other types
                    long expiresInValue;
                    var now = DateTime.UtcNow;
                    if (long.TryParse(GetQueryStringValueFromUri(queryString, ExpiresInKey), out expiresInValue))
                    {
                        session.Expires = now + TimeSpan.FromSeconds(expiresInValue);
                        session.Issued = now -
                                         (TimeSpan.FromDays(MaxTokenLifeTime) - TimeSpan.FromSeconds(expiresInValue));
                    }
                }
                else
                {
                    throw new FacebookOAuthException(
                        string.Format("{0}: {1}", error, errorDescription),
                        errorReason,
                        errorCodeValue,
                        0);
                }

                if (!string.IsNullOrWhiteSpace(session.AccessToken))
                {
                    session.AppId = Session.AppId;
                    Session.ActiveSession.CurrentAccessTokenData = session;

                    // trigger the event handler with the session
                    if (Session.OnFacebookAuthenticationFinished != null)
                    {
                        Session.OnFacebookAuthenticationFinished(session);
                    }
                }
            }
            catch
            {
                // fire the authentication finished handler with the exception 
                if (Session.OnFacebookAuthenticationFinished != null)
                {
                    Session.OnFacebookAuthenticationFinished(session);
                }
            }

            if (uri.ToString().StartsWith("/Protocol"))
            {
                // Read which page to redirect to when redirecting from the Facebook authentication.

                IsNavigateByFacebookLogin = true;
                return new Uri("/" + "MainPage.xaml", UriKind.Relative);
            }

            // Otherwise perform normal launch.
            return uri;
        }

        private static string GetQueryStringValueFromUri(string uri, string key)
        {
            var questionMarkIndex = uri.LastIndexOf("?", StringComparison.Ordinal);
            if (questionMarkIndex == -1)
            {
                // if no questionmark found, this is just a queryString
                questionMarkIndex = 0;
            }

            var queryString = uri.Substring(questionMarkIndex, uri.Length - questionMarkIndex);
            queryString = queryString.Replace("#", string.Empty).Replace("?", string.Empty);

            var keyValuePairs = queryString.Split('&');
            for (var i = 0; i < keyValuePairs.Length; i++)
            {
                var pair = keyValuePairs[i].Split('=');
                if (pair[0].ToLowerInvariant() == key.ToLowerInvariant())
                {
                    return WebUtility.UrlDecode(pair[1]);
                }
            }

            return string.Empty;
        }
    }
}