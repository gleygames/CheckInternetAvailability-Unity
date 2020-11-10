namespace GleyInternetAvailability
{
    using UnityEngine.Events;

    public class Network
    {
        /// <summary>
        /// This method should be called to check if the user has Internet connection
        /// </summary>
        /// <param name="completeMethod">Callback, called after all connection tests are done</param>
        public static void IsAvailable(UnityAction<ConnectionResult> completeMethod)
        {
            ConnectionManager.Instance.CheckConnection(completeMethod);
        }
    }
}
