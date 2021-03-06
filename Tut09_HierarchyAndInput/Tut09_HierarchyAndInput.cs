using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;
using Fusee.Engine.GUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuseeApp
{
    [FuseeApplication(Name = "Tut09_HierarchyAndInput", Description = "Yet another FUSEE App.")]
    public class Tut09_HierarchyAndInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Transform _baseTransform;
        private Transform _bodyTransform;
        private Transform _upperArmTransform;
        private Transform _forearmTransform;
        private Transform grabBottomTransform;
        private Transform grabTopTransform;
        private float clawState =1;
        private Boolean damping;
        private float cameraV;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };

            _bodyTransform = new Transform
            {
                Rotation = new float3(0, 0.2f, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 1, 0)
            };

            _upperArmTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 9, 0)
            };

            _forearmTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 8, 0)
            };

            grabBottomTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 9, 0)
            };

            grabTopTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 9, 0)
            };

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNode>
                {
                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            // TRANSFORM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            MakeEffect.FromDiffuseSpecular((float4) ColorUint.LightGrey, float4.Zero),

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        },


                    Children = new ChildList
                    {

                    new SceneNode
                    {
                        Components = new List<SceneComponent>
                        {
                            _bodyTransform,
                        },

                        Children = new ChildList{

                        new SceneNode
                        {
                            Components = new List<SceneComponent>
                            {
                                new Transform
                                {
                                    Rotation = new float3(0, 0, 0),
                                    Scale = new float3(1, 1, 1),
                                    Translation = new float3(0, 5, 0)
                                },
                                MakeEffect.FromDiffuseSpecular((float4) ColorUint.Red, float4.Zero),
                                SimpleMeshes.CreateCuboid(new float3(2,10,2))   
                            }
                        },
                        //Upper Arm
                        new SceneNode
                        {
                            Components = new List<SceneComponent>
                            {
                                _upperArmTransform,
                            },

                            Children = new ChildList
                            {
                                new SceneNode
                                {
                                    Components = new List<SceneComponent>
                                    {
                                        new Transform
                                        {
                                            Rotation = new float3(0,0,0),
                                            Scale = new float3(1,1,1),
                                            Translation= new float3(0,4,0)
                                        },
                                        MakeEffect.FromDiffuseSpecular((float4)ColorUint.Green, float4.Zero),
                                        SimpleMeshes.CreateCuboid(new float3(2,10,2))
                                    }
                                },
                                //Forearm
                                new SceneNode
                                {
                                    Components = new List<SceneComponent>
                                    {
                                        _forearmTransform,
                                    },

                                    Children = new ChildList
                                    {
                                        new SceneNode
                                        {
                                            Components = new List<SceneComponent>
                                            {
                                                new Transform
                                                {
                                                    Rotation = new float3(0,0,0),
                                                    Scale = new float3(1,1,1),
                                                    Translation= new float3(0,4,0)
                                                },
                                                MakeEffect.FromDiffuseSpecular((float4)ColorUint.Blue, float4.Zero),
                                                SimpleMeshes.CreateCuboid(new float3(2,10,2))
                                            }
                                        },

                                        //Claw
                                        //"Bottom" Part
                                        new SceneNode
                                        {
                                            Components = new List<SceneComponent>
                                            {
                                                grabBottomTransform
                                            },

                                            Children = new ChildList
                                            {
                                                new SceneNode
                                                {
                                                    Components = new List<SceneComponent>
                                                    {
                                                        new Transform
                                                        {
                                                            Rotation = new float3(0,0,0),
                                                            Scale = new float3(1,1,1),
                                                            Translation= new float3(0.5f,2,0)  
                                                        },
                                                        MakeEffect.FromDiffuseSpecular((float4)ColorUint.Black, float4.Zero),
                                                        SimpleMeshes.CreateCuboid(new float3(1,4,2))
                                                    }
                                                }

                                            }
                                        },
                                        //"Top" Part
                                        new SceneNode
                                        {
                                            Components = new List<SceneComponent>
                                            {
                                                grabTopTransform
                                            },

                                            Children = new ChildList
                                            {
                                                new SceneNode
                                                {
                                                    Components = new List<SceneComponent>
                                                    {
                                                        new Transform
                                                        {
                                                            Rotation = new float3(0,0,0),
                                                            Scale = new float3(1,1,1),
                                                            Translation= new float3(-0.5f,2,0)  
                                                        },
                                                        MakeEffect.FromDiffuseSpecular((float4)ColorUint.BlueViolet, float4.Zero),
                                                        SimpleMeshes.CreateCuboid(new float3(1,4,2))
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        }
                        }

                    }
                }


            }

            };
        }


        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            float bodyRot = _bodyTransform.Rotation.y;
            bodyRot += 1.5f * (-Keyboard.LeftRightAxis) * DeltaTime;

            float moveArmX = _bodyTransform.Rotation.x;
            moveArmX += 0.2f * Keyboard.UpDownAxis * DeltaTime;

            float moveArmY = _bodyTransform.Rotation.z;
            moveArmY += 0.2f * (-Keyboard.ADAxis) * DeltaTime;

            //make claw open and close by pressing a Button once

            float openClaw = grabBottomTransform.Rotation.z;
            float openTopClaw = grabTopTransform.Rotation.z;
            if(Keyboard.IsKeyDown(KeyCodes.E)==true){
                clawState *= -1;
                if(clawState<0){
                    openClaw = -1;
                    openTopClaw =1;
                }
                if(clawState>0){
                    openClaw = 0;
                    openTopClaw =0;
                }
            };

            grabBottomTransform.Rotation = new float3(0,0,openClaw);
            grabTopTransform.Rotation = new float3(0,0,openTopClaw);
            _bodyTransform.Rotation = new float3(moveArmX, bodyRot, moveArmY);
            _upperArmTransform.Rotation = new float3(moveArmX,0,moveArmY);
            _forearmTransform.Rotation = new float3(moveArmX,0,moveArmY);




            SetProjectionAndViewport();

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            //animate camangle

            //make Camera controllable

            if(Mouse.LeftButton==true){
                _camAngle += 0.005f * (-Mouse.Velocity.x) * DeltaTime;
                damping=true;
                if(Mouse.Velocity.x!=0 ){
                    cameraV=Mouse.Velocity.x;
                }
            }

            //Add damping

            if(damping==true && Mouse.LeftButton==false){
                _camAngle += 0.005f * (-cameraV) * DeltaTime;
                if(cameraV>0){
                    cameraV = cameraV-30;
                    if(cameraV-30<0){
                        cameraV=0;
                    }
                }
                if(cameraV<0){
                    cameraV = cameraV+30;
                    if(cameraV+30>0){
                        cameraV=0;
                    }
                }
            }

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, -10, 70) * float4x4.CreateRotationY(_camAngle);

            // Render the scene on the current render context
            _sceneRenderer.Render(RC);

            // Swap buffers: Show the contents of the backbuffer (containing the currently rendered frame) on the front buffer.
            Present();
        }

        public void SetProjectionAndViewport()
        {
            // Set the rendering area to the entire window size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}