﻿using System;
using HarmonyLib;
using UnityEngine;

namespace Trainer
{
    public class Bootstrapper : MonoBehaviour
    {        
        private static GameObject trainer = null;

        internal static GameObject Create(string name)
        {
            try
            {
                var obj = new GameObject(name);
                var component = new Bootstrapper(obj.AddComponent(UnhollowerRuntimeLib.Il2CppType.Of<Bootstrapper>()).Pointer);
                return obj;
            }
            catch { return null; }

        }
        
        public Bootstrapper(IntPtr intPtr) : base(intPtr) { }
        
        [HarmonyPostfix]
        public static void Update()
        {
            try
            {
                
                if (trainer == null)
                {
                    try
                    {
                        trainer = TrainerComponent.Create("TrainerComponentGO");
                        if (trainer != null) { BepInExLoader.log.LogMessage(" "); }
                    }
                    catch(Exception e)
                    {
                        BepInExLoader.log.LogError("ERROR Bootstrapping Trainer: " + e.Message);
                        BepInExLoader.log.LogMessage(" ");
                    }
                }
                
            }
            catch { }   
            
        }
        
    }
}
