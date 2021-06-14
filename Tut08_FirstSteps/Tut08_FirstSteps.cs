using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Effects;
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
    [FuseeApplication(Name = "Tut08_FirstSteps", Description = "Yet another FUSEE App.")]
    public class Tut08_FirstSteps : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Transform _cubeTransform;

        // extra Animations

        private Transform _cubeScale;

        private Transform _cubeRotate;

        private float _cubeAngle =0;

        private DefaultSurfaceEffect _cubeShading;

        

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to "white"
            RC.ClearColor = new float4(1,1,1,1);

            //Create a scene
            //3 Components: Transform, Shader and Mesh
            _cubeTransform = new Transform {Translation = new float3(0,0,0), Scale = new float3(1,1,1)};
            var cubeShader = MakeEffect.FromDiffuseSpecular(new float4(1,0,0,1), float4.Zero);
            var cubeMesh = SimpleMeshes.CreateCuboid(new float3(5,5,5)); 
            _cubeShading = MakeEffect.FromDiffuseSpecular(new float4(1,0,0,1), float4.Zero);


            //Assemble the cube node containing the components
            var cubeNode = new SceneNode();
            cubeNode.Components.Add(_cubeTransform);
            cubeNode.Components.Add(cubeShader);
            cubeNode.Components.Add(cubeMesh);

            //Create Scene containing the cube as only object
            _scene = new SceneContainer();
            _scene.Children.Add(cubeNode);


            //Declare Transformations
            _cubeScale = new Transform { Scale = new float3(2,5,1)};
            _cubeRotate = new Transform{Rotation = new float3(0,0,0)};

            //Assemble more cubes
            var cubes = new SceneNode[9];
            
            for(int i =0; i<cubes.Length; i++){
                int pos = i*10;
                var cubePosition = new Transform{Translation = new float3(pos-50,0,0)};
                cubes[i] = new SceneNode();

                if(i<3){
                    cubes[i].Components.Add(_cubeScale);
                    cubes[i].Components.Add(cubeShader);
                }
                if(i>=3 && i<6){
                    cubes[i].Components.Add(_cubeShading);
                }
                if(i>=6){
                    cubes[i].Components.Add(_cubeRotate);
                    cubes[i].Components.Add(cubeShader);
                }
                cubes[i].Components.Add(cubePosition);
                cubes[i].Components.Add(cubeMesh);
                _scene.Children.Add(cubes[i]);
            }
          

            //Create Scenerenderer for scene created above
            _sceneRenderer = new SceneRendererForward(_scene);

        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            //Animate camera angle
           // _camAngle = _camAngle + 90.0f * M.Pi/180.0f * DeltaTime;
            Diagnostics.Log(_camAngle);

            //Animate cube
            _cubeTransform.Translation = new float3(5*M.Sin(3*TimeSinceStart),10,5*M.Sin(3*TimeSinceStart));

            //More Animations
            //Scale in y direction
            _cubeScale.Scale = new float3(10*M.Sin(TimeSinceStart)+10,10*M.Sin(TimeSinceStart)+10,10*M.Sin(TimeSinceStart)+10);
            //rotation
            _cubeAngle = _cubeAngle +   45.0f * M.Pi/180.0f * DeltaTime;
            _cubeRotate.Rotation = new float3(_cubeAngle,0,0);
            //color
            _cubeShading.SurfaceInput.Albedo = new float4(0.5f + 0.5f * M.Sin(2*Time.TimeSinceStart),0,0,1);


            //Setup Camera
            RC.View=float4x4.CreateTranslation(0,0,500)*float4x4.CreateRotationY(_camAngle);

            //Render scene
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