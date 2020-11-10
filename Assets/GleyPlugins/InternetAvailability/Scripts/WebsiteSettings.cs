namespace GleyInternetAvailability
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Used for storing values inside editor
    /// </summary>
    public class WebsiteSettings : ScriptableObject
    {
        public List<string> websitesToPing = new List<string>() { "https://www.google.com", "http://www.baidu.com" };
    }
}