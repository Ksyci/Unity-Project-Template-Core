using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTemplate
{
    public partial class ProjectProperties : ScriptableObject
    {
        #region Static Methods

        public static partial ProjectProperties Get()
        {
            if (_instance == null)
            {
                #if UNITY_EDITOR

                string[] guids = AssetDatabase.FindAssets("t:" + typeof(ProjectProperties).Name);

                try
                {
                    if (guids.Length == 0)
                    {
                        throw new Error.ProjectPropertyNotFoundException();
                    }
                    else if (guids.Length > 1)
                    {
                        throw new Error.InstanceNotUniqueException(typeof(ProjectProperties));
                    }

                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);

                    _instance = AssetDatabase.LoadAssetAtPath<ProjectProperties>(path);

                }
                catch (Exception e)
                {
                    Error.Warn(e);
                }

                #else

                _instance = Resources.Load<ProjectProperties>(PATH);
        
                if (_instance == null)
                {
                    throw new System.Exception(WARNING);
                }

                #endif
            }

            return _instance;
        }

        #endregion

        #region Public Methods

        public partial void UpdateScenes()
        {
            HashSet<string> namesSet = new(FindSceneNames());

            List<GameScene> filteredScenes = _scenes.Where(scene => namesSet.Contains(scene.Name)).ToList();

            HashSet<string> existingNames = new(filteredScenes.Select(s => s.Name));

            foreach (string name in namesSet)
            {
                if (!existingNames.Contains(name))
                {
                    GameScene newScene = new(name, GameStates.Select(s => s.Name).ToArray());

                    filteredScenes.Add(newScene);
                }
            }

            _scenes = filteredScenes.OrderBy(scene => scene.Name).ToArray();
        }

        public partial void UpdatePropertyTypeEnum()
        {
            foreach (SettingSection section in _settingSections)
            {
                if(section != null)
                {
                    foreach (SettingTemplate template in section.SettingTemplates)
                    {
                        template.Type.Update();
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private partial string[] FindSceneNames()
        {
            List<string> sceneNames = new();

            int sceneCount = SceneManager.sceneCountInBuildSettings;

            for (int i = 0; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);

                sceneNames.Add(sceneName);
            }

            return sceneNames.ToArray();
        }

        #endregion
    }
}
