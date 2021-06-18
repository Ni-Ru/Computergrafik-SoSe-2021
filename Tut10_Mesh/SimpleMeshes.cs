﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fusee.Engine.Core;
using Fusee.Engine.Core.Scene;
using Fusee.Engine.Core.Effects;
using Fusee.Math.Core;
using Fusee.Serialization;

namespace FuseeApp
{
    public static class SimpleMeshes
    {
        public static Mesh CreateCuboid(float3 size)
        {
            return new Mesh
            {
                Vertices = new[]
                {
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = +0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z},
                    new float3 {x = +0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = +0.5f * size.z},
                    new float3 {x = -0.5f * size.x, y = -0.5f * size.y, z = -0.5f * size.z}
                },

                Triangles = new ushort[]
                {
                    // front face
                    0, 2, 1, 0, 3, 2,

                    // right face
                    4, 6, 5, 4, 7, 6,

                    // back face
                    8, 10, 9, 8, 11, 10,

                    // left face
                    12, 14, 13, 12, 15, 14,

                    // top face
                    16, 18, 17, 16, 19, 18,

                    // bottom face
                    20, 22, 21, 20, 23, 22

                },

                Normals = new[]
                {
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(0, 0, 1),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(1, 0, 0),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(0, 0, -1),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(-1, 0, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, 1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0),
                    new float3(0, -1, 0)
                },

                UVs = new[]
                {
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0),
                    new float2(1, 0),
                    new float2(1, 1),
                    new float2(0, 1),
                    new float2(0, 0)
                },
                BoundingBox = new AABBf(-0.5f * size, 0.5f * size)
            };
        }

        public static SurfaceEffect MakeMaterial(float4 color)
        {
            return MakeEffect.FromDiffuseSpecular(
                albedoColor: color,
                emissionColor: float4.Zero,
                shininess: 25.0f,
                specularStrength: 1f);
        }

        public static Mesh CreateCylinder(float radius, float height, int segments)
        {


            float3[] verts = new float3[4 * segments + 2];
            float3[] norms = new float3[4 * segments + 2+16];
            ushort[] tris = new ushort[4 * 3 * segments];

            float delta = 2 * M.Pi / segments;

            //upper center
            verts[segments*4] = new float3(0, 0.5f * height, 0);
            norms[segments*4] = new float3(0,0.5f*height+1,0);

            //bottom center
            verts[segments*4+1] = new float3(0, -0.5f * height, 0);
            norms[segments*4+1] = new float3(0,-0.5f*height-1,0);

            //upper surface start
            verts[0] = new float3(radius, 0.5f * height, 0);
            norms[0] = float3.UnitY;

            //side triangles start
            verts[1] = new float3(radius, 0.5f * height, 0);
            norms[1] = new float3(radius,0,0);

            verts[2] = new float3(radius, -0.5f * height, 0);
            norms[2] = new float3(radius,0,0);

            //bottom surface start
            verts[3] = new float3(radius, -0.5f * height, 0);
            norms[3] = float3.UnitY;
            for (int i = 1; i < segments; i++)
            {
                //verts and norms for top surface
                verts[i*4] = new float3(radius * M.Cos(i * delta), 0.5f * height, radius * M.Sin(i * delta));
                norms[i*4] = float3.UnitY;

                //verts and norms for side triangles
                verts[4 * i + 1] = new float3(radius * M.Cos(i*delta), 0.5f * height, radius * M.Sin(i * delta));
                norms[4 * i + 1] = new float3(M.Cos(i*delta),0,M.Sin(i * delta));

                verts[4 * i + 2] = new float3(radius * M.Cos(i*delta), -0.5f * height, radius * M.Sin(i * delta));
                norms[4 * i + 2] = new float3(M.Cos(i*delta),0,M.Sin(i * delta));

                //verts and norms for bottom surface
                verts[4 * (i) + 3] = new float3(radius * M.Cos(i * delta), -0.5f * height, radius * M.Sin(i * delta));
                norms[4 * (i) + 3] = float3.UnitY;

               //verts and norms for side triangles

                /*tris[3 * i - 1] = (ushort)segments;
                tris[3 * i - 2] = (ushort)i;
                tris[3 * i - 3] = (ushort)(i - 1);*/

                // top triangle
                tris[12 * (i - 1) + 0] = (ushort)(4 * segments);      // top center point
                tris[12 * (i - 1) + 2] = (ushort)(4 * i + 0);         // current top segment point
                tris[12 * (i - 1) + 1] = (ushort)(4 * (i - 1) + 0);   // previous top segment point

                // side triangle 1
                tris[12 * (i - 1) + 3] = (ushort)(4 * (i - 1) + 2);   // previous lower shell point
                tris[12 * (i - 1) + 4] = (ushort)(4 * i + 2);         // current lower shell point
                tris[12 * (i - 1) + 5] = (ushort)(4 * i + 1);         // current top shell point

                // side triangle 2
                tris[12 * (i - 1) + 6] = (ushort)(4 * (i - 1) + 2);   // previous lower shell point
                tris[12 * (i - 1) + 7] = (ushort)(4 * i + 1);         // current top shell point
                tris[12 * (i - 1) + 8] = (ushort)(4 * (i - 1) + 1);   // previous top shell point

                //bottom triangle
                tris[12 * (i - 1) + 9] = (ushort)(4 * segments + 1);  // bottom center point
                tris[12 * (i - 1) + 11] = (ushort)(4 * (i - 1) + 3);  // current bottom segment point
                tris[12 * (i - 1) + 10] = (ushort)(4 * i + 3);        // previous bottom segment point

            };

            //upper surface last tri
            tris[12 * (segments - 1)+0] = (ushort)(4*segments);
            tris[12 * (segments - 1)+1] = (ushort)(4*(segments - 1));
            tris[12 * (segments - 1)+2] = (ushort)0;

            //side last tri1
            tris[12 * (segments - 1) + 3] = (ushort)(4 * (segments - 1) + 2);  
            tris[12 * (segments - 1) + 4] = (ushort)2;         
            tris[12 * (segments - 1) + 5] = (ushort)1;  

            // side last tri2
            tris[12 * (segments - 1) + 6] = (ushort)(4 * (segments - 1) + 2);  
            tris[12 * (segments - 1) + 7] = (ushort)1;     
            tris[12 * (segments - 1) + 8] = (ushort)(4 * (segments - 1) + 1);                 

            //bottom surface last tri
            tris[12 * (segments - 1)+9] = (ushort)(4*segments+1);
            tris[12 * (segments - 1)+11] = (ushort)(4*segments-1);
            tris[12 * (segments - 1)+10] = (ushort)3;
            return new Mesh
            {
                Vertices = verts,
                Normals = norms,
                Triangles = tris,
            };
        }

        public static Mesh CreateCone(float radius, float height, int segments)
        {
            return CreateConeFrustum(radius, 0.0f, height, segments);
        }

        public static Mesh CreateConeFrustum(float radiuslower, float radiusupper, float height, int segments)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreatePyramid(float baselen, float height)
        {
            throw new NotImplementedException();
        }
        public static Mesh CreateTetrahedron(float edgelen)
        {
            throw new NotImplementedException();
        }

        public static Mesh CreateTorus(float mainradius, float segradius, int segments, int slices)
        {
            throw new NotImplementedException();
        }

    }
}
