using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Windows;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3.Avatars.ScriptableObjects;
using VRC.SDKBase;
using Directory = System.IO.Directory;
// using Unity.EditorCoroutines.Editor;


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
            
            GUILayout.Space(20);

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
            
            GUILayout.Space(10);
            
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

            if (GUILayout.Button("Assign Eyebones"))
            {
                var descriptor = BoilerPlateGetDescriptor();
                
                if (descriptor == null) return;

                if (!descriptor.enableEyeLook)
                {
                    descriptor.enableEyeLook = true;
                }
                
                AssignEyeBones(descriptor);
            }

            if (GUILayout.Button("Assign Blink Blendshape"))
            {
                var descriptor = BoilerPlateGetDescriptor();
                
                if (descriptor == null) return;

                if (!descriptor.enableEyeLook)
                {
                    descriptor.enableEyeLook = true;
                }
                
                AssignBlink(descriptor);
                
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

            return descriptor;
        }

        private Animator BoilerPlateGetAnimator()
        {
            Animator anim = avatar.GetComponent<Animator>();
            if (anim == null)
            {
                Debug.LogError("There is no Animator on this avatar! It will not animate properly. Be sure you have set it up as a humanoid rig");
            } else if (!anim.isHuman)
            {
                Debug.LogError("This rig is not humanoid, it will not animate properly");
            }

            return anim;
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

        private void AssignEyeBones(VRCAvatarDescriptor descriptor)
        {
            Animator animator = BoilerPlateGetAnimator();

            Transform leftEyeBone = null;
            Transform rightEyeBone = null;

            if (animator != null && animator.isHuman) 
            {
                leftEyeBone = animator.GetBoneTransform(HumanBodyBones.LeftEye);
                rightEyeBone = animator.GetBoneTransform(HumanBodyBones.RightEye);
            }

            if (leftEyeBone == null || rightEyeBone == null)
            {
                List<Transform> transforms = avatar.GetComponentsInChildren<Transform>().ToList();

                List<Transform> eyebones = new List<Transform>();
                
                foreach (Transform t in transforms)
                {
                    
                    if (t.gameObject.name.ToLower().Contains("eye"))
                    {
                        eyebones.Add(t);
                    }
                }
                
                Debug.Log($"Total eyes are {eyebones.Count}");
                if (eyebones.Count == 0)
                {
                    Debug.LogWarning("There are no eye bones assigned via the animator or there isn't a name it could find in it hierarchy");
                    return;
                }

                descriptor.enableEyeLook = true;

                leftEyeBone = eyebones.Where(e => e.gameObject.name.ToUpper().Contains("L"))
                    .Select(e => e).SingleOrDefault();
                rightEyeBone = eyebones.Where(e => e.gameObject.name.ToUpper().Contains("R"))
                    .Select(e => e).SingleOrDefault();
            }

            descriptor.customEyeLookSettings.leftEye = leftEyeBone;
            descriptor.customEyeLookSettings.rightEye = rightEyeBone;
        }

        private void AssignBlink(VRCAvatarDescriptor descriptor)
        {
            Debug.Log("Fake break");
            Animator animator = BoilerPlateGetAnimator();

            SkinnedMeshRenderer sm = null;

            if (descriptor.VisemeSkinnedMesh != null)
            {
                sm = descriptor.VisemeSkinnedMesh;
            }
            else
            {
                SkinnedMeshRenderer[] smrs = avatar.GetComponentsInChildren<SkinnedMeshRenderer>(true);
                for (int i = 0; i < smrs.Length; i++)
                {
                    if (i > 0 && smrs[i].sharedMesh.blendShapeCount > smrs[i-1].sharedMesh.blendShapeCount)
                    {
                        sm = smrs[i];
                    }
                    else
                    {
                        sm = smrs[i];
                    }
                }
            }
            
            descriptor.customEyeLookSettings.eyelidType = VRCAvatarDescriptor.EyelidType.Blendshapes;
            descriptor.customEyeLookSettings.eyelidsSkinnedMesh = sm;

            if (sm == null)
            {
                Debug.LogError("Could not find a skinned mesh as part of this avatar.");
            }

            Mesh m = sm.sharedMesh;
            
            int bsI = 0;

            for (int i = 0; i < m.blendShapeCount; i++)
            {
                if (m.GetBlendShapeName(i).ToLower().Equals("blink") || m.GetBlendShapeName(i).ToLower().Equals("ブリンク"))
                {
                    bsI = i;
                    break;
                } else if (i == m.blendShapeCount - 1)
                {
                    Debug.LogWarning("Could not find a blendshape named blink");
                    bsI = -1;
                }
            }

            if (descriptor.customEyeLookSettings.eyelidsBlendshapes.Length < 1)
            {
                EditorUtility.DisplayDialog("You're not in trouble", "Assign Blink Blendshape again\n" +
                                                                     "Assignments might be incorrect", "ok", "cancel");
            }
            
            descriptor.customEyeLookSettings.eyelidsBlendshapes[0] = bsI;
            descriptor.customEyeLookSettings.eyelidsBlendshapes[1] = -1;
            descriptor.customEyeLookSettings.eyelidsBlendshapes[2] = -1;
            
        }

    }
}
