using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
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
    [FuseeApplication(Name = "Tut11_AssetsPicking", Description = "Yet another FUSEE App.")]
    public class Tut11_AssetsPicking : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRendererForward _sceneRenderer;
        private float _camAngle = 0;
        private Transform _baseTransform;
        private SurfaceEffect _rightRearEffect;
        private Transform _canonrotate;
        private Transform _front;
        private Transform _rear;
        private SceneNode body;
        private SceneNode rotatecanons;
        private SceneNode rotate;
        private ScenePicker _scenePicker;
        private float clickCheck = 0;
        private PickResult _currentPick;
        private float4 _oldColor;
        private SceneNode canon;
        private SceneNode steering;



        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new Transform
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
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
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // SHADER EFFECT COMPONENT
                            SimpleMeshes.MakeMaterial((float4) ColorUint.LightGrey),

                            // MESH COMPONENT
                            // SimpleAssetsPickinges.CreateCuboid(new float3(10, 10, 10))
                            SimpleMeshes.CreateCuboid(new float3(10, 10, 10))
                        }
                    },
                }
            };
        }


        // Init is called on startup. 
        public override void Init()
        {
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = AssetStorage.Get<SceneContainer>("vehicle.fus");

            _baseTransform = _scene.Children.FindNodes((node) => node.Name == "Body")?.FirstOrDefault()?.GetTransform();

            _rightRearEffect = _scene.Children.FindNodes(node => node.Name == "Body")?.FirstOrDefault()?.GetComponent<SurfaceEffect>();

            _canonrotate = _scene.Children.FindNodes(node => node.Name == "rotatecanons")?.FirstOrDefault()?.GetTransform();
            rotatecanons = _scene.Children.FindNodes(node => node.Name == "rotatecanons")?.FirstOrDefault();
            rotate = _scene.Children.FindNodes(node => node.Name == "Window.001")?.FirstOrDefault();
            canon = _scene.Children.FindNodes(node => node.Name == "canonhull")?.FirstOrDefault();

            _front = _scene.Children.FindNodes(node => node.Name == "front-turn")?.FirstOrDefault()?.GetTransform();

            _rear = _scene.Children.FindNodes(node => node.Name == "back-turn")?.FirstOrDefault()?.GetTransform();

            body = _scene.Children.FindNodes(node => node.Name == "Body")?.FirstOrDefault();
            steering = _scene.Children.FindNodes(node => node.Name == "steering")?.FirstOrDefault();




            //_baseTransform.Rotation.y=M.Pi/2;


            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRendererForward(_scene);

            _scenePicker = new ScenePicker(_scene);
        }
        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            SetProjectionAndViewport();

            // Clear the backbuffer
            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            // Setup the camera 
            RC.View = float4x4.CreateTranslation(0, 0, 40) * float4x4.CreateRotationX(-(float)Math.Atan(15.0 / 40.0));

            if (Mouse.LeftButton)
            {
                float2 pickPosClip = Mouse.Position * new float2(2.0f / Width, -2.0f / Height) + new float2(-1, 1);

                PickResult newPick = _scenePicker.Pick(RC, pickPosClip).OrderBy(pr => pr.ClipPos.z).FirstOrDefault();

                if (newPick?.Node != _currentPick?.Node)
                {
                     if (_currentPick != null)
                    {
                        var ef = _currentPick.Node.GetComponent<DefaultSurfaceEffect>();
                        ef.SurfaceInput.Albedo = _oldColor;
                    }
                    if (newPick != null)
                    {
                        if (newPick.Node == rotatecanons || newPick.Node == rotate || newPick.Node ==canon)
                        {
                            clickCheck = 1;
                        }
                        if (newPick.Node == body || newPick.Node == steering)
                        {
                            clickCheck = 2;
                        }
                         var ef = newPick.Node.GetComponent<SurfaceEffect>();
                        _oldColor = ef.SurfaceInput.Albedo;
                        ef.SurfaceInput.Albedo = (float4) ColorUint.OrangeRed;
                    }
                    _currentPick = newPick;
                }

            }
            if (clickCheck == 1)
            {
                _canonrotate.Rotation.y += Keyboard.ADAxis * 0.02f;
                _canonrotate.Rotation.z = Keyboard.WSAxis * 0.1f;


            }
            if (clickCheck == 2)
            {
                //wheels forward / backwards
                float drive = _rear.Rotation.z;
                drive += (-Keyboard.WSAxis) * DeltaTime;
                _rear.Rotation.z = drive;
                _front.Rotation.z = drive;

                //wheels turning
                _rear.Rotation.y = Keyboard.ADAxis * (-0.2f);
                _front.Rotation.y = Keyboard.ADAxis * 0.2f;

                //move car
                float move = _baseTransform.Translation.x;
                move += Keyboard.WSAxis * DeltaTime;
                _baseTransform.Translation.x = move;
            }

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