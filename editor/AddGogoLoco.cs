using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Windows;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using Directory = System.IO.Directory;


namespace GoGoLoco
{
    public class AddGogoLoco : EditorWindow
    {
        public GameObject avatar;
        private static readonly string WDfolderPath = "Assets/GoGo/GoLoco/ControllersWD/";
        private static readonly string folderPath = "Assets/GoGo/GoLoco/Controllers/";
        private static readonly string paramenuFolderPath = "Assets/GoGo/GoLoco/MainMenu/";
        private static List<string> controllers;
        private static List<string> paramenu;

        [MenuItem("Tools/GoGo Loco Automator", false, 0)]
        public static void ShowWindow()
        {
            AddGogoLoco window = (AddGogoLoco) EditorWindow.GetWindow(typeof(AddGogoLoco));
        }

        private void OnGUI()
        {
            avatar = EditorGUILayout.ObjectField("avatar", avatar, typeof(object), true) as GameObject;

            if (GUILayout.Button("Select Avatar in scene"))
            {
                var objs = Selection.gameObjects;
                if (objs.Length < 1) return;

                avatar = objs[0];
            }

            if (GUILayout.Button("Add Gogo Loco Write Defaults"))
            {
                var descriptor = BoilerPlateGetDescriptor();
                
                if (descriptor == null) return;

                BoilerPlateFindControllers(WDfolderPath);

                BoilerPlateAddControllersAndMenus(descriptor);

            }
            
            if (GUILayout.Button("Add Gogo Loco (not Write Defaults)"))
            {
                var descriptor = BoilerPlateGetDescriptor();
                if (descriptor == null) return;

                BoilerPlateFindControllers(folderPath);

                BoilerPlateAddControllersAndMenus(descriptor);

            }
            
            if (GUILayout.Button("Add Descriptor"))
            {
                var descriptor = avatar.GetComponent<VRCAvatarDescriptor>();
                if (descriptor == null)
                {
                    avatar.AddComponent<VRCAvatarDescriptor>();
                }
                else
                {
                    Debug.Log("An avatar descriptor already exists! on this avatar");
                }

            }
            GUILayout.Label("GoGo Loco created by Franda\nEditor script created by akalink\nIf you run into an issue, contact akalink" +
                            "\n@mcphersonsound twitter\n akalink github"); 
        }

        #region BoilerPlateCode
        
        private VRCAvatarDescriptor BoilerPlateGetDescriptor()
        {
            if (avatar == null) return null;

            var descriptor = avatar.GetComponent<VRCAvatarDescriptor>();

            if (descriptor == null)
            {
                Debug.LogError("The is no avatar descriptor, please add one");
                return null;
            }

            var anim = avatar.GetComponent<Animator>();
            if (anim == null)
            {
                Debug.LogError("There is no Animator on this avatar! It will not animate properly. Be sure you have set it up as a humanoid rig");
            } else if (!anim.isHuman)
            {
                Debug.LogError("This rig is not humanoid, it will not animate properly");
            }

            return descriptor;
        }
        private void BoilerPlateFindControllers(string path)
        {
            controllers = new List<string>();
            string[] cn = Directory.GetFiles(path, ".", SearchOption.TopDirectoryOnly);
            foreach (var c in cn)
            {
                if (!c.Contains("meta"))
                {
                    controllers.Add(c);
                }
            }

            paramenu = new List<string>();
            string[] pm = Directory.GetFiles(paramenuFolderPath, ".", SearchOption.TopDirectoryOnly);
            foreach (var asset in pm)
            {
                if (asset.Contains("GoAll") && !asset.Contains("meta"))
                {
                    paramenu.Add(asset);
                }
            }

        }

        private void BoilerPlateAddControllersAndMenus(VRCAvatarDescriptor descriptor)
        {
            descriptor.customizeAnimationLayers = true;

            string con;

            try
            {
                con = controllers.Where(c => c.Contains("Base")).ToList()[0];
                descriptor.baseAnimationLayers[0].isDefault = false;
                descriptor.baseAnimationLayers[0].animatorController =
                    AssetDatabase.LoadAssetAtPath<AnimatorController>(con);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The base controller cannot be found");
                throw;
            }
            
            try
            {
                con = controllers.Where(c => c.Contains("Additive")).ToList()[0];
                descriptor.baseAnimationLayers[1].isDefault = false;
                descriptor.baseAnimationLayers[1].animatorController =
                    AssetDatabase.LoadAssetAtPath<AnimatorController>(con);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The Additive controller cannot be found");
                throw;
            }
            
            try
            {
                con = controllers.Where(c => c.Contains("Gesture")).ToList()[0];
                descriptor.baseAnimationLayers[2].isDefault = false;
                descriptor.baseAnimationLayers[2].animatorController =
                    AssetDatabase.LoadAssetAtPath<AnimatorController>(con);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The Gesture Controller controller cannot be found");
                throw;
            }
            
            try
            {
                con = controllers.Where(c => c.Contains("Action")).ToList()[0];
                descriptor.baseAnimationLayers[3].isDefault = false;
                descriptor.baseAnimationLayers[3].animatorController =
                    AssetDatabase.LoadAssetAtPath<AnimatorController>(con);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The Action controller cannot be found");
                throw;
            }
            
            try
            {
                con = controllers.Where(c => c.Contains("FX")).ToList()[0];
                descriptor.baseAnimationLayers[4].isDefault = false;
                descriptor.baseAnimationLayers[4].animatorController =
                    AssetDatabase.LoadAssetAtPath<AnimatorController>(con);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The FX controller cannot be found");
                throw;
            }

            try
            {
                con = controllers.Where(c => c.Contains("Sitting")).ToList()[0];
                descriptor.specialAnimationLayers[0].isDefault = false;
                descriptor.specialAnimationLayers[0].animatorController =
                    AssetDatabase.LoadAssetAtPath<AnimatorController>(con);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The TPose controller cannot be found");
                throw;
            }
            
            try
            {
                con = controllers.Where(c => c.Contains("TPose")).ToList()[0];
                descriptor.specialAnimationLayers[1].isDefault = false;
                descriptor.specialAnimationLayers[1].animatorController =
                    AssetDatabase.LoadAssetAtPath<AnimatorController>(con);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The Sitting controller cannot be found");
                throw;
            }




            try
            {
                var para = paramenu.Where(p => p.Contains("Parameters")).ToList()[0];
                var menu = paramenu.Where(m => m.Contains("MainMenu"))
                    .ToList()[1]; //The first in the List is the incorrect file

                descriptor.customExpressions = true;

                descriptor.expressionParameters = AssetDatabase.LoadAssetAtPath<VRCExpressionParameters>(para);
                descriptor.expressionsMenu = AssetDatabase.LoadAssetAtPath<VRCExpressionsMenu>(menu);
            }
            catch (Exception e)
            {
                Debug.LogWarning("The Menu or Parameters asset cannot be found" + e);
                throw;
            }

        }
        #endregion

        static IEnumerator IEDelayEditor()
        {
            yield return new WaitForSeconds(1f);
        }
        
    }
}
