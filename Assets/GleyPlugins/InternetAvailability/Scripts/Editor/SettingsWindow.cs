namespace GleyInternetAvailability
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class SettingsWindow : EditorWindow
    {
        private WebsiteSettings websiteSettings;
        List<string> websitesToPing;

        [MenuItem("Window/Gley/Internet Availability", false, 110)]
        private static void Init()
        {
            SettingsWindow window = (SettingsWindow)GetWindow(typeof(SettingsWindow));
            window.titleContent = new GUIContent(" Internet Availability ");
            window.minSize = new Vector2(320, 220);
            window.Show();
        }


        /// <summary>
        /// Load Saved data
        /// </summary>
        private void OnEnable()
        {
            websiteSettings = Resources.Load<WebsiteSettings>("WebsiteSettingsData");
            if (websiteSettings == null)
            {
                CreateWebsiteSettings();
                websiteSettings = Resources.Load<WebsiteSettings>("WebsiteSettingsData");
            }

            LoadSettings(websiteSettings);
        }


        /// <summary>
        /// Draw Settings window UI
        /// </summary>
        private void OnGUI()
        {
            EditorStyles.label.wordWrap = true;
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Add websites to ping. If one of them responds, network connection is available and working.");
            EditorGUILayout.Space();
            for(int i=0;i<websitesToPing.Count;i++)
            {
                EditorGUILayout.BeginHorizontal();
                websitesToPing[i] = EditorGUILayout.TextField(websitesToPing[i]);
                if (GUILayout.Button("Test", GUILayout.Width(60)))
                {
                    Application.OpenURL(websitesToPing[i]);
                }
                if (GUILayout.Button("Remove", GUILayout.Width(60)))
                {
                    websitesToPing.RemoveAt(i);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Add new website"))
            {
                websitesToPing.Add("");
            }
            EditorGUILayout.Space();
            if (GUILayout.Button("Save"))
            {
                SaveSettings();
            }
        }


        /// <summary>
        /// Create settings data if is does not exists
        /// </summary>
        private void CreateWebsiteSettings()
        {
            WebsiteSettings asset = CreateInstance<WebsiteSettings>();
            if (!AssetDatabase.IsValidFolder("Assets/GleyPlugins/InternetAvailability/Resources"))
            {
                AssetDatabase.CreateFolder("Assets/GleyPlugins/InternetAvailability", "Resources");
                AssetDatabase.Refresh();
            }

            AssetDatabase.CreateAsset(asset, "Assets/GleyPlugins/InternetAvailability/Resources/WebsiteSettingsData.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="websiteSettings"></param>
        private void LoadSettings(WebsiteSettings websiteSettings)
        {
            websitesToPing = websiteSettings.websitesToPing;
        }


        /// <summary>
        /// Save settings
        /// </summary>
        private void SaveSettings()
        {
            websiteSettings.websitesToPing = websitesToPing;
            EditorUtility.SetDirty(websiteSettings);
        }
    }
}
