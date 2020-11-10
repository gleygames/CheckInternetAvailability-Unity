namespace GleyInternetAvailability
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Networking;

    public class ConnectionManager : MonoBehaviour
    {
        private static ConnectionManager instance;
        int availableServers;

        /// <summary>
        /// Create a static instance for this class
        /// </summary>
        public static ConnectionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("ConnectionManager");
                    instance = go.AddComponent<ConnectionManager>();
                }
                return instance;
            }
        }


        /// <summary>
        /// Call this method to check the Internet availability of the device
        /// </summary>
        /// <param name="completeMethod"></param>
        public void CheckConnection(UnityAction<ConnectionResult> completeMethod)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                completeMethod(ConnectionResult.NetorkCardDisabled);
                return;
            }

            WebsiteSettings websiteSettings = Resources.Load<WebsiteSettings>("WebsiteSettingsData");

            availableServers = websiteSettings.websitesToPing.Count;
            for (int i = 0; i < websiteSettings.websitesToPing.Count; i++)
            {
                StartCoroutine(TestURL(websiteSettings.websitesToPing[i], completeMethod));
            }
        }


        /// <summary>
        /// Tries to load the url. If success the complete method is triggered 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="completeMethod"></param>
        /// <returns></returns>
        IEnumerator TestURL(string url, UnityAction<ConnectionResult> completeMethod)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError || !string.IsNullOrEmpty(www.error))
            {
                ConnectionFailed(completeMethod);       
            }
            else
            {
                completeMethod(ConnectionResult.Working);
                StopAllCoroutines();
            }
        }


        /// <summary>
        /// When all test fail, the fail method is triggered 
        /// </summary>
        /// <param name="completeMethod"></param>
        private void ConnectionFailed(UnityAction<ConnectionResult> completeMethod)
        {
            availableServers--;
            if (availableServers == 0)
            {
                completeMethod(ConnectionResult.CannotReachWebsite);
            }
        }
    }
}
