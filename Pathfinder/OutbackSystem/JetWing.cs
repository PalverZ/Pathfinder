﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.IO;

/*
Source code copyrighgt 2015, by Michael Billard (Angel-125)
License: CC BY-NC-SA 4.0
License URL: https://creativecommons.org/licenses/by-nc-sa/4.0/
If you want to use this code, give me a shout on the KSP forums! :)
Wild Blue Industries is trademarked by Michael Billard and may be used for non-commercial purposes. All other rights reserved.
Note that Wild Blue Industries is a ficticious entity 
created for entertainment purposes. It is in no way meant to represent a real entity.
Any similarity to a real entity is purely coincidental.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace WildBlueIndustries
{
    class JetWing : PartModule
    {
        public bool engineActivated;
        ModuleEnginesFX engine;
        VesselAutopilot autopilot;

        public override void OnStart(StartState state)
        {
            base.OnStart(state);

            engine = this.part.FindModuleImplementing<ModuleEnginesFX>();
            autopilot = this.part.FindModuleImplementing<VesselAutopilot>();

            if (autopilot != null)
            {
                autopilot.Enable(VesselAutopilot.AutopilotMode.StabilityAssist);
                Debug.Log("FRED I have an autopilot");
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Input.GetKeyDown(KeyCode.Z) && engineActivated == false)
            {
                engine.Activate();
                engine.currentThrottle = 1.0f;
                engineActivated = true;
            }

            if (engineActivated)
            {
                engine.currentThrottle = 1.0f;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                engine.currentThrottle = 0f;
                engine.Shutdown();
                engineActivated = false;
            }

            if (Input.GetKey(KeyCode.T))
            {
                part.vessel.ActionGroups.SetGroup(KSPActionGroup.SAS, true);
            }
        }

    }
}